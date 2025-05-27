using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

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
    }
}
