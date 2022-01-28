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
    public class UpdateGiftCardViewModel: ViewModelBase
    {
        private GiftCard giftCard;
        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; RaisePropertyChanged(); }
        }

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

        public UpdateGiftCardViewModel()
        {
            UpdateCommand = new RelayCommand(() => ExecuteUpdate());
            UpdateCommand = new RelayCommand(() => ExecuteCancel());
            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (UpdateCommand as RelayCommand).RaiseCanExecuteChanged();
                };

            }

        }

        public UpdateGiftCardViewModel(GiftCard giftCard):this()
        {
            if (giftCard == null) throw new ArgumentNullException(nameof(giftCard));
            this.giftCard = giftCard;
            Id = giftCard.Id;
            Messaggio= giftCard.Messaggio; 
            Mittente= giftCard.Mittente;
            Importo= giftCard.Importo;
            DataDiScadenza= giftCard.DataDiScadenza;
            Destinatario= giftCard.Destinatario;


        }

        private void ExecuteCancel()
        {
            Messenger.Default.Send(new CloseUpdateGiftCardMessage());
        }

        private void ExecuteUpdate()
        {
            var giftCardToUpdate = new GiftCard
            {
                Id = Id,
                Messaggio = Messaggio,
                Mittente = Mittente,
                DataDiScadenza = DataDiScadenza,
                Destinatario = Destinatario,
                Importo = Importo

            };

            var bl = new MainBusinessLayer( new GiftCardRepositoryMock());
            var result = bl.UpdateGiftCard(giftCardToUpdate);
            if (!result.Success)
            {
                
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Attenzione! Alcuni dati non sono validi!",
                    Content = result.Message,
                    Icon = MessageBoxImage.Warning
                });
                return;
            }
            Messenger.Default.Send(new DialogMessage
            {
                Title = "Conferma",
                Content = $"La GiftCard regalata da {Mittente} per {Destinatario} è stata aggiornata!",
                Icon = MessageBoxImage.Information
            });
            CancelCommand.Execute(null);
        }
    }
}
