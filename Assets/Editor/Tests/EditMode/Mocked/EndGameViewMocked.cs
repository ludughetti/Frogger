using System;
using UI.EndGame;

namespace Editor.Tests.EditMode.Mocked
{
    public class EndGameViewMocked : IEndGameView
    {
        public string SetupMessage { get; private set; }
        public bool SetupPlayerWon { get; private set; }
        public event Action OnReturnToMainMenu;
        public bool DisposeCalled { get; private set; }
        public bool OnReturnToMainMenuSubscribed { get; private set; }

        public void Setup(string message, bool playerWon)
        {
            SetupMessage = message;
            SetupPlayerWon = playerWon;
            OnReturnToMainMenuSubscribed = true;
        }

        public void TriggerReturnToMainMenu()
        {
            OnReturnToMainMenu?.Invoke();
        }

        public void Dispose()
        {
            DisposeCalled = true;
            OnReturnToMainMenuSubscribed = false;
        }
    }
}