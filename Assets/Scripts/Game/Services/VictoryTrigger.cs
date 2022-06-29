using AlienArenas.Game.UI;
using AlienArenas.Game.Utility.Constants;
using UnityEngine;

namespace AlienArenas.Game.Services
{
    public class VictoryTrigger : MonoBehaviour
    {
        [SerializeField] private LevelVictoryScreen _victoryScreen;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Player))
                return;

            Invoke(nameof(ShowVictoryScreen), 1.4f);
        }

        private void ShowVictoryScreen()
        {
            _victoryScreen.SetActive(true);
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.victory);
        }
    }
}
