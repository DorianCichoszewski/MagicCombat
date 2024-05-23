// https://github.com/adammyhre/Unity-Improved-Timers

using System;
using UnityEngine;

namespace MagicCombat.Shared.TimeSystem
{
	public abstract class TimerBase : IDisposable
	{
		public string Name { get; set; }
		public bool IsRunning { get; private set; } = false;

		public event Action<TimerBase> OnTimerStart = delegate { };
		public event Action<TimerBase> OnTimerStop = delegate { };
		public event Action OnTimerTrigger = delegate { };

		public void Start()
		{
			Reset();
			if (!IsRunning)
			{
				IsRunning = true;
				OnTimerStart(this);
			}
		}

		public void Stop()
		{
			if (IsRunning)
			{
				IsRunning = false;
				OnTimerStop.Invoke(this);
			}
		}

		protected void TriggerEvent()
		{
			OnTimerTrigger();
		}

		public abstract void Tick(float deltaTime);
		public abstract bool IsFinished { get; }

		public void Resume() => IsRunning = true;
		public void Pause() => IsRunning = false;

		public abstract void Reset();

		private bool disposed;

		~TimerBase()
		{
			Dispose(false);
		}

		// Call Dispose to ensure deregistration of the timer from the TimerManager
		// when the consumer is done with the timer or being destroyed
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposed) return;

			if (IsRunning && disposing)
				OnTimerStop(this);

			disposed = true;
		}

		public override string ToString()
		{
			return $"{Name} {GetType()} {(IsRunning ? "Running" : "Stopped")}";
		}
	}

	public abstract class Timer<T> : TimerBase
	{
		public T CurrentTime { get; protected set; }

		protected T initialTime;

		protected Timer(T time)
		{
			initialTime = time;
		}

		public override void Reset()
		{
			CurrentTime = initialTime;
		}

		public virtual void Reset(T newData)
		{
			initialTime = newData;
			Reset();
		}
	}

	public abstract class Timer : Timer<float>
	{
		public float Progress => Mathf.Clamp(CurrentTime / initialTime, 0, 1);

		protected Timer(float value) : base(value) { }
	}
}