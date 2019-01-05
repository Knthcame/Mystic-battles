using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PVPMistico.Widgets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConditionalMenuItemsViewCell : ViewCell
	{
        private IList<MenuItem> _myMenus = new List<MenuItem>();

        private bool _menusEnabled = true;

        public static readonly BindableProperty MenuItemsEnabledProperty =
            BindableProperty.Create(propertyName: nameof(MenuItemsEnabled), returnType: typeof(bool), defaultValue: true, declaringType: typeof(ValidationEntry));

        public bool MenuItemsEnabled
        {
            get => (bool)GetValue(MenuItemsEnabledProperty);
            set => SetValue(MenuItemsEnabledProperty, value);
        }

        public ConditionalMenuItemsViewCell ()
		{
			InitializeComponent ();
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            try
            {
                if (propertyName == nameof(MenuItemsEnabled))
                {
                    if (MenuItemsEnabled)
                        RestoreMenus();
                    else
                        StoreAndClearMenus();
                }
                else if(propertyName == nameof(HasContextActions))
                {
                    if (!_menusEnabled && HasContextActions)
                        StoreAndClearMenus();
                }
            }
            catch (Exception e) { }
        }

        private void StoreAndClearMenus()
        {
            _menusEnabled = false;
            _myMenus = MyViewCell.ContextActions;
            MyViewCell.ContextActions.Clear();
        }

        private void RestoreMenus()
        {
            _menusEnabled = true;
            foreach (MenuItem menu in _myMenus)
            {
                MyViewCell.ContextActions.Add(menu);
            }
        }
    }
}