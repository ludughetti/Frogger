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
        [SerializeField] private AudioSource sfxSource;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip playerWin;
        [SerializeField] private AudioClip playerLose;

        public Action OnReturnToMainMenu;

        public void Setup(string message, bool playerWon)
        {
            resultText.text = message;
            mainMenuButton.onClick.AddListener(TriggerReturnToMainMenu);
            
            // Trigger sound
            PlayEndGameSound(playerWon);
        }

        private void TriggerReturnToMainMenu()
        {
            OnReturnToMainMenu?.Invoke();
        }

        public void Dispose()
        {
            mainMenuButton.onClick.RemoveAllListeners();
        }

        private void PlayEndGameSound(bool playerWon)
        {
            sfxSource.PlayOneShot(playerWon ? playerWin : playerLose);
        }
    }
}
