using Profumeria.Core.BusinessLayer;
using Profumeria.Core.Entities;
using Profumeria.Core.Mock.Repositories;
using Profumeria.Core.Mock.Storage;
using Profumeria.Core.Repositories;
using Profumeria.Core.Utils;
using Xunit;

namespace Profumeria.Test
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldCreateBeOk()
        {
            //ARRANGE
            GiftCard giftCard = new GiftCard()
            {
                Mittente = "Stefania",
                Destinatario = "Laura",
                Importo = 45,
                Messaggio = "tanti auguri",
                DataDiScadenza = new System.DateTime(2022, 12, 12)

            };

            GiftCardMockStorage.Initialize();
            IGiftCardRepository repo = new GiftCardRepositoryMock();
            MainBusinessLayer bl = new MainBusinessLayer(repo);

            //conto prima di aggiungere
            int countGiftCardsBefore = bl.FetchAllGiftCards().Count;

            //ACT

            Response aggiunta = bl.CreateGiftCard(giftCard);

            //ASSERT
            
            int countGiftCardsAfter = bl.FetchAllGiftCards().Count;
            Assert.Equal(countGiftCardsBefore+1, countGiftCardsAfter);





        }
    }
}