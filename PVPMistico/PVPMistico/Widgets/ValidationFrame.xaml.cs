using PVPMistico.Validation;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidationFrame : Frame
    {
        #region BindableProperties
        public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create(propertyName: nameof(UnfocusedCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(ValidationFrame));

        public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.Create(propertyName: nameof(ValidatableObject), returnType: typeof(ValidatableObject<string>), defaultValue: null, declaringType: typeof(ValidationFrame));

        public static readonly BindableProperty TitleHeightProperty =
            BindableProperty.Create(propertyName: nameof(TitleHeight), returnType: typeof(GridLength), defaultValue: new GridLength(22.5, GridUnitType.Star), declaringType: typeof(ValidationFrame));

        public static readonly BindableProperty EntryHeightProperty =
            BindableProperty.Create(propertyName: nameof(EntryHeight), returnType: typeof(GridLength), defaultValue: new GridLength(55, GridUnitType.Star), declaringType: typeof(ValidationFrame));

        public static readonly BindableProperty ErrorHeightProperty =
            BindableProperty.Create(propertyName: nameof(ErrorHeight), returnType: typeof(GridLength), defaultValue: new GridLength(22.5, GridUnitType.Star), declaringType: typeof(ValidationFrame));

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(propertyName: nameof(Title), returnType: typeof(string), defaultValue: null, declaringType: typeof(ValidationFrame));
        #endregion

        #region Properties
        public ICommand UnfocusedCommand
        {
            get => (ICommand)GetValue(UnfocusedCommandProperty);
            set => SetValue(UnfocusedCommandProperty, value);
        }

        public ValidatableObject<string> ValidatableObject
        {
            get => (ValidatableObject<string>)GetValue(ValidatableObjectProperty);
            set => SetValue(ValidatableObjectProperty, value);
        }

        public GridLength TitleHeight
        {
            get => (GridLength)GetValue(TitleHeightProperty);
            set => SetValue(TitleHeightProperty, value);
        }

        public GridLength EntryHeight
        {
            get => (GridLength)GetValue(EntryHeightProperty);
            set => SetValue(EntryHeightProperty, value);
        }

        public GridLength ErrorHeight
        {
            get => (GridLength)GetValue(ErrorHeightProperty);
            set => SetValue(ErrorHeightProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        #endregion
        public ValidationFrame ()
		{
			InitializeComponent ();
		}
	}
}