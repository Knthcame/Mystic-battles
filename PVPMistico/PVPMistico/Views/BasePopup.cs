using PVPMistico.ViewModels;
using Rg.Plugins.Popup.Pages;

namespace PVPMistico.Views
{
    public class BasePopup : PopupPage
    {
		public BasePopup ()
		{
		}

        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            return (bool) bindingContext?.OnBackButtonPressed();
        }
    }
}