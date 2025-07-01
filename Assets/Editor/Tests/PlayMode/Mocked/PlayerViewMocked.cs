using Player;
using UnityEngine;

namespace Editor.Tests.PlayMode.Mocked
{
    public class PlayerViewMocked : PlayerView
    {
        public void SetSpriteRenderer(SpriteRenderer testRenderer)
        {
            spriteRenderer = testRenderer;
        }
        
        public void SetAnimator(Animator anim)
        {
            animator = anim;
        }

        public void SetWalkingAudioSource(AudioSource source)
        {
            walkingAudioSource = source;
        }
    }
}