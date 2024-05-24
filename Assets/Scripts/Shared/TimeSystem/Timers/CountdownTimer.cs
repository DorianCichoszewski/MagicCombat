namespace Shared.TimeSystem.Timers
{
	public class CountdownTimer : Timer
	{
		public CountdownTimer(float value) : base(value) { }

		public override void Tick(float deltaTime)
		{
			if (IsRunning && CurrentTime > 0) CurrentTime -= deltaTime;

			if (IsRunning && CurrentTime <= 0)
			{
				Stop();
				TriggerEvent();
			}
		}

		public override bool IsFinished => CurrentTime <= 0;

		public float RemainingTime => initialTime - CurrentTime;
		public float RemainingPercent => (initialTime - CurrentTime) / initialTime;
	}
}