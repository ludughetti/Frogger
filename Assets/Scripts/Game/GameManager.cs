using System;
using System.Collections.Generic;
using Cars.Car;
using Cars.Spawner;
using Player;
using Timer;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private List<CarSpawnerView> carSpawnerView;
        
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
    
        private List<CarSpawnerPresenter> _carSpawnerPresenters = new ();
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
            
            // Setup car spawners
            foreach (var spawnerView in carSpawnerView)
            {
                var carSpawnerModel = new CarSpawnerModel(spawnerView.SpawnerData, spawnerView.SpawnContainer,
                    spawnerView.transform.position, spawnerView.transform.right);
                var carSpawnerPresenter = new CarSpawnerPresenter(carSpawnerModel, spawnerView);
                carSpawnerPresenter.OnPlayerCollision += HandlePlayerCollision;
                
                _carSpawnerPresenters.Add(carSpawnerPresenter);
            }
            
            // Setup timer
            _timerPresenter = new TimerPresenter(new TimerModel(levelStartTime), timerView);
            _timerPresenter.OnTimerEnd += HandleLose;
            
            // Setup player
            var playerModel = new PlayerModel(_startZonePresenter.GetPlayerSpawnPosition(), boundTopLeft.position, boundBottomRight.position, 
                playerView.GetPlayerHalfWidth(), playerView.GetPlayerHalfHeight());
            _playerPresenter = new PlayerPresenter(playerModel, playerView, inputHandler, moveSpeed);
            
            // Setup camera
            cameraHandler.Setup(boundTopLeft.position, boundBottomRight.position, moveSpeed);
            _playerPresenter.OnPlayerMoved += cameraHandler.Follow;
        }

        private void Update()
        {
            _timerPresenter.Update(Time.deltaTime);
            _playerPresenter.Update();
            
            foreach (var carSpawnerPresenter in _carSpawnerPresenters)
                carSpawnerPresenter.Update();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void HandleWin()
        {
            EndGameState.ResultMessage = "You win!";
            SceneManager.LoadScene("EndGame");
        }

        private void HandleLose()
        {
            EndGameState.ResultMessage = "You lose!";
            SceneManager.LoadScene("EndGame");
        }

        private void Dispose()
        {
            Debug.Log("Disposing game manager...");
            
            // Unsuscribe
            _endZonePresenter.OnPlayerEntered -= HandleWin;
            _timerPresenter.OnTimerEnd -= HandleLose;
            _playerPresenter.OnPlayerMoved -= cameraHandler.Follow;
            
            foreach (var carSpawnerPresenter in _carSpawnerPresenters)
                carSpawnerPresenter.OnPlayerCollision -= HandlePlayerCollision;
            
            // Dispose
            _endZonePresenter.Dispose();
            _playerPresenter.Dispose(inputHandler);
            
            Debug.Log("Game manager was disposed.");
        }

        private void HandlePlayerCollision(CarSpawnerPresenter spawnerPresenter, CarPresenter presenter)
        {
            // Destroy car
            spawnerPresenter.HandleCarDestruction(presenter);
            
            // Respawn player
            _playerPresenter.RespawnPlayer();
            _startZonePresenter.PlayOnPlayerRespawn();
        }
    }
}
