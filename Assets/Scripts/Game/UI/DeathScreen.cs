using System;
using UnityEngine;
using UnityEngine.UI;

namespace AlienArenas.Game.UI
{
    public class DeathScreen : MonoBehaviour
    {
        #region Variables

        [SerializeField] private GameObject _innerContainer;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;

        #endregion


        #region Events

        public static event Action OnRestartButtonClicked;
        public static event Action OnMenuButtonClicked;

        #endregion


        #region Unity lifecycle

        private void Awake()
        {
            SetActive(false);

            _restartButton.onClick.AddListener(() => OnRestartButtonClicked?.Invoke());
            _menuButton.onClick.AddListener(() => OnMenuButtonClicked?.Invoke());
        }

        #endregion


        #region Public methods

        public void SetActive(bool isActive) =>
            _innerContainer.SetActive(isActive);

        #endregion
    }
}