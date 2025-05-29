using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EndGame
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private Button mainMenuButton;

        public Action OnReturnToMainMenu;

        public void Setup(string message)
        {
            resultText.text = message;
            mainMenuButton.onClick.AddListener(TriggerReturnToMainMenu);
        }

        private void TriggerReturnToMainMenu()
        {
            OnReturnToMainMenu?.Invoke();
        }

        public void Dispose()
        {
            mainMenuButton.onClick.RemoveAllListeners();
        }
    }
}
