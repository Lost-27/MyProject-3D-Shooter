using AlienArenas.Infrastructure.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AlienArenas.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }


        private void Awake()
        {
            _playButton.onClick.AddListener(PlayButtonClicked);
            _quitButton.onClick.AddListener(QuitButtonClicked);
        }


        private void PlayButtonClicked()
        {
            _sceneLoader.LoadSceneAsync(SceneName.Level1);
        }


        private void QuitButtonClicked()
        {
            _sceneLoader.Quit();
        }
    }
}