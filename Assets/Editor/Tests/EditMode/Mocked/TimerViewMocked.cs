using Timer;

namespace Editor.Tests.EditMode.Mocked
{
    public class TimerViewMocked : ITimerView
    {
        public float LastTimeUpdated { get; private set; }

        public void UpdateUI(float remainingTime)
        {
            LastTimeUpdated = remainingTime;
        }
    }
}