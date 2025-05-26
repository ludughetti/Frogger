using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Zones.EndZone;
using Zones.StartZone;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private StartZoneView startZoneView;
        [SerializeField] private EndZoneView endZoneView;
        [SerializeField] private InputHandler inputHandler;

        [Header("Core Settings")] 
        [SerializeField] private Transform boundTopLeft;
        [SerializeField] private Transform boundBottomRight;
    
        [Header("Player Settings")]
        [SerializeField] private float moveSpeed = 5f;
        
        [Header("Zones Settings")]
        [SerializeField] private Transform spawnPosition;
    
        private PlayerPresenter _playerPresenter;
        private StartZonePresenter _startZonePresenter;
        private EndZonePresenter _endZonePresenter;

        private void Start()
        {
            // Setup zones
            _startZonePresenter = new StartZonePresenter(new StartZoneModel(spawnPosition.position), startZoneView);
            
            _endZonePresenter = new EndZonePresenter(endZoneView);
            _endZonePresenter.OnPlayerEntered += HandleWin;
            
            // Setup player
            var playerModel = new PlayerModel(Vector2.zero, boundTopLeft.position, boundBottomRight.position);
            _playerPresenter = new PlayerPresenter(playerModel, playerView, inputHandler, moveSpeed);
            _playerPresenter.SetSpawnPosition(_startZonePresenter.GetPlayerSpawnPosition());
        }

        private void Update()
        {
            _playerPresenter.Update();
        }

        [ContextMenu("Respawn Player")]
        private void HandlePlayerRespawn()
        {
            Debug.Log("Player Respawn");
            _playerPresenter.SetSpawnPosition(_startZonePresenter.GetPlayerSpawnPosition());
        }

        private void HandleWin()
        {
            Debug.Log("Win");
        }
    }
}
