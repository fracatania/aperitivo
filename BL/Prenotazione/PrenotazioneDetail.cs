using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Prenotazione
{
    public class PrenotazioneDetail
    {
        public int ID { get; set; }
        public string Descrizione { get; set; }
        public Nullable<System.DateTime> DataInizio { get; set; }
        public Nullable<System.DateTime> DataFine { get; set; }
        public int PrenotazioneRisorsa { get; set; }
        public Nullable<int> PrenotazioneEvento { get; set; }
        public Nullable<int> PrenotazioneSala { get; set; }
    }
}
