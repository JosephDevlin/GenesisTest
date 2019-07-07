using GenesisTest.Core.Models;
using GenesisTest.Core.Services;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GenesisTest.Core.ViewModels
{
    public class PullRequestsViewModel : MvxViewModel
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IMvxNavigationService _navigationService;
        private MvxNotifyTask loadPullRequestsTask;
        private int _pageNumber = 1;

        public IMvxCommand LoadPullRequestsCommand { get; private set; }
        public IMvxCommand RefreshPullRequestsCommand { get; private set; }

        public MvxNotifyTask LoadPullRequestsTask
        {
            get => loadPullRequestsTask;
            private set => SetProperty(ref loadPullRequestsTask, value);
        }

        private MvxObservableCollection<PullRequest> _githubPullRequests;
        public MvxObservableCollection<PullRequest> GithubPullRequests
        {
            get
            {
                return _githubPullRequests;
            }
            set
            {
                _githubPullRequests = value;
                RaisePropertyChanged(() => GithubPullRequests);
            }
        }

        public MvxCommand PullRequestSelectedCommand { get; }

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

        public PullRequestsViewModel(IRepositoryService repositoryService, IMvxNavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;

            LabelText = "Loading";
            GithubPullRequests = new MvxObservableCollection<PullRequest>();

            PullRequestSelectedCommand = new MvxCommand(async () =>
            {
                await Browser.OpenAsync("https://github.com/freeCodeCamp/freeCodeCamp/pull/36387", BrowserLaunchMode.SystemPreferred);
            });
            RefreshPullRequestsCommand = new MvxCommand(RefreshRepositories);
            LoadPullRequestsCommand = new MvxCommand(
                () =>
                {
                    LoadPullRequestsTask = MvxNotifyTask.Create(LoadRepos(), onException: ex => OnException(ex));
                    RaisePropertyChanged(() => LoadPullRequestsTask);
                });
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private async Task LoadRepos()
        {
            var data = await _repositoryService.GetPullRequests(null);

            if (_pageNumber == 1)
            {
                GithubPullRequests.Clear();
            }

            GithubPullRequests.AddRange(data);
            _pageNumber++;
        }

        private void RefreshRepositories()
        {
            _pageNumber = 1;

            LoadPullRequestsTask = MvxNotifyTask.Create(LoadRepos(), onException: ex => OnException(ex));
            RaisePropertyChanged(() => LoadPullRequestsTask);
        }

        private void OnException(Exception exception)
        {
            LabelText = "Loading failed";
        }
    }
}
