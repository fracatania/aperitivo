using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Evento
{
    public class EventoTabella
    {
        public int ID { get; set; }
        public string Descrizione { get; set; }
        public string Anno { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public string Risorse { get; set; }
        public string Prenotazione { get; set; }
        public string Sala { get; set; }
    }
}
