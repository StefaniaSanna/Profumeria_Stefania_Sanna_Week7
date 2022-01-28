using Profumeria.Core.Entities;
using Profumeria.Core.Mock.Storage;
using Profumeria.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profumeria.Core.Mock.Repositories
{
    public class GiftCardRepositoryMock : IGiftCardRepository
    {
        public void Create(GiftCard giftCard)
        {
            var newId = GiftCardMockStorage.carte.Max(e => e.Id) +1;
            giftCard.Id = newId;
            GiftCardMockStorage.carte.Add(giftCard);
        }

        public void Delete(GiftCard giftCard)
        {
            var cartaTrovata = GiftCardMockStorage.carte.FirstOrDefault(e=>e.Id == giftCard.Id);
            GiftCardMockStorage.carte.Remove(cartaTrovata);
        }

        public IList<GiftCard> FetchAll()
        {
            return GiftCardMockStorage.carte.ToList();
        }

        

        public void Update(GiftCard vecchiaGiftCard, GiftCard nuovaGiftCard)
        {
            var existingGiftCard = GiftCardMockStorage.carte.FirstOrDefault(c => c.Id == nuovaGiftCard.Id);
            GiftCardMockStorage.carte.Remove(vecchiaGiftCard);
            GiftCardMockStorage.carte.Add(nuovaGiftCard);
        }
    }
}
