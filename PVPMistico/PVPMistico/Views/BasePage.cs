using PVPMistico.ViewModels.BaseViewModels;
using Xamarin.Forms;

namespace PVPMistico.Views
{
    public class BasePage : ContentPage
	{
		public BasePage ()
		{
        }
        protected override bool OnBackButtonPressed()
        {
            var bindingContext = BindingContext as BaseViewModel;
            return (bool) bindingContext?.OnBackButtonPressed();
        }
	}
}