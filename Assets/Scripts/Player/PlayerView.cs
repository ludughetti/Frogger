using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private AudioSource walkingAudioSource;

        public void SetAnimationValues(Vector2 direction, bool isMoving)
        {
            animator.SetBool("IsMoving", isMoving);
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
        }

        public float GetPlayerHalfHeight()
        {
            return spriteRenderer.bounds.size.y / 2.0f;
        }
        
        public float GetPlayerHalfWidth()
        {
            return spriteRenderer.bounds.size.x / 2.0f;
        }
        
        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }

        public void HandlePlayerWalkSFX(bool isMoving)
        {
            if (isMoving)
                PlayWalkingSound();
            else
                StopWalkingSound();
        }
        
        private void PlayWalkingSound()
        {
            if (!walkingAudioSource.isPlaying)
                walkingAudioSource.Play();
        }

        private void StopWalkingSound()
        {
            if (walkingAudioSource.isPlaying)
                walkingAudioSource.Stop();
        }
    }
}
