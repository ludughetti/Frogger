using Utils;

namespace Editor.Tests.EditMode.Mocked
{
    public class FakeSceneLoader : ISceneLoader
    {
        public bool SceneLoaded { get; private set; }
        public string LoadedSceneName { get; private set; }

        public void LoadScene(string sceneName)
        {
            SceneLoaded = true;
            LoadedSceneName = sceneName;
        }
    }
}