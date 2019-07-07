using GenesisTest.Core.Models;
using GenesisTest.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;

namespace GenesisTest.Core.ViewModels
{
    public class RepositoriesViewModel : MvxViewModel
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IMvxNavigationService _navigationService;
        private MvxNotifyTask loadRepositoriesTask;
        private int _pageNumber = 1;

        public IMvxCommand LoadRepositoriesCommand { get; private set; }
        public IMvxCommand RefreshRepositoriesCommand { get; private set; }
        public IMvxCommand RepositorySelectedCommand { get; private set; }

        public MvxNotifyTask LoadRepositoriesTask
        {
            get => loadRepositoriesTask;
            private set => SetProperty(ref loadRepositoriesTask, value);
        }

        private MvxObservableCollection<GithubRepository> _githubRepositories;
        public MvxObservableCollection<GithubRepository> GithubRepositories
        {
            get
            {
                return _githubRepositories;
            }
            set
            {
                _githubRepositories = value;
                RaisePropertyChanged(() => GithubRepositories);
            }
        }

        private string _labelText;

        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                RaisePropertyChanged(() => LabelText);
            }
        }

        public RepositoriesViewModel(IRepositoryService repositoryService, IMvxNavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;

            LabelText = "Loading";
            GithubRepositories = new MvxObservableCollection<GithubRepository>();

            RepositorySelectedCommand = new MvxCommand<GithubRepository>((repo) =>
            {
                _navigationService.Navigate<PullRequestsViewModel, GithubRepository>(repo);
            });
            RefreshRepositoriesCommand = new MvxCommand(RefreshRepositories);
            LoadRepositoriesCommand = new MvxCommand(
                () =>
                {
                    LoadRepositoriesTask = MvxNotifyTask.Create(LoadRepos(), onException: ex => OnException(ex));
                    RaisePropertyChanged(() => LoadRepositoriesTask);
                });
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private async Task LoadRepos()
        {
            var data = await _repositoryService.GetRepositories(_pageNumber ,null, null);

            if(_pageNumber == 1)
            {
                GithubRepositories.Clear();
            }

            GithubRepositories.AddRange(data);
            _pageNumber++;
        }

        private void RefreshRepositories()
        {
            _pageNumber = 1;

            LoadRepositoriesTask = MvxNotifyTask.Create(LoadRepos(), onException: ex => OnException(ex));
            RaisePropertyChanged(() => LoadRepositoriesTask);
        }

        private void OnException(Exception exception)
        {
            LabelText = "Loading failed";
        }
    }
}
