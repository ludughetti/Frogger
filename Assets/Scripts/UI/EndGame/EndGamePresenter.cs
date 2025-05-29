using UnityEngine.SceneManagement;

namespace UI.EndGame
{
    public class EndGamePresenter
    {
        private readonly EndGameModel _model;
        private readonly EndGameView _view;

        public EndGamePresenter(EndGameModel model, EndGameView view)
        {
            _model = model;
            _view = view;

            _view.Setup(_model.ResultMessage);
            _view.OnReturnToMainMenu += HandleMainMenu;
        }

        private void HandleMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void Dispose()
        {
            _view.OnReturnToMainMenu -= HandleMainMenu;
            _view.Dispose();
        }
    }
}
