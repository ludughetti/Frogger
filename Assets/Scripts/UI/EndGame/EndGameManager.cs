using UnityEngine;
using Utils;

namespace UI.EndGame
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private EndGameView endGameView;

        private EndGamePresenter _presenter;

        private void Start()
        {
            // Get the result message from a global state or static class
            var resultMessage = EndGameState.ResultMessage ?? "Unknown Result";
            var resultPlayerWon = EndGameState.PlayerWon;

            var model = new EndGameModel(resultMessage, resultPlayerWon);
            _presenter = new EndGamePresenter(model, endGameView);
        }

        private void OnDestroy()
        {
            _presenter?.Dispose();
        }
    }
}
