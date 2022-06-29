using System;

namespace AlienArenas.Game.Services.Currency
{
    public interface ICurrencyService
    {
        event Action<Currency, int> OnCurrencyChanged;

        void Add(Currency currency, int count);
        int Count(Currency currency);
    }
}