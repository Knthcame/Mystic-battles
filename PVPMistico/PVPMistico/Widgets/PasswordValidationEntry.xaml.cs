using PVPMistico.Validation;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordValidationEntry : Grid

    {
        #region BindableProperties
        public static readonly BindableProperty UnfocusedCommandProperty =
            BindableProperty.Create(propertyName: nameof(UnfocusedCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(PasswordValidationEntry));

        public static readonly BindableProperty PasswordVisibilityToggleCommandProperty =
            BindableProperty.Create(propertyName: nameof(PasswordVisibilityToggleCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(PasswordValidationEntry));

        public static readonly BindableProperty ValidatableObjectProperty =
            BindableProperty.Create(propertyName: nameof(ValidatableObject), returnType: typeof(ValidatableObject<string>), defaultValue: null, declaringType: typeof(PasswordValidationEntry));

        public static readonly BindableProperty IsPasswordProperty =
            BindableProperty.Create(propertyName: nameof(IsPassword), returnType: typeof(bool), defaultValue: true, declaringType: typeof(PasswordValidationEntry));

        public static readonly BindableProperty PasswordVisibilityIconProperty =
            BindableProperty.Create(propertyName: nameof(PasswordVisibilityIcon), returnType: typeof(ImageSource), defaultValue: null, declaringType: typeof(PasswordValidationEntry));
        #endregion

        #region Properties
        public ICommand UnfocusedCommand
        {
            get => (ICommand)GetValue(UnfocusedCommandProperty);
            set => SetValue(UnfocusedCommandProperty, value);
        }

        public ICommand PasswordVisibilityToggleCommand
        {
            get => (ICommand)GetValue(PasswordVisibilityToggleCommandProperty);
            set => SetValue(PasswordVisibilityToggleCommandProperty, value);
        }

        public ValidatableObject<string> ValidatableObject
        {
            get => (ValidatableObject<string>)GetValue(ValidatableObjectProperty);
            set => SetValue(ValidatableObjectProperty, value);
        }

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public ImageSource PasswordVisibilityIcon
        {
            get => (ImageSource)GetValue(PasswordVisibilityIconProperty);
            set => SetValue(PasswordVisibilityIconProperty, value);
        }
        #endregion

        public PasswordValidationEntry ()
		{
            try
            {
			    InitializeComponent ();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
		}
	}
}