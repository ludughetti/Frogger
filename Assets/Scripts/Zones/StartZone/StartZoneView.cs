using UnityEngine;

namespace Zones.StartZone
{
    public class StartZoneView : MonoBehaviour, IStartZoneView
    {
        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }
    }
}
