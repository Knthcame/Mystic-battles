using Prism;
using Prism.Ioc;
using PVPMistico.Logging;
using PVPMistico.Logging.Interfaces;
using PVPMistico.Managers;
using PVPMistico.Managers.Interfaces;
using PVPMistico.ViewModels;
using PVPMistico.ViewModels.PopupViewModels;
using PVPMistico.Views;
using PVPMistico.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PVPMistico
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync(nameof(StartupPage));
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LogInPage, LogInPageViewModel>();
            containerRegistry.RegisterForNavigation<SignInPage, SignInPageViewModel>();
            containerRegistry.RegisterForNavigation<StartupPage, StartupPageViewmodel>();
            containerRegistry.RegisterForNavigation<LeaderboardPage, LeaderboardPageViewModel>();
            containerRegistry.RegisterForNavigation<CreateLeaderboardPopup, CreateLeaderboardPopupViewModel>();
            containerRegistry.RegisterForNavigation<AddTrainerPopup, AddTrainerPopupViewModel>();
            containerRegistry.RegisterForNavigation<InputMatchPopup, InputMatchPopupViewModel>();

            containerRegistry.Register<IHttpManager, HttpManager>();
            containerRegistry.Register<IAccountManager, AccountManager>();
            containerRegistry.Register<ICustomLogger, CustomLogger>();
            containerRegistry.Register<ILeaderboardManager, LeaderboardManager>();
            containerRegistry.Register<IDialogManager, DialogManager>();
        }
    }
}
