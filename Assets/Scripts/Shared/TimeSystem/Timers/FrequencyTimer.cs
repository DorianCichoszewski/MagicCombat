namespace Shared.TimeSystem.Timers
{
	public class FrequencyTimer : Timer
	{
		public float TicksPerSecond
		{
			get => 1f / CurrentTime;
			set => Reset(1f / value);
		}

		public FrequencyTimer(float value) : base(value) { }


		public override void Tick(float deltaTime)
		{
			if (IsRunning) CurrentTime -= deltaTime;

			// Support for timers which are below deltaTime
			while (CurrentTime < 0)
			{
				TriggerEvent();
				CurrentTime += initialTime;
			}
		}

		public override bool IsFinished => false;
	}
}