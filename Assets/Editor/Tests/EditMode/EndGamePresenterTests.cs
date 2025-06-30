using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using UI.EndGame;

namespace Editor.Tests.EditMode
{
    public class EndGamePresenterTests
    {
        private EndGameModel _model;
        private EndGameViewMocked _mockView;
        private FakeSceneLoader _mockSceneLoader;
        private EndGamePresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _model = new EndGameModel("You Win!", true);
            _mockView = new EndGameViewMocked();
            _mockSceneLoader = new FakeSceneLoader();

            _presenter = new EndGamePresenter(_model, _mockView, _mockSceneLoader);
        }

        [TearDown]
        public void TearDown()
        {
            _presenter.Dispose();
        }

        [Test]
        public void Setup_CallsViewSetupWithModelData()
        {
            Assert.AreEqual(_model.ResultMessage, _mockView.SetupMessage);
            Assert.AreEqual(_model.PlayerWon, _mockView.SetupPlayerWon);
        }

        [Test]
        public void ClickingMainMenuButton_TriggersSceneLoad()
        {
            _mockView.TriggerReturnToMainMenu();

            Assert.IsTrue(_mockSceneLoader.SceneLoaded);
            Assert.AreEqual("MainMenu", _mockSceneLoader.LoadedSceneName);
        }

        [Test]
        public void Dispose_UnsubscribesEventAndDisposesView()
        {
            _presenter.Dispose();

            Assert.IsTrue(_mockView.DisposeCalled);
            Assert.IsFalse(_mockView.OnReturnToMainMenuSubscribed);
        }
    }
}