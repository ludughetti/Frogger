using System.Collections;
using Editor.Tests.PlayMode.Mocked;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Editor.Tests.PlayMode
{
    public class AudioManagerTests
    {
        private GameObject _audioManagerGo;
        private FakeAudioManager _audioManager;

        private AudioSource _bgMusicSource;
        private AudioSource _ambientSource;
        private AudioSource _sfx1;
        private AudioSource _sfx2;

        private AudioClip _catCrashClip;
        private AudioClip _carCrashClip;

        [UnitySetUp]
        public IEnumerator SetUp()
        {
            _audioManagerGo = new GameObject("AudioManager");
            _audioManager = _audioManagerGo.AddComponent<FakeAudioManager>();

            _bgMusicSource = _audioManagerGo.AddComponent<AudioSource>();
            _ambientSource = _audioManagerGo.AddComponent<AudioSource>();
            _sfx1 = _audioManagerGo.AddComponent<AudioSource>();
            _sfx2 = _audioManagerGo.AddComponent<AudioSource>();

            _catCrashClip = AudioClip.Create("CatCrash", 44100, 1, 44100, false);
            _carCrashClip = AudioClip.Create("CarCrash", 44100, 1, 44100, false);

            _audioManager.SetData(_bgMusicSource, _ambientSource, _sfx1, _sfx2, _catCrashClip, _carCrashClip);

            yield return null;
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            Object.DestroyImmediate(_audioManagerGo);
            yield return null;
        }

        [UnityTest]
        public IEnumerator PlayCrashSfx_PlaysCorrectSfx()
        {
            _audioManager.PlayCrashSfx();
            yield return null;

            Assert.IsTrue(_audioManager.CatCrashPlayed, "Cat crash SFX was not played.");
            Assert.IsTrue(_audioManager.CarCrashPlayed, "Car crash SFX was not played.");
        }

        [UnityTest]
        public IEnumerator PlaySoundsOnStart_PlaysBackgroundAndAmbientMusic()
        {
            _audioManager.PlaySoundsOnStart();
            yield return null;

            Assert.IsTrue(_audioManager.BackgroundMusicPlayed, "Background music was not played.");
            Assert.IsTrue(_audioManager.AmbientSoundPlayed, "Ambient sound was not played.");
        }
    }
}
