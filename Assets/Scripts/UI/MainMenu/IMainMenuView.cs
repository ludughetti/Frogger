using System;

namespace UI.MainMenu
{
    public interface IMainMenuView
    {
        void Setup();
        void Dispose();
        event Action OnPlayButtonClicked;
        event Action OnQuitButtonClicked;
    }
}