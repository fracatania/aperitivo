using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Prenotazione
{
    public class PrenotazioneTabella
    {
        public int ID { get; set; }
        public string Descrizione { get; set; }
        public Nullable<System.DateTime> DataInizio { get; set; }
        public Nullable<System.DateTime> DataFine { get; set; }
        public string PrenotazioneRisorsa { get; set; }
        public string PrenotazioneEvento { get; set; }
        public string PrenotazioneSala { get; set; }
    }
}

