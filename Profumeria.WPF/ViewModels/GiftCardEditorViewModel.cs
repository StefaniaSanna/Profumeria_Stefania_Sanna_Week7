using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Profumeria.Core.BusinessLayer;
using Profumeria.Core.Entities;
using Profumeria.Core.Mock.Repositories;
using Profumeria.Core.Repositories;
using Profumeria.WPF.Messaging.GiftCardMessaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Profumeria.WPF.ViewModels
{
    public class GiftCardEditorViewModel:ViewModelBase
    {

        public ICommand CreateGiftCard { get; set; }

        public ObservableCollection<GiftCardRowViewModel> _GiftCardsSource;

        private ICollectionView _GiftCards;
        public ICollectionView GiftCards
        {
            get { return _GiftCards; }
            set { _GiftCards = value; RaisePropertyChanged(); }
                      
        }

        public ICommand LoadGiftCardsCommand { get; set; }

        //costruttore vuoto
        public GiftCardEditorViewModel()
        {
            CreateGiftCard = new RelayCommand(() => ExecuteShowCreateGiftCard());
            LoadGiftCardsCommand = new RelayCommand(() => ExecuteLoadGiftCards());

            _GiftCardsSource = new ObservableCollection<GiftCardRowViewModel>();
            _GiftCards = new CollectionView(_GiftCardsSource);

            LoadGiftCardsCommand.Execute(null);

        }

        private void ExecuteLoadGiftCards()
        {
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            MainBusinessLayer bl = new MainBusinessLayer(repo);
            List<GiftCard> giftCards = bl.FetchAllGiftCards();
            _GiftCardsSource.Clear();

            foreach (var giftCard in giftCards)
            {
                var vmGiftCardRow = new GiftCardRowViewModel(giftCard);
                _GiftCardsSource.Add(vmGiftCardRow);
            } 

        }

        private void ExecuteShowCreateGiftCard()
        {
            Messenger.Default.Send(new showCreateGiftCardMessage());
        }
    }
}
