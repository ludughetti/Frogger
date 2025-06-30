using System;

namespace UI.EndGame
{
    public interface IEndGameView
    {
        void Setup(string message, bool playerWon);
        event Action OnReturnToMainMenu;
        void Dispose();
    }
}