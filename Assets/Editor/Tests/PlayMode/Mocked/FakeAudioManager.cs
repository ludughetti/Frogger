using Core;
using UnityEngine;

namespace Editor.Tests.PlayMode.Mocked
{
    public class FakeAudioManager : AudioManager
    {
        public bool BackgroundMusicPlayed { get; private set; }
        public bool AmbientSoundPlayed { get; private set; }
        public bool CatCrashPlayed { get; private set; }
        public bool CarCrashPlayed { get; private set; }

        public void SetData(AudioSource testBackgroundMusic, AudioSource testAmbientSound, AudioSource testSfxSource1,
            AudioSource testSfxSource2, AudioClip testSfxCatCrashMeow, AudioClip testSfxCarCrash)
        {
            backgroundMusic = testBackgroundMusic;
            ambientSound = testAmbientSound;
            sfxSource1 = testSfxSource1;
            sfxSource2 = testSfxSource2;
            sfxCatCrashMeow = testSfxCatCrashMeow;
            sfxCarCrash = testSfxCarCrash;
        }

        protected override void PlayOnSource(AudioSource source)
        {
            if (source == backgroundMusic)
                BackgroundMusicPlayed = true;
            else if (source == ambientSound)
                AmbientSoundPlayed = true;
        }

        protected override void PlayOneShotOnSource(AudioSource source, AudioClip clip)
        {
            if (source == sfxSource1 && clip == sfxCatCrashMeow)
                CatCrashPlayed = true;
            else if (source == sfxSource2 && clip == sfxCarCrash)
                CarCrashPlayed = true;
        }
    }
}