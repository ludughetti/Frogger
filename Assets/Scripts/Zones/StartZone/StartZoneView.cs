using UnityEngine;

namespace Zones.StartZone
{
    public class StartZoneView : MonoBehaviour
    {
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
        
        public void PlayOnRespawn()
        {
            Debug.Log("PlayOnPlayerRespawn");
        }
    }
}
