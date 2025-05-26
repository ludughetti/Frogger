using UnityEngine;

namespace Zones.StartZone
{
    public class StartZonePresenter
    {
        private StartZoneView _view;
        private StartZoneModel _model;

        public StartZonePresenter(StartZoneModel model, StartZoneView view)
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

        public void PlayOnPlayerRespawn()
        {
            _view.PlayOnRespawn();
        }
    }
}
