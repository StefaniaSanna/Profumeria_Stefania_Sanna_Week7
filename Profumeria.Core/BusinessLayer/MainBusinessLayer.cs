using Profumeria.Core.Entities;
using Profumeria.Core.Repositories;
using Profumeria.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profumeria.Core.BusinessLayer
{
    public class MainBusinessLayer
    {
        private IGiftCardRepository _giftCardRepository;

        public MainBusinessLayer( IGiftCardRepository repoGiftCard)
        {
            _giftCardRepository=repoGiftCard;
        }

        public List<GiftCard> FetchAllGiftCards()
        {
            return _giftCardRepository.FetchAll().ToList();
        }

        public Response DeleteGiftCard(GiftCard giftCardDaEliminare)
        {
            if (giftCardDaEliminare == null)
                return new Response { Success = false, Message = "giftCard invalida!" };
            if (giftCardDaEliminare.Id < 0)
                return new Response { Success = false, Message = "ID Invalido" };
            var giftCardToDelete = FetchAllGiftCards().FirstOrDefault(x => x.Id == giftCardDaEliminare.Id);
            if (giftCardToDelete == null)
                return new Response
                {
                    Success = false,
                    Message = $"Nessuna giftCard ha Id: {giftCardDaEliminare.Id}"
                };
            _giftCardRepository.Delete(giftCardToDelete);

            return new Response { Success = true, Message = $"GiftCard eliminata con successo" };
        }

        public Response CreateGiftCard(GiftCard nuovaGiftCard)
        {
            if (nuovaGiftCard == null)
                return new Response { Success = false, Message = " GiftCard invalida" };

            if (nuovaGiftCard.Importo < 0.0)
                return new Response { Success = false, Message = "E' un regalo, devi spendere!" };

            _giftCardRepository.Create(nuovaGiftCard);
            return new Response
            {
                Success = true,
                Message = $"{nuovaGiftCard.Mittente} hai regalato una giftCard a {nuovaGiftCard.Destinatario} da {nuovaGiftCard.Importo} euro!"
            };
        }

        public Response UpdateGiftCard(GiftCard giftCard)
        {
            if (giftCard == null)
                return new Response() { Success = false, Message = "GiftCard non valida" };
   
            var giftCardToUpdate = FetchAllGiftCards().FirstOrDefault(x => x.Id == giftCard.Id);
            _giftCardRepository.Update(giftCardToUpdate, giftCard);
            return new Response() { Success = true, Message = "GiftCard modificata" };
        }
    }
}
