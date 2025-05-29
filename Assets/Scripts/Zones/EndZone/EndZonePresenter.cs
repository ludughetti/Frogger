using System;
using UnityEngine;

namespace Zones.EndZone
{
    public class EndZonePresenter
    {
        private EndZoneView _view;

        public Action OnPlayerEntered;
        
        public EndZonePresenter(EndZoneView view)
        {
            _view = view;
            _view.OnPlayerEntered += HandlePlayerEntered;
        }

        private void HandlePlayerEntered()
        {
            OnPlayerEntered?.Invoke();
        }

        public void Dispose()
        {
            _view.OnPlayerEntered -= HandlePlayerEntered;
        }
    }
}
