using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] protected TMP_Text timerText;
        
        // Prints the result in a mm:ss format
        public void UpdateUI(float remainingTime)
        {
            var minutes = Mathf.FloorToInt(remainingTime / 60f);
            var seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
