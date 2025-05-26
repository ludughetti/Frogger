using Player;
using UnityEngine;
using Utils;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private InputHandler inputHandler;
    
        [Header("Player Settings")]
        [SerializeField] private float moveSpeed = 5f;
    
        private PlayerPresenter _playerPresenter;

        private void Start()
        {
            _playerPresenter = new PlayerPresenter(new PlayerModel(), playerView, inputHandler, moveSpeed);
        }

        private void Update()
        {
            _playerPresenter.Update();
        }
    }
}
