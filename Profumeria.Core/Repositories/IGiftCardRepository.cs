using Profumeria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profumeria.Core.Repositories
{
    public interface IGiftCardRepository
    {
        IList<GiftCard> FetchAll();
        void Create(GiftCard giftCard);
        void Update(GiftCard vecchiaGiftCard, GiftCard nuovaGiftCard);
        void Delete(GiftCard giftCard);
    }
}
