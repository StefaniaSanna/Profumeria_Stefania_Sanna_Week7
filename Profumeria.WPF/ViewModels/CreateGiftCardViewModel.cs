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
using System.Windows.Input;

namespace Profumeria.WPF.ViewModels
{
    public class CreateGiftCardViewModel:ViewModelBase
    {
        public ICommand CreateCommand { get; set; }

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

        public CreateGiftCardViewModel()
        {
            CreateCommand = new RelayCommand(() => ExecuteCreate(), () => CanExecuteCreate());

            if (!IsInDesignMode)
            {
                PropertyChanged += (s, e) =>
                {
                    (CreateCommand as RelayCommand).RaiseCanExecuteChanged();
                };
            }

        }

        private bool CanExecuteCreate()
        {
            return !string.IsNullOrEmpty(_Mittente) &&
             !string.IsNullOrEmpty(_Destinatario) &&
             !string.IsNullOrEmpty(_Importo.ToString()) &&
             !string.IsNullOrEmpty(_DataDiScadenza.ToString());
        }

        private void ExecuteCreate()
        {
            var nuovaGiftCard = new GiftCard
            {
                Messaggio = Messaggio,
                Importo = Importo,
                Mittente = Mittente,
                DataDiScadenza = DataDiScadenza,
                Destinatario = Destinatario,
            };

            var bl = new MainBusinessLayer(new GiftCardRepositoryMock());
            var response = bl.CreateGiftCard(nuovaGiftCard);
            if (!response.Success)
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Something wrong",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Warning
                });
                return;
            }
            else
            {
                Messenger.Default.Send(new DialogMessage
                {
                    Title = "Creazione completata",
                    Content = response.Message,
                    Icon = System.Windows.MessageBoxImage.Information
                });
            }
            Messenger.Default.Send(new CloseCreateGiftCardMessage());

        }
    }
}
