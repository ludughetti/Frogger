using System;
using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using Zones.EndZone;

namespace Editor.Tests.EditMode
{
    public class EndZonePresenterTests
    {
        private EndZoneViewMocked _mockView;
        private EndZonePresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _mockView = new EndZoneViewMocked();
            _presenter = new EndZonePresenter(_mockView);
        }

        [TearDown]
        public void Teardown()
        {
            _presenter.Dispose();
        }

        [Test]
        public void OnPlayerEntered_EventIsForwarded()
        {
            var wasCalled = false;
            _presenter.OnPlayerEntered += () => wasCalled = true;

            _mockView.TriggerEnter();

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void Dispose_UnsubscribesEvent()
        {
            var wasCalled = false;
            _presenter.OnPlayerEntered += () => wasCalled = true;

            _presenter.Dispose();
            _mockView.TriggerEnter();

            Assert.IsFalse(wasCalled);
        }
    }
}