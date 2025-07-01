using Timer;
using TMPro;

namespace Editor.Tests.PlayMode.Mocked
{
    public class TimerViewMocked : TimerView
    {
        public void SetTimerText(TMP_Text text)
        {
            timerText = text;
        }
    }
}