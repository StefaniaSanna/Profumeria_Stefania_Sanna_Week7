using Profumeria.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profumeria.Core.Mock.Storage
{
    public class GiftCardMockStorage
    {
        public static IList<GiftCard> carte { get; set; }

        public static void Initialize()
        {
            carte = new List<GiftCard>();
            carte.Add(new GiftCard
            {
                Id = 1,
                Mittente = "Stefania Sanna",
                Destinatario = "Michela Pintor",
                Messaggio = "Tanti auguri di buon compleanno",
                Importo = 50,
                DataDiScadenza = new DateTime(2022, 05, 10)
            });

            carte.Add(new GiftCard
            {
                Id = 2,
                Mittente = "Mauro Sanna",
                Destinatario = "Pamela Gitani",
                Messaggio = "Buon anniversario!",
                Importo = 150,
                DataDiScadenza = new DateTime(2022, 02,15 )
            });
        }
    }
}
