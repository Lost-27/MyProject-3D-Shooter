using AlienArenas.Infrastructure.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AlienArenas.Game.UI
{
    public class DeathScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _innerContainer;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }


        private void Awake()
        {
            SetActive(false);

            _restartButton.onClick.AddListener(RestartButtonClicked);
            _menuButton.onClick.AddListener(MenuButtonClicked);
        }
        

        public void SetActive(bool isActive) =>
            _innerContainer.SetActive(isActive);
        

        private void MenuButtonClicked()
        {
            SoundManager.Instance.StopSoundBG();
            _sceneLoader.LoadSceneAsync(SceneName.Menu);
        }
        

        private void RestartButtonClicked()
        {
            _sceneLoader.LoadSceneAsync(SceneName.Level1);
        }
    }
}