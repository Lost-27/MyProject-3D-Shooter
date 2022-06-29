using System;
using System.Collections.Generic;

namespace AlienArenas.Game.Services.Currency
{
    public class CurrencyService : ICurrencyService
    {
        private Dictionary<Currency, int> _currencies = new Dictionary<Currency, int>();

        public event Action<Currency, int> OnCurrencyChanged;

        public void Add(Currency currency, int count)
        {
            _currencies[currency] += count;
            OnCurrencyChanged?.Invoke(currency, Count(currency));
        }

        public int Count(Currency currency)
        {
            if (!_currencies.ContainsKey(currency))
                _currencies.Add(currency, 0);

            return _currencies[currency];
        }
    }
}