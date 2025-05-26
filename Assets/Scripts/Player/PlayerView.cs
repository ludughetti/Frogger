using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        public void UpdatePosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
