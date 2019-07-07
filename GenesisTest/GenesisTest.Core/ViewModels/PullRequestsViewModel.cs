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
    public class PullRequestsViewModel : MvxViewModel<GithubRepository>
    {
        private readonly IRepositoryService _repositoryService;
        private MvxNotifyTask loadPullRequestsTask;
        private int _pageNumber = 1;
        private string _labelText;
        private GithubRepository _githubRepository;
        private MvxObservableCollection<PullRequest> _githubPullRequests;

        public IMvxCommand LoadPullRequestsCommand { get; private set; }
        public IMvxCommand RefreshPullRequestsCommand { get; private set; }

        public MvxNotifyTask LoadPullRequestsTask
        {
            get => loadPullRequestsTask;
            private set => SetProperty(ref loadPullRequestsTask, value);
        }

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

        public IMvxCommand PullRequestSelectedCommand { get; }

        public string LabelText
        {
            get => _labelText;
            set
            {
                _labelText = value;
                RaisePropertyChanged(() => LabelText);
            }
        }

        public PullRequestsViewModel(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            LabelText = "Loading";
            GithubPullRequests = new MvxObservableCollection<PullRequest>();

            PullRequestSelectedCommand = new MvxCommand<PullRequest>(async (pullRequest) =>
            {
                await Browser.OpenAsync(pullRequest.Url, BrowserLaunchMode.SystemPreferred);
            });
            RefreshPullRequestsCommand = new MvxCommand(RefreshRepositories);
            LoadPullRequestsCommand = new MvxCommand(
                () =>
                {
                    LoadPullRequestsTask = MvxNotifyTask.Create(LoadRepos(), onException: ex => OnException(ex));
                    RaisePropertyChanged(() => LoadPullRequestsTask);
                });
        }

        public override void Prepare(GithubRepository parameter)
        {
            _githubRepository = parameter;
        }

        public override Task Initialize()
        {
            return base.Initialize();
        }

        private async Task LoadRepos()
        {
            var data = await _repositoryService.GetPullRequests(_githubRepository.AuthorUsername, _githubRepository.Name);

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
