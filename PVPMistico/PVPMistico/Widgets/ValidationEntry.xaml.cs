using PVPMistico.Validation;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ValidationEntry : Entry
	{
        #region BindableProperties
        public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create(propertyName: nameof(UnfocusedCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(ValidationEntry));

        public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.Create(propertyName: nameof(ValidatableObject), returnType: typeof(ValidatableObject<string>), defaultValue: null, declaringType: typeof(ValidationEntry));
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
        #endregion
        public ValidationEntry ()
		{
            try
            {
                InitializeComponent();
            }
			catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }
		}
    }
}