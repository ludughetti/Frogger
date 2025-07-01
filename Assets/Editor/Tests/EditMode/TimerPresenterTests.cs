using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using Timer;

namespace Editor.Tests.EditMode
{
    public class TimerPresenterTests
    {
        private TimerModel _model;
        private TimerPresenter _presenter;
        private TimerViewMocked _view;

        [SetUp]
        public void Setup()
        {
            _model = new TimerModel(10f);
            _view = new TimerViewMocked();
            _presenter = new TimerPresenter(_model, _view);
        }

        [Test]
        public void Presenter_Initializes_ViewWithStartTime()
        {
            Assert.AreEqual(10f, _view.LastTimeUpdated);
        }

        [Test]
        public void Presenter_Update_DecrementsRemainingTime()
        {
            _presenter.Update(1f);

            Assert.AreEqual(9f, _model.RemainingTime);
            Assert.AreEqual(9f, _view.LastTimeUpdated);
        }

        [Test]
        public void Presenter_Update_ClampsToZeroAndInvokesCallback()
        {
            var callbackCalled = false;
            _presenter.OnTimerEnd += () => callbackCalled = true;

            _presenter.Update(15f);

            Assert.AreEqual(0f, _model.RemainingTime);
            Assert.AreEqual(0f, _view.LastTimeUpdated);
            Assert.IsTrue(callbackCalled);
        }

        [Test]
        public void Presenter_DoesNotUpdate_WhenPaused()
        {
            _model.Toggle(false);

            _presenter.Update(1f);

            Assert.AreEqual(10f, _model.RemainingTime);
            Assert.AreEqual(10f, _view.LastTimeUpdated);
        }
    }
}