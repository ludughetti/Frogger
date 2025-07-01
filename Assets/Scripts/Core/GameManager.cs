using System.Collections.Generic;
using Cars.Car;
using Cars.Spawner;
using Cars.Spawner.Factory.FactoryImpl;
using Player;
using Timer;
using UnityEngine;
using Utils;
using Zones.EndZone;
using Zones.StartZone;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Core Settings")] 
        [SerializeField] protected InputHandler inputHandler;
        [SerializeField] protected CameraHandler cameraHandler;
        [SerializeField] protected Transform boundTopLeft;
        [SerializeField] protected Transform boundBottomRight;
    
        [Header("Cars Spawner Settings")]
        [SerializeField] protected List<CarSpawnerView> carSpawnerView;
        
        [Header("Player Settings")]
        [SerializeField] protected PlayerView playerView;
        [SerializeField] protected float moveSpeed = 5f;
        
        [Header("Timer Settings")]
        [SerializeField] protected TimerView timerView;
        [SerializeField] protected float levelStartTime = 60f;
        
        [Header("Zones Settings")]
        [SerializeField] protected StartZoneView startZoneView;
        [SerializeField] protected EndZoneView endZoneView;
        [SerializeField] protected Transform playerSpawnPosition;
        
        [Header("Sound Settings")]
        [SerializeField] protected AudioManager audioManager;
    
        protected List<CarSpawnerPresenter> CarSpawnerPresenters = new ();
        protected EndZonePresenter EndZonePresenter;
        protected PlayerPresenter PlayerPresenter;
        protected StartZonePresenter StartZonePresenter;
        protected TimerPresenter TimerPresenter;
        protected ISceneLoader SceneLoader;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            // Setup zones
            StartZonePresenter = new StartZonePresenter(new StartZoneModel(playerSpawnPosition.position), startZoneView);
            
            EndZonePresenter = new EndZonePresenter(endZoneView);
            EndZonePresenter.OnPlayerEntered += HandleWin;
            
            // Setup car spawners
            foreach (var spawnerView in carSpawnerView)
            {
                var carSpawnerModel = new CarSpawnerModel(spawnerView.SpawnerData, spawnerView.SpawnContainer,
                    spawnerView.transform.position, spawnerView.transform.right);
                var carFactory = new DefaultCarFactory(carSpawnerModel.CarPrefab);
                var carSpawnerPresenter = new CarSpawnerPresenter(carSpawnerModel, carFactory);
                carSpawnerPresenter.OnPlayerCollision += HandlePlayerCollision;
                
                CarSpawnerPresenters.Add(carSpawnerPresenter);
            }
            
            // Setup timer
            TimerPresenter = new TimerPresenter(new TimerModel(levelStartTime), timerView);
            TimerPresenter.OnTimerEnd += HandleLose;
            
            // Setup player
            var playerModel = new PlayerModel(StartZonePresenter.GetPlayerSpawnPosition(), boundTopLeft.position, boundBottomRight.position, 
                playerView.GetPlayerHalfWidth(), playerView.GetPlayerHalfHeight());
            PlayerPresenter = new PlayerPresenter(playerModel, playerView, inputHandler, moveSpeed);
            
            // Setup camera
            cameraHandler.Setup(boundTopLeft.position, boundBottomRight.position, moveSpeed);
            PlayerPresenter.OnPlayerMoved += cameraHandler.Follow;
            
            // Setup scene loader
            SceneLoader = new SceneLoader();
        }

        private void Update()
        {
            HandleUpdate();
        }

        public void HandleUpdate()
        {
            var deltaTime = Time.deltaTime;
            TimerPresenter.Update(deltaTime);
            PlayerPresenter.Update(deltaTime);
            
            foreach (var carSpawnerPresenter in CarSpawnerPresenters)
                carSpawnerPresenter.Update(deltaTime);
        }

        private void OnDestroy()
        {
            Dispose();
        }

        private void HandleWin()
        {
            EndGameState.ResultMessage = "You win!";
            EndGameState.PlayerWon = true;
            SceneLoader.LoadScene("EndGame");
        }

        private void HandleLose()
        {
            EndGameState.ResultMessage = "You lose!";
            EndGameState.PlayerWon = false;
            SceneLoader.LoadScene("EndGame");
        }

        private void Dispose()
        {
            Debug.Log("Disposing game manager...");
            
            // Unsuscribe if non null
            if (EndZonePresenter != null)
                EndZonePresenter.OnPlayerEntered -= HandleWin;
            if (TimerPresenter != null)
                TimerPresenter.OnTimerEnd -= HandleLose;
            if (PlayerPresenter != null && cameraHandler != null)
                PlayerPresenter.OnPlayerMoved -= cameraHandler.Follow;
            
            foreach (var carSpawnerPresenter in CarSpawnerPresenters)
                carSpawnerPresenter.OnPlayerCollision -= HandlePlayerCollision;
            
            // Dispose
            EndZonePresenter?.Dispose();
            PlayerPresenter?.Dispose(inputHandler);
            
            Debug.Log("Game manager was disposed.");
        }

        private void HandlePlayerCollision(CarSpawnerPresenter spawnerPresenter, CarPresenter presenter)
        {
            // Play sounds
            audioManager.PlayCrashSfx();
            
            // Destroy car
            spawnerPresenter.HandleCarDestruction(presenter);
            
            // Respawn player
            PlayerPresenter.RespawnPlayer();
        }
    }
}
