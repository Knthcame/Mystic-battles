using PVPMistico.Resources;
using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PasswordValidationFrame : BaseValidationFrame
    {
        #region BindableProperties
        public static readonly BindableProperty PasswordVisibilityToggleCommandProperty =
            BindableProperty.Create(propertyName: nameof(PasswordVisibilityToggleCommand), returnType: typeof(ICommand), defaultValue: null, declaringType: typeof(PasswordValidationFrame));

        public static readonly BindableProperty HidePasswordProperty =
            BindableProperty.Create(propertyName: nameof(HidePassword), returnType: typeof(bool), defaultValue: true, declaringType: typeof(PasswordValidationFrame));

        public static readonly BindableProperty PasswordVisibilityIconProperty =
            BindableProperty.Create(propertyName: nameof(PasswordVisibilityIcon), returnType: typeof(ImageSource), defaultValue: ImageSource.FromResource(AppImages.ViewPassword), declaringType: typeof(PasswordValidationFrame));
        #endregion

        #region Properties
        public ICommand PasswordVisibilityToggleCommand
        {
            get => (ICommand)GetValue(PasswordVisibilityToggleCommandProperty);
            set => SetValue(PasswordVisibilityToggleCommandProperty, value);
        }


        public bool HidePassword
        {
            get => (bool)GetValue(HidePasswordProperty);
            set => SetValue(HidePasswordProperty, value);
        }

        public ImageSource PasswordVisibilityIcon
        {
            get => (ImageSource)GetValue(PasswordVisibilityIconProperty);
            set => SetValue(PasswordVisibilityIconProperty, value);
        }
        #endregion
        public PasswordValidationFrame ()
		{
            try
            {
    			InitializeComponent ();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

		}
	}
}