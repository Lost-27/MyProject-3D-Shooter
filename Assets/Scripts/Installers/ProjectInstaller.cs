using AlienArenas.Infrastructure.SceneLoading;
using Zenject;

namespace AlienArenas.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SceneLoaderInstaller.Install(Container);
        }
    }
}