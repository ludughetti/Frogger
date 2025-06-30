using System;

namespace Zones.EndZone
{
    public interface IEndZoneView
    {
        event Action OnPlayerEntered;
    }
}