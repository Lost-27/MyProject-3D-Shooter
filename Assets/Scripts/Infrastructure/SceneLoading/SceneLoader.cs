using Cysharp.Threading.Tasks;
using Zenject;

namespace AlienArenas.Infrastructure.SceneLoading
{
    public class SceneLoader : ISceneLoader
    {
        private readonly ZenjectSceneLoader _sceneLoader;

        public SceneLoader(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public async UniTask LoadSceneAsync(string sceneName) =>
            await _sceneLoader.LoadSceneAsync(sceneName).ToUniTask();


        public void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}