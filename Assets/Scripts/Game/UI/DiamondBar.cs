using AlienArenas.Game.Services.Currency;
using TMPro;
using UnityEngine;
using Zenject;

namespace AlienArenas.Game.UI
{
    public class DiamondBar : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _dynamicDiamondLabel;

        private ICurrencyService _currencyService;

        [Inject]
        public void Construct(ICurrencyService currencyService)
        {            
            _currencyService = currencyService;
        }


        private void Start()
        {
            _currencyService.OnCurrencyChanged += UpdateScoreLabel;
            UpdateScoreLabel(Currency.Diamonds, _currencyService.Count(Currency.Diamonds));            
        }


        private void OnDestroy()
        {
            _currencyService.OnCurrencyChanged -= UpdateScoreLabel;            
        }


        private void UpdateScoreLabel(Currency currency, int count) => 
            _dynamicDiamondLabel.text = count.ToString();
    }
}
