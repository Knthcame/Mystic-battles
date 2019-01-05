using Prism.Mvvm;
using Prism.Navigation;
using PVPMistico.Logging.Interfaces;

namespace PVPMistico.ViewModels.BaseViewModels
{
    public class BaseViewModel : BindableBase, INavigationAware, IDestructible
    {
        private bool _isPageLoading;
        protected INavigationService NavigationService { get; private set; }
        protected ICustomLogger _logger;

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsPageLoading
        {
            get => _isPageLoading;
            set => SetProperty(ref _isPageLoading, value);
        }

        public virtual bool OnBackButtonPressed()
        {
            NavigationService.GoBackAsync();
            return true;
        }

        public BaseViewModel(INavigationService navigationService, ICustomLogger logger)
        {
            NavigationService = navigationService;
            _logger = logger;
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
