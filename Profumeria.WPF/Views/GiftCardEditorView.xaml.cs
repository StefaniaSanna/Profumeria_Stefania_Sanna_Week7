using GalaSoft.MvvmLight.Messaging;
using Profumeria.WPF.Messaging.GiftCardMessaging;
using Profumeria.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Profumeria.WPF.Views
{
    /// <summary>
    /// Interaction logic for GiftCardEditorView.xaml
    /// </summary>
    public partial class GiftCardEditorView : Window
    {
        public GiftCardEditorView()
        {
            InitializeComponent();
            Messenger.Default.Register<showCreateGiftCardMessage>(this, OnShowCreateGiftCardExecuted);
            Messenger.Default.Register<ShowUpdateGiftCardMessage>(this, OnShowUpdateGiftCardMessageReceived);

            
        }

        private void OnShowUpdateGiftCardMessageReceived(ShowUpdateGiftCardMessage message)
        {
            UpdateGiftCardView view = new UpdateGiftCardView();
            UpdateGiftCardViewModel vm = new UpdateGiftCardViewModel(message.Entity);
            view.DataContext = vm;
            view.ShowDialog();



        }

        private void OnShowCreateGiftCardExecuted(showCreateGiftCardMessage message)
        {
            CreateGiftCardView view = new CreateGiftCardView(); 
            CreateGiftCardViewModel vm = new CreateGiftCardViewModel();
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
