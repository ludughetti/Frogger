using UnityEngine;

namespace Zones.StartZone
{
    public class StartZoneModel
    {
        public Vector2 SpawnPosition { get; private set; }

        public StartZoneModel(Vector2 position)
        {
            SpawnPosition = position;
        }
    }
}
