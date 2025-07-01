using System;
using UnityEngine;

namespace Timer
{
    public class TimerPresenter
    {
        private TimerModel _model;
        private ITimerView _view;

        public Action OnTimerEnd;
        
        public TimerPresenter(TimerModel model, ITimerView view)
        {
            _model = model;
            _view = view;
            
            _view.UpdateUI(_model.RemainingTime);
        }

        public virtual void Update(float deltaTime)
        {
            // If timer is not running (e.g. paused), then early return
            if (!_model.IsRunning) 
                return;
            
            var timeUpdated = Mathf.Clamp(_model.RemainingTime - deltaTime, 0, _model.StartTime);
            _model.UpdateRemainingTime(timeUpdated);
            _view.UpdateUI(_model.RemainingTime);

            // If timer didn't run out, then early return
            if (_model.RemainingTime > 0) 
                return;
            
            OnTimerEnd?.Invoke();
        }
    }
}
