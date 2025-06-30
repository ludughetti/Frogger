using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}