using GalaSoft.MvvmLight.Messaging;
using Profumeria.Core.Mock.Storage;
using Profumeria.WPF.Messaging.Misc;
using Profumeria.WPF.ViewModels;
using Profumeria.WPF.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Profumeria.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Messenger.Default.Register<DialogMessage>(this, OnDialogMessageReceived);
            Messenger.Default.Register<ShutDownApplicationMessage>(this, _ => Current.Shutdown());

            GiftCardMockStorage.Initialize();
            GiftCardEditorView view = new GiftCardEditorView();
            GiftCardEditorViewModel vm = new GiftCardEditorViewModel();
            view.DataContext = vm;
            view.Show();
            base.OnStartup(e);
        }

        private void OnDialogMessageReceived(DialogMessage message)
        {
            MessageBoxResult result = MessageBox.Show(
                message.Content,
                message.Title,
                message.Buttons, message.Icon);           
            if (message.Callback != null)
                message.Callback(result);
        }
    }
}
