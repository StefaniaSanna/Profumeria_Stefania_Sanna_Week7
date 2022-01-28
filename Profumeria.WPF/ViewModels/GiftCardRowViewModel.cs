using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Profumeria.Core.BusinessLayer;
using Profumeria.Core.Entities;
using Profumeria.Core.Mock.Repositories;
using Profumeria.WPF.Messaging.GiftCardMessaging;
using Profumeria.WPF.Messaging.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Profumeria.WPF.ViewModels
{
    public class GiftCardRowViewModel:ViewModelBase
    {
        private GiftCard giftCard;

        private string _Mittente;
        public string Mittente
        {
            get { return _Mittente; }
            set { _Mittente = value; RaisePropertyChanged(); }
        }

        private string _Destinatario;
        public string Destinatario
        {
            get { return _Destinatario; }
            set { _Destinatario = value; RaisePropertyChanged(); }
        }

        private string _Messaggio;
        public string Messaggio
        {
            get { return _Messaggio; }
            set { _Messaggio = value; RaisePropertyChanged(); }
        }

        private double _Importo;
        public double Importo
        {
            get { return _Importo; }
            set { _Importo = value; RaisePropertyChanged(); }
        }

        private DateTime _DataDiScadenza;
        public DateTime DataDiScadenza
        {
            get { return _DataDiScadenza; }
            set { _DataDiScadenza = value; RaisePropertyChanged(); }
        }

        private bool _ViewDetails = false;
        public bool ViewDetails
        {
            get { return _ViewDetails; }
            set { _ViewDetails = value; RaisePropertyChanged(); }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public GiftCardRowViewModel()
        {
            UpdateCommand = new RelayCommand(() => ExecuteUpdate()); 
            DeleteCommand = new RelayCommand(() => ExecuteDelete());


        }

        private void ExecuteUpdate()
        {
            Messenger.Default.Send(new ShowUpdateGiftCardMessage { Entity = giftCard });
        }

        private void ExecuteDelete()
        {
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Confirm delete",
                Content = "Are you sure?",
                Icon = MessageBoxImage.Question,
                Buttons = MessageBoxButton.YesNo,
                Callback = OnMessageBoxResultReceived
            });
        }

        private void OnMessageBoxResultReceived(MessageBoxResult result)
        {
            if (result == MessageBoxResult.Yes)
            {
                var layer = new MainBusinessLayer(new GiftCardRepositoryMock());
                var response = layer.DeleteGiftCard(giftCard);
                if (!response.Success)
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Errore",
                        Content = response.Message,
                        Icon = MessageBoxImage.Error,
                        Buttons = MessageBoxButton.OK
                    });
                    return;
                }
                else
                {
                    Messenger.Default.Send(new DialogMessage
                    {
                        Title = "Deletion Confirmed",
                        Content = response.Message,
                        Icon = MessageBoxImage.Information
                    });
                }
            }
        }

        public GiftCardRowViewModel(GiftCard giftCard):this()
        {
            this.giftCard = giftCard;
            Mittente = giftCard.Mittente;
            Destinatario = giftCard.Destinatario;
            Messaggio = giftCard.Messaggio;
            Importo = giftCard.Importo;
            DataDiScadenza=giftCard.DataDiScadenza;
        }
    }
}
