using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly MainMenuView _view;

        public MainMenuPresenter(MainMenuView view)
        {
            _view = view;
            _view.Setup();
            _view.OnPlayButtonClicked += HandlePlay;
            _view.OnQuitButtonClicked += HandleQuit;
        }

        public void Dispose()
        {
            _view.OnPlayButtonClicked -= HandlePlay;
            _view.OnQuitButtonClicked -= HandleQuit;
            _view.Dispose();
        }

        private void HandlePlay()
        {
            SceneManager.LoadScene("Game");
        }

        private void HandleQuit()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
