using Cysharp.Threading.Tasks;

namespace AlienArenas.Infrastructure.SceneLoading
{
    public interface ISceneLoader
    {
        UniTask LoadSceneAsync(string sceneName);
        void Quit();
    }
}