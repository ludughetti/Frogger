using Utils;

namespace UI.MainMenu
{
    public class MainMenuPresenter
    {
        private readonly IMainMenuView _view;
        private readonly ISceneLoader _sceneLoader;
        private readonly IApplicationHandler _applicationHandler;

        public MainMenuPresenter(IMainMenuView view, ISceneLoader sceneLoader, IApplicationHandler applicationHandler)
        {
            _view = view;
            _view.Setup();
            _view.OnPlayButtonClicked += HandlePlay;
            _view.OnQuitButtonClicked += HandleQuit;
            
            _sceneLoader = sceneLoader;
            _applicationHandler = applicationHandler;
        }

        public void Dispose()
        {
            _view.OnPlayButtonClicked -= HandlePlay;
            _view.OnQuitButtonClicked -= HandleQuit;
            _view.Dispose();
        }

        private void HandlePlay()
        {
            _sceneLoader.LoadScene("Game");
        }

        private void HandleQuit()
        {
            _applicationHandler.Quit();
        }
    }
}
