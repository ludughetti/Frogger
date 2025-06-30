using Editor.Tests.EditMode.Mocked;
using NUnit.Framework;
using UI.MainMenu;

namespace Editor.Tests.EditMode
{
    public class MainMenuPresenterTests
    {
        private MainMenuViewMocked _mockView;
        private FakeSceneLoader _fakeSceneLoader;
        private FakeApplicationHandler _mockQuitter;
        private MainMenuPresenter _presenter;

        [SetUp]
        public void Setup()
        {
            _mockView = new MainMenuViewMocked();
            _fakeSceneLoader = new FakeSceneLoader();
            _mockQuitter = new FakeApplicationHandler();

            _presenter = new MainMenuPresenter(_mockView, _fakeSceneLoader, _mockQuitter);
        }

        [TearDown]
        public void TearDown()
        {
            _presenter.Dispose();
        }

        [Test]
        public void Setup_RegistersListeners()
        {
            Assert.IsTrue(_mockView.SetupCalled);
        }

        [Test]
        public void PlayButton_TriggersSceneLoad()
        {
            _mockView.TriggerPlay();

            Assert.IsTrue(_fakeSceneLoader.SceneLoaded);
            Assert.AreEqual("Game", _fakeSceneLoader.LoadedSceneName);
        }

        [Test]
        public void QuitButton_TriggersApplicationQuit()
        {
            _mockView.TriggerQuit();

            Assert.IsTrue(_mockQuitter.QuitCalled);
        }

        [Test]
        public void Dispose_UnsubscribesEventsAndDisposesView()
        {
            _presenter.Dispose();

            Assert.IsTrue(_mockView.DisposeCalled);
            Assert.IsFalse(_mockView.PlaySubscribed);
            Assert.IsFalse(_mockView.QuitSubscribed);
        }
    }
}
