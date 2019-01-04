using Acr.UserDialogs;
using PVPMistico.Enums;
using PVPMistico.Managers.Interfaces;
using System.Drawing;

namespace PVPMistico.Managers
{
    public class DialogManager : IDialogManager
    {
        public void ShowAlert(AlertConfig config)
        {
            UserDialogs.Instance.Alert(config);
        }

        public void ShowConfirmationDialog(ConfirmConfig config)
        {
            UserDialogs.Instance.Confirm(config);
        }

        public void ShowToast(ToastConfig config, ToastModes mode = ToastModes.Info)
        {
            switch (mode)
            {
                case ToastModes.Error:
                    config.SetBackgroundColor(Color.Red);
                    config.SetMessageTextColor(Color.White);
                    //config.SetIcon(AppImages.Error);
                    //config.Position = ToastPosition.Top;
                    break;

                case ToastModes.Warning:
                    config.SetBackgroundColor(Color.Yellow);
                    config.SetMessageTextColor(Color.Black);
                    //config.SetIcon(AppImages.Warning);
                    //config.Position = ToastPosition.Top;
                    break;

                
                case ToastModes.Info:
                    config.SetBackgroundColor(Color.LightGray);
                    config.SetMessageTextColor(Color.Black);
                    //config.SetIcon(AppImages.Info);
                    break;
            }
            UserDialogs.Instance.Toast(config);
        }

        public void ShowToast(string message, ToastModes mode = ToastModes.Info)
        {
            var config = new ToastConfig(message);
            ShowToast(config, mode);
        }

        public void ShowLoading(string title = null)
        {
            UserDialogs.Instance.ShowLoading(title);
        }

        public void EndLoading()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}