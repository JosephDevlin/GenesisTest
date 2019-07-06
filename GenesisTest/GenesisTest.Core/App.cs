using GenesisTest.Core.Api;
using GenesisTest.Core.Services;
using GenesisTest.Core.ViewModels;
using MvvmCross;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using Refit;

namespace GenesisTest.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            Mvx.IoCProvider.LazyConstructAndRegisterSingleton(() => RestService.For<IGithubRepositories>("https://api.github.com"));
            Mvx.IoCProvider.RegisterType<IRepositoryService, RepositoryService>();

            RegisterAppStart<RepositoriesViewModel>();
        }
    }
}
