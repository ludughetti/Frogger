using System;
using UI.MainMenu;

namespace Editor.Tests.EditMode.Mocked
{
    class MainMenuViewMocked : IMainMenuView
    {
        public bool SetupCalled { get; private set; }
        public bool DisposeCalled { get; private set; }
        public bool PlaySubscribed { get; private set; }
        public bool QuitSubscribed { get; private set; }

        public event Action OnPlayButtonClicked;
        public event Action OnQuitButtonClicked;

        public void Setup()
        {
            SetupCalled = true;
            PlaySubscribed = true;
            QuitSubscribed = true;
        }

        public void Dispose()
        {
            DisposeCalled = true;
            PlaySubscribed = false;
            QuitSubscribed = false;
        }

        public void TriggerPlay() => OnPlayButtonClicked?.Invoke();
        public void TriggerQuit() => OnQuitButtonClicked?.Invoke();
    }
}