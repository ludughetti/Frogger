using Cars.Spawner;
using Player;
using Timer;
using UnityEngine;
using Utils;
using Zones.EndZone;
using Zones.StartZone;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        [Header("Core Settings")] 
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private CameraHandler cameraHandler;
        [SerializeField] private Transform boundTopLeft;
        [SerializeField] private Transform boundBottomRight;
        
        [Header("Cars Settings")]
        [SerializeField] private GameObject carPrefab;
    
        [Header("Cars Spawner Settings")]
        [SerializeField] private CarSpawnerView carSpawnerView;
        [SerializeField] private Transform spawnContainer;
        [SerializeField] private Transform carSpawnPosition;
        [SerializeField] private float carSpawnRate;
        [SerializeField] private float carSpeed;
        
        [Header("Player Settings")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private float moveSpeed = 5f;
        
        [Header("Timer Settings")]
        [SerializeField] private TimerView timerView;
        [SerializeField] private float levelStartTime = 60f;
        
        [Header("Zones Settings")]
        [SerializeField] private StartZoneView startZoneView;
        [SerializeField] private EndZoneView endZoneView;
        [SerializeField] private Transform playerSpawnPosition;
    
        private CarSpawnerPresenter _carSpawnerPresenter;
        private EndZonePresenter _endZonePresenter;
        private PlayerPresenter _playerPresenter;
        private StartZonePresenter _startZonePresenter;
        private TimerPresenter _timerPresenter;

        private void Start()
        {
            // Setup zones
            _startZonePresenter = new StartZonePresenter(new StartZoneModel(playerSpawnPosition.position), startZoneView);
            
            _endZonePresenter = new EndZonePresenter(endZoneView);
            _endZonePresenter.OnPlayerEntered += HandleWin;
            
            // Setup cars
            var carSpawnerModel = new CarSpawnerModel(carPrefab, spawnContainer, carSpawnPosition.position, 
                carSpawnerView.transform.right.normalized, carSpawnRate, carSpeed);
            _carSpawnerPresenter = new CarSpawnerPresenter(carSpawnerModel, carSpawnerView);
            
            // Setup timer
            _timerPresenter = new TimerPresenter(new TimerModel(levelStartTime), timerView);
            _timerPresenter.OnTimerEnd += HandleLose;
            
            // Setup player
            var playerModel = new PlayerModel(Vector2.zero, boundTopLeft.position, boundBottomRight.position, 
                playerView.GetPlayerHalfWidth(), playerView.GetPlayerHalfHeight());
            _playerPresenter = new PlayerPresenter(playerModel, playerView, inputHandler, moveSpeed);
            _playerPresenter.SetSpawnPosition(_startZonePresenter.GetPlayerSpawnPosition());
            
            // Setup camera
            cameraHandler.Setup(boundTopLeft.position, boundBottomRight.position, moveSpeed);
            _playerPresenter.OnPlayerMoved += cameraHandler.Follow;
        }

        private void Update()
        {
            _playerPresenter.Update();
            _carSpawnerPresenter.Update();
            _timerPresenter.Update(Time.deltaTime);
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

        private void HandleLose()
        {
            Debug.Log("Lose");
        }

        private void Dispose()
        {
            _endZonePresenter.OnPlayerEntered -= HandleWin;
            _timerPresenter.OnTimerEnd -= HandleLose;
            _playerPresenter.OnPlayerMoved -= cameraHandler.Follow;
        }
    }
}
