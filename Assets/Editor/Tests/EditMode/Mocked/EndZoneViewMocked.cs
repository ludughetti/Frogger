using System;
using Zones.EndZone;

namespace Editor.Tests.EditMode.Mocked
{
    class EndZoneViewMocked : IEndZoneView
    {
        public event Action OnPlayerEntered;

        public void TriggerEnter() => OnPlayerEntered?.Invoke();
    }
}