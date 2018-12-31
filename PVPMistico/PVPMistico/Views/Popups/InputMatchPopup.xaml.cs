using Xamarin.Forms.Xaml;

namespace PVPMistico.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InputMatchPopup : BasePopup
	{
		public InputMatchPopup ()
		{
			InitializeComponent ();
		}
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
            //HACK: Patches bug causing picker to focus on GoBackAsync();
            opponentPicker.Unfocus();
        }
    }
}