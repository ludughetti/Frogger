namespace Timer
{
    public class TimerModel
    {
        public float RemainingTime { get; private set; }
        public float StartTime { get; }
        
        public bool IsRunning { get; private set; }

        public TimerModel(float startTimeInSeconds)
        {
            StartTime = startTimeInSeconds;
            RemainingTime = StartTime;
            IsRunning = true;
        }

        public void UpdateRemainingTime(float timeInSeconds)
        {
            RemainingTime = timeInSeconds;

            if (RemainingTime <= 0)
            {
                IsRunning = false;
            }
        }

        public void Reset()
        {
            RemainingTime = StartTime;
            IsRunning = true;
        }

        // Toggle to Pause or Unpause depending on bool
        public void Toggle(bool isRunning)
        {
            IsRunning = isRunning;
        }
    }
}
