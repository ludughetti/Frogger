using UnityEngine;
using Zones.StartZone;

namespace Editor.Tests.EditMode.Mocked
{
    public class StartZoneViewMocked : IStartZoneView
    {
        public Vector2 LastSetPosition;

        public void SetPosition(Vector2 position)
        {
            LastSetPosition = position;
        }
    }
}