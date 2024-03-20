using System.Collections.Generic;
using MagicCombat.Gameplay.Player;
using MagicCombat.Gameplay.Time;
using UnityEngine;

namespace MagicCombat.Spell
{
	public class Spell : MonoBehaviour
	{
		private readonly List<ISpellFragment> createdFragments = new();

		private readonly List<Timer> usedTimers = new();
		private SpellData data;

		public PropertyGroup Properties => Prototype.properties;
		public SpellPrototype Prototype { get; private set; }

		public SpellData Data => data;

		private void OnCollisionEnter(Collision other)
		{
			HitEvent(other.gameObject);
		}

		private void OnTriggerEnter(Collider other)
		{
			HitEvent(other.gameObject);
		}

		public void Init(SpellPrototype spellPrototype, SpellData spellData)
		{
			Prototype = spellPrototype;
			data = spellData;
			gameObject.name = Prototype.name;

			AddRigidbody();
			InitFragments();
			InitTimers();

			data.gameplayGlobals.clockManager.DynamicClock.ClockUpdate += Tick;
		}

		public void Destroy()
		{
			if (Prototype.useDestroyEvents)
				foreach (var destroyEvent in Prototype.destroyEvents)
				{
					destroyEvent.Perform(this);
				}

			foreach (var createdFragment in createdFragments)
			{
				createdFragment.OnDestroyEvent(this);
			}

			foreach (var timer in usedTimers)
			{
				timer.Cancel();
			}

			data.gameplayGlobals.clockManager.DynamicClock.ClockUpdate -= Tick;

			Destroy(gameObject);
		}

		private void AddRigidbody()
		{
			var rb = gameObject.AddComponent<Rigidbody>();
			rb.useGravity = false;
			rb.freezeRotation = false;
			rb.drag = 0;
			rb.angularDrag = 0;
			rb.isKinematic = true;
			gameObject.layer = LayerMask.NameToLayer("Ethereal");
		}

		private void InitFragments()
		{
			var cachedTransform = transform;
			foreach (var simpleFragment in Prototype.graphicalFragments)
			{
				Instantiate(simpleFragment, cachedTransform.position, Quaternion.identity, cachedTransform);
			}

			foreach (var visualFragment in Prototype.visualFragments)
			{
				Instantiate(visualFragment, cachedTransform.position, Quaternion.identity, cachedTransform);
				createdFragments.Add(visualFragment);
			}

			foreach (var logicalFragment in Prototype.logicalFragments)
			{
				createdFragments.Add(logicalFragment);
			}

			foreach (var fragment in createdFragments)
			{
				fragment.Init(this);
			}
		}

		private void InitTimers()
		{
			if (!Prototype.useTimers) return;

			foreach (var timer in Prototype.timers)
			{
				usedTimers.Add(timer.Start(this));
			}
		}

		private void Tick(float deltaTime)
		{
			foreach (var fragment in createdFragments)
			{
				fragment.Tick(this, deltaTime);
			}
		}

		private void HitEvent(GameObject other)
		{
			var otherAvatar = other.GetComponentInParent<PlayerAvatar>();
			if (otherAvatar != null)
			{
				if (Prototype.UsePlayerHitEvents)
					foreach (var hitEvent in Prototype.playerHitEvents)
					{
						hitEvent.Perform(this, otherAvatar);
					}
			}
			else if (Prototype.UseOtherHitEvents)
			{
				foreach (var hitEvent in Prototype.otherHitEvents)
				{
					hitEvent.Perform(this, other);
				}
			}

			if (Prototype.UseAllHitEvents)
				foreach (var hitEvent in Prototype.allHitEvents)
				{
					hitEvent.Perform(this, other);
				}
		}
	}
}