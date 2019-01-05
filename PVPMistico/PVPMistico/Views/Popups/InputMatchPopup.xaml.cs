using PVPMistico.ViewModels.PopupViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputMatchPopup : BasePopup
	{
		public InputMatchPopup ()
		{
			InitializeComponent ();
            ConfirmButton.Clicked += ConfirmButton_Clicked;
        }

        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
            //HACK: Fixes bug causing picker to focus on GoBackAsync();
            OpponentPicker.Unfocus();
        }

            //HACK: Fixes bug where picker was focused when clicking the button
            private void ConfirmButton_Clicked(object sender, System.EventArgs e)
        {
            OpponentPicker.Unfocus();
            WinnerPicker.Unfocus();
        }
    }
}