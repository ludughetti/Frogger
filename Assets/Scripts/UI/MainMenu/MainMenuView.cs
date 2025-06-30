using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuView : MonoBehaviour, IMainMenuView
    {
        [Header("UI References")]
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        public event Action OnPlayButtonClicked;
        public event Action OnQuitButtonClicked;

        public void Setup()
        {
            playButton.onClick.AddListener(TriggerPlay);
            quitButton.onClick.AddListener(TriggerQuit);
        }

        public void Dispose()
        {
            playButton.onClick.RemoveAllListeners();
            quitButton.onClick.RemoveAllListeners();
        }

        private void TriggerPlay()
        {
            OnPlayButtonClicked?.Invoke();
        }

        private void TriggerQuit()
        {
            OnQuitButtonClicked?.Invoke();
        }
    }
}
