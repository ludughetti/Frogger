using UnityEngine;

namespace Zones.StartZone
{
    public class StartZonePresenter
    {
        private readonly IStartZoneView _view;
        private readonly StartZoneModel _model;

        public StartZonePresenter(StartZoneModel model, IStartZoneView view)
        {
            _view = view;
            _model = model;
            
            // Move view to the Spawn point so it's dynamically handled
            _view.SetPosition(_model.SpawnPosition);
        }

        public Vector2 GetPlayerSpawnPosition()
        {
            return _model.SpawnPosition;
        }
    }
}
