using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoadingIndicator : ContentView
	{
        public static readonly BindableProperty IsLoadingProperty =
            BindableProperty.Create(propertyName: nameof(IsLoading), returnType: typeof(bool), defaultValue: false, declaringType: typeof(LoadingIndicator));
        
        public bool IsLoading
        {
            get => (bool)GetValue(IsLoadingProperty);
            set => SetValue(IsLoadingProperty, value);
        }

        public LoadingIndicator ()
		{
			InitializeComponent ();
		}
    }
}