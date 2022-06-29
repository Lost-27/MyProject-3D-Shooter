using AlienArenas.Game.Managers;
using TMPro;
using UnityEngine;

namespace AlienArenas.Game.UI
{
    public class TotalAliensBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dynamicTotalAliensLabel;
        [SerializeField] private GameManager_V2 _gameManager;

        private void Update()
        {
            _dynamicTotalAliensLabel.text = _gameManager.TotalAliens.ToString();
        }
    }
}
