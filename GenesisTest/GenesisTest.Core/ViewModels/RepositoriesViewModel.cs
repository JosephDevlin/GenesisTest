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
        private MvxNotifyTask _getRepositoriesTask;
        private int _pageNumber = 1;
        private int _totalPagedCount = 999;
        private MvxObservableCollection<GithubRepository> _githubRepositories;
        private string _searchText;

        public IMvxCommand GetRepositoriesCommand { get; private set; }
        public IMvxCommand GetNextPageCommand { get; private set; }
        public IMvxCommand RepositorySelectedCommand { get; private set; }
        public string SearchString { get; private set; }

        public MvxNotifyTask GetRepositoriesTask
        {
            get => _getRepositoriesTask;
            private set => SetProperty(ref _getRepositoriesTask, value);
        }
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

        public RepositoriesViewModel(IRepositoryService repositoryService, IMvxNavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;

            GithubRepositories = new MvxObservableCollection<GithubRepository>();

            GetRepositoriesCommand = new MvxCommand<string>((searchText) =>
            {
                _searchText = searchText;
                GetRepositoriesTask = MvxNotifyTask.Create(RefreshRepositories, onException: ex => OnException(ex));
                RaisePropertyChanged(() => GetRepositoriesTask);
            });

            GetNextPageCommand = new MvxCommand(() =>
            {
                GetRepositoriesTask = MvxNotifyTask.Create(GetRepositories, onException: ex => OnException(ex));
                RaisePropertyChanged(() => GetRepositoriesTask);
            }, CanGetNextPage());

            RepositorySelectedCommand = new MvxCommand<GithubRepository>((repo) =>
            {
                _navigationService.Navigate<PullRequestsViewModel, GithubRepository>(repo);
            });
        }

        public override Task Initialize()
        {
            GetRepositoriesTask = MvxNotifyTask.Create(RefreshRepositories, onException: ex => OnException(ex));
            RaisePropertyChanged(() => GetRepositoriesTask);

            return Task.FromResult(0);
        }

        private async Task GetRepositories()
        {
            var pagedResults = await _repositoryService.GetRepositories(_pageNumber, _searchText);

            GithubRepositories.AddRange(pagedResults.Results);
            _pageNumber++;
            _totalPagedCount = pagedResults.TotalCount;
        }

        private async Task RefreshRepositories()
        {
            _pageNumber = 1;
            GithubRepositories.Clear();
            await GetRepositories();
        }

        private void OnException(Exception exception)
        {
            // TODO: put a notification on the screen
        }

        private Func<bool> CanGetNextPage()
        {
            return () =>
            {
                return GithubRepositories.Count < _totalPagedCount;
            };
        }
    }
}
