using UnityEngine;

namespace Core
{
    public class AudioManager : MonoBehaviour
    {
        [Header("Sources")]
        [SerializeField] protected AudioSource backgroundMusic;
        [SerializeField] protected AudioSource ambientSound;
        [SerializeField] protected AudioSource sfxSource1;
        [SerializeField] protected AudioSource sfxSource2;
        
        [Header("Sounds")]
        [SerializeField] protected AudioClip sfxCatCrashMeow;
        [SerializeField] protected AudioClip sfxCarCrash;

        private void Start()
        {
            PlaySoundsOnStart();
        }

        public void PlaySoundsOnStart()
        {
            if (backgroundMusic != null && !backgroundMusic.isPlaying)
                PlayOnSource(backgroundMusic);

            if (ambientSound != null && !ambientSound.isPlaying)
                PlayOnSource(ambientSound);
        }

        public void PlayCrashSfx()
        {
            PlayOneShotOnSource(sfxSource1, sfxCatCrashMeow);
            PlayOneShotOnSource(sfxSource2, sfxCarCrash);
        }
        
        protected virtual void PlayOnSource(AudioSource source)
        {
            source.Play();
        }
        
        protected virtual void PlayOneShotOnSource(AudioSource source, AudioClip clip)
        {
            source.PlayOneShot(clip);
        }
    }
}
