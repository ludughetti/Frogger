using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Sources")]
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource ambientSound;
        [SerializeField] private AudioSource sfxSource1;
        [SerializeField] private AudioSource sfxSource2;
        
        [Header("Sounds")]
        [SerializeField] private AudioClip sfxCatCrashMeow;
        [SerializeField] private AudioClip sfxCarCrash;

        private void Start()
        {
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
                backgroundMusic.Play();

            if (ambientSound != null && !ambientSound.isPlaying)
                ambientSound.Play();
        }

        public void PlayCrashSfx()
        {
           sfxSource1.PlayOneShot(sfxCatCrashMeow);
           sfxSource2.PlayOneShot(sfxCarCrash);
        }
    }
}
