using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace MagicCombat.Shared.TimeSystem
{
	public abstract class Clock
	{
		public event Action<float> OnTick;
		private readonly List<TimerBase> timers = new();

		public bool Paused { get; set; } = false;
		public float Speed { get; set; } = 1;

		public void RegisterTimer(TimerBase timer)
		{
			timers.Add(timer);
		}

		public void DeregisterTimer(TimerBase timer)
		{
			timers.Remove(timer);
		}

		public void Clear()
		{
			OnTick = null;
			timers.Clear();
		}

		public void UpdateTimers()
		{
			if (Paused) return;

			float deltaTime = DeltaTime * Speed;
			OnTick?.Invoke(deltaTime);
			foreach (var timer in new List<TimerBase>(timers))
			{
				timer.Tick(deltaTime);
			}
		}

		public void SetupTimer(TimerBase timer)
		{
			timer.OnTimerStart += RegisterTimer;
			timer.OnTimerStop += DeregisterTimer;
		}

		public Timer CreateTimer(Action callback, float value = 0, string name = null,
			TimerType type = TimerType.Countdown, bool autoStart = true)
		{
			var timer = TimerFactory.CreateTimer(this, type, value);
			timer.Name = name;
			timer.OnTimerTrigger += callback;
			if (autoStart)
				timer.Start();
			return timer;
		}

		protected abstract float DeltaTime { get; }
	}

	public abstract class Clock<T> : Clock, IDisposable
	{
		private PlayerLoopSystem timerSystem;

#if UNITY_EDITOR
		private static readonly List<Clock<T>> clocks = new();
		private static void OnPlayModeState(UnityEditor.PlayModeStateChange state)
		{
			if (state == UnityEditor.PlayModeStateChange.ExitingPlayMode)
			{
				var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
				foreach (var clock in clocks)
				{
					PlayerLoopUtils.RemoveSystem<T>(ref currentPlayerLoop, in clock.timerSystem);
				}

				PlayerLoop.SetPlayerLoop(currentPlayerLoop);
			}
		}
#endif

		protected Clock()
		{
			var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			if (InsertTimerManager(ref currentPlayerLoop, 0))
				PlayerLoop.SetPlayerLoop(currentPlayerLoop);
			else
				Debug.LogError($"Couldn't insert {GetType()}");
#if UNITY_EDITOR
			clocks.Add(this);
			UnityEditor.EditorApplication.playModeStateChanged -= OnPlayModeState;
			UnityEditor.EditorApplication.playModeStateChanged += OnPlayModeState;
#endif
		}

		private bool InsertTimerManager(ref PlayerLoopSystem loop, int index)
		{
			timerSystem = new PlayerLoopSystem
			{
				type = GetType(),
				updateDelegate = UpdateTimers,
				subSystemList = null
			};
			return PlayerLoopUtils.InsertSystem<T>(ref loop, in timerSystem, index);
		}

		public void Dispose()
		{
			Clear();
			var currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
			PlayerLoopUtils.RemoveSystem<T>(ref currentPlayerLoop, in timerSystem);
			PlayerLoop.SetPlayerLoop(currentPlayerLoop);
		}
	}

	public class ClockUpdate : Clock<Update>
	{
		protected override float DeltaTime => Time.deltaTime;
	}

	public class ClockFixedUpdate : Clock<FixedUpdate>
	{
		protected override float DeltaTime => Time.deltaTime;
	}
}