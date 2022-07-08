using System;
using AlienArenas.Game;
using AlienArenas.Infrastructure.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AlienArenas.MainMenu
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _quitButton;

        private ISceneLoader _sceneLoader;

        [Inject]
        public void Construct(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }


        private void Awake()
        {
            _menuButton.onClick.AddListener(MenuButtonClicked);
            _quitButton.onClick.AddListener(QuitButtonClicked);
            SoundManager.Instance.StopSoundBG();
        }


        private void MenuButtonClicked()
        {
            _sceneLoader.LoadSceneAsync(SceneName.Menu);
        }


        private void QuitButtonClicked()
        {
            _sceneLoader.Quit();
        }
    }
}