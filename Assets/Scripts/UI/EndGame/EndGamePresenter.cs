using UnityEngine.SceneManagement;
using Utils;

namespace UI.EndGame
{
    public class EndGamePresenter
    {
        private readonly EndGameModel _model;
        private readonly IEndGameView _view;
        private readonly ISceneLoader _sceneLoader;

        public EndGamePresenter(EndGameModel model, IEndGameView view, ISceneLoader sceneLoader)
        {
            _model = model;
            _view = view;

            _view.Setup(_model.ResultMessage, _model.PlayerWon);
            _view.OnReturnToMainMenu += HandleMainMenu;
            
            _sceneLoader = sceneLoader;
        }

        private void HandleMainMenu()
        {
            _sceneLoader.LoadScene("MainMenu");
        }

        public void Dispose()
        {
            _view.OnReturnToMainMenu -= HandleMainMenu;
            _view.Dispose();
        }
    }
}
