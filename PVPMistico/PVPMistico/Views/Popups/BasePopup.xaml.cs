using PVPMistico.ViewModels;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BasePopup : PopupPage
	{
		public BasePopup ()
		{
			InitializeComponent ();
		}
        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            return (bool)bindingContext?.OnBackButtonPressed();
        }
    }
}