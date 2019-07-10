using GenesisTest.Core.Models;
using GenesisTest.Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GenesisTest.Core.ViewModels
{
    public class PullRequestsViewModel : MvxViewModel<GithubRepository>
    {
        private readonly IRepositoryService _repositoryService;
        private MvxNotifyTask _getPullRequestsTask;
        private int _pageNumber = 1;
        private int _totalPagedCount = 999;
        private GithubRepository _repository;
        private MvxObservableCollection<PullRequest> _pullRequests;

        public IMvxCommand GetPullRequestsCommand { get; private set; }
        public IMvxCommand GetNextPageCommand { get; private set; }
        public IMvxCommand PullRequestSelectedCommand { get; }

        public MvxNotifyTask GetPullRequestsTask
        {
            get => _getPullRequestsTask;
            private set => SetProperty(ref _getPullRequestsTask, value);
        }
        public MvxObservableCollection<PullRequest> PullRequests
        {
            get
            {
                return _pullRequests;
            }
            set
            {
                _pullRequests = value;
                RaisePropertyChanged(() => PullRequests);
            }
        }

        public PullRequestsViewModel(IRepositoryService repositoryService)
        {
            _repositoryService = repositoryService;

            PullRequests = new MvxObservableCollection<PullRequest>();

            GetPullRequestsCommand = new MvxCommand<string>((searchText) =>
            {
                GetPullRequestsTask = MvxNotifyTask.Create(RefreshPullRequests, onException: ex => OnException(ex));
                RaisePropertyChanged(() => GetPullRequestsTask);
            });

            GetNextPageCommand = new MvxCommand(() =>
            {
                GetPullRequestsTask = MvxNotifyTask.Create(GetPullRequests, onException: ex => OnException(ex));
                RaisePropertyChanged(() => GetPullRequestsTask);
            }, CanGetNextPage());

            PullRequestSelectedCommand = new MvxCommand<PullRequest>(async (pullRequest) =>
            {
                await Browser.OpenAsync(pullRequest.Url, BrowserLaunchMode.SystemPreferred);
            });
        }

        public override void Prepare(GithubRepository parameter)
        {
            _repository = parameter;
        }

        public override Task Initialize()
        {
            GetPullRequestsTask = MvxNotifyTask.Create(RefreshPullRequests, onException: ex => OnException(ex));
            RaisePropertyChanged(() => GetPullRequestsTask);

            return Task.FromResult(0);
        }

        private async Task GetPullRequests()
        {
            var pagedResults = await _repositoryService.GetPullRequests(_pageNumber, _repository);

            PullRequests.AddRange(pagedResults.Results);
            _pageNumber++;
            _totalPagedCount = pagedResults.TotalCount;
        }

        private async Task RefreshPullRequests()
        {
            _pageNumber = 1;
            PullRequests.Clear();

            await GetPullRequests();
        }

        private void OnException(Exception exception)
        {
            // TODO: put a notification on the screen
        }

        private Func<bool> CanGetNextPage()
        {
            return () =>
            {
                return PullRequests.Count < _totalPagedCount;
            };
        }
    }
}
