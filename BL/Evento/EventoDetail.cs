using System;

namespace BL.Evento
{

    public class EventoDetail
    {
        public int ID { get; set; }
        public string Descrizione { get; set; }
        public string Anno { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> Risorse { get; set; }
    }
}
