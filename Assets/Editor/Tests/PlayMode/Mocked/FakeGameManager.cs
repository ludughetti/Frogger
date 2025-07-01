using System.Collections.Generic;
using Cars.Spawner;
using Core;
using TMPro;
using UnityEngine;
using Utils;
using Zones.EndZone;
using Zones.StartZone;

namespace Editor.Tests.PlayMode.Mocked
{
    public class FakeGameManager : GameManager
    {
        // Set dependencies for testing only
        public void SetDependencies(Transform testBoundTopLeft, Transform testBoundBottomRight,
            Transform testPlayerSpawnPosition)
        {
            inputHandler = gameObject.AddComponent<InputHandler>();
            cameraHandler = gameObject.AddComponent<CameraHandler>();
            boundTopLeft = testBoundTopLeft;
            boundBottomRight = testBoundBottomRight;
            playerSpawnPosition = testPlayerSpawnPosition;
            startZoneView = gameObject.AddComponent<StartZoneView>();
            audioManager = gameObject.AddComponent<FakeAudioManager>();
            
            InitializeCarSpawner();
            InitializeTimerView();
            InitializePlayerView();
            InitializeEndZoneView();
        }

        // Helpers to check if the Presenters were initialized correctly in Initialize()
        public bool WasCarSpawnerPresenterSet()
        {
            return CarSpawnerPresenters.Count > 0;
        }

        public bool WasEndZonePresenterSet()
        {
            return EndZonePresenter != null;
        }

        public bool WasPlayerPresenterSet()
        {
            return PlayerPresenter != null;
        }

        public bool WasStartZonePresenterSet()
        {
            return StartZonePresenter != null;
        }

        public bool WasTimerPresenterSet()
        {
            return TimerPresenter != null;
        }

        public bool WasSceneLoaderSet()
        {
            return SceneLoader != null;
        }

        public void MockSceneLoader(ISceneLoader sceneLoader)
        {
            SceneLoader = sceneLoader;
        }

        private void InitializeCarSpawner()
        {
            // Setup CarSpawnerView and its data
            var carSpawner = gameObject.AddComponent<CarSpawnerView>();
            var container = new GameObject("SpawnContainer").transform;
            var data = ScriptableObject.CreateInstance<CarSpawnerData>();
            var testPrefab = GameObject.CreatePrimitive(PrimitiveType.Cube);
            data.SetData(testPrefab, 2f, 10f);
            carSpawner.SetData(data, container);

            carSpawnerView = new List<CarSpawnerView> { carSpawner };
        }

        private void InitializeTimerView()
        {
            // In your test setup
            var timerGo = new GameObject("TimerViewMocked");
            var timerViewMocked = timerGo.AddComponent<TimerViewMocked>();

            // Create a dummy Text component to avoid null refs
            var textGo = new GameObject("TimerText");
            var uiText = textGo.AddComponent<TextMeshProUGUI>();

            timerViewMocked.SetTimerText(uiText);

            // Assign this TimerViewMocked to your FakeGameManager
            timerView = timerViewMocked;
        }
        
        private void InitializePlayerView()
        {
            // Add PlayerView component
            var playerGo = new GameObject("PlayerViewMocked");
            var playerViewMocked = playerGo.AddComponent<PlayerViewMocked>();
            playerView = playerViewMocked;

            // SpriteRenderer setup
            var spriteGo = new GameObject("SpriteRenderer");
            spriteGo.transform.parent = playerView.transform;
            var spriteRenderer = spriteGo.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = CreateTestSprite();
            playerViewMocked.SetSpriteRenderer(spriteRenderer);

            // Animator setup
            var animator = playerGo.AddComponent<Animator>();
            animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("TestAnimatorController");
            playerViewMocked.SetAnimator(animator);
            
            // Audio Source setup
            var audioSource = playerGo.AddComponent<AudioSource>();
            playerViewMocked.SetWalkingAudioSource(audioSource);
        }
        
        private static Sprite CreateTestSprite()
        {
            // Create a tiny 1x1 texture
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.white);
            tex.Apply();

            // Create a sprite from the texture
            return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
        }

        private void InitializeEndZoneView()
        {
            // Create a dummy GameObject to hold the EndZoneView
            var endZoneGo = new GameObject("EndZoneView");
            endZoneView = endZoneGo.AddComponent<EndZoneView>();

            endZoneGo.transform.position = Vector3.zero;
        }

    }
}