using System;
using UnityEngine;

namespace Zones.EndZone
{
    public class EndZoneView : MonoBehaviour, IEndZoneView
    {
        public event Action OnPlayerEntered;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            OnPlayerEntered?.Invoke();
        }
    }
}
