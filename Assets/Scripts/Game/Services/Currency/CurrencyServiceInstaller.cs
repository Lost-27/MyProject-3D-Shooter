using Zenject;

namespace AlienArenas.Game.Services.Currency
{
    public class CurrencyServiceInstaller : Installer<CurrencyServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICurrencyService>().To<CurrencyService>().AsSingle();
        }
    }
}