using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;

namespace BL.Evento
{
    //classe per gestire le diverse funzionalità dell'oggetto evento
    public class EventoManager
    {
        
        //metodo per recuperare tutte gli evento da DB
        public static IEnumerable<EventoDTO> EventoDTOList()
        {
            var db = new DAL.AperitivoEntities();
            var myEvento = from b in db.Evento.ToList()
                            select new EventoDTO()
                            {
                                ID = b.ID,
                                Descrizione = b.Descrizione,
                                Anno = b.Anno
                            };

            return myEvento;
        }

        //metodo per recuperare tutte gli evento da DB utilizzando il valore ID inviato dal client
        public static EventoTabella EventoDTO(int id)
        {
            var db = new DAL.AperitivoEntities();
            var myEvento = db.Evento.Where(r => r.ID == id).FirstOrDefault();

            var eventoRisorsaNome = db.Risorsa.Where(r => r.ID == myEvento.Risorse).Select(s => s.Nome).FirstOrDefault();
            var eventoRisorsaCognome = db.Risorsa.Where(r => r.ID == myEvento.Risorse).Select(s => s.Cognome).FirstOrDefault();
            var eventoRisorsa = eventoRisorsaCognome + ' ' + eventoRisorsaNome;

            var eventoPrenotazione = db.Prenotazione.Where(r => r.PrenotazioneEvento == myEvento.ID).Select(s => s.Descrizione).FirstOrDefault();
            var idSala = db.Prenotazione.Where(r => r.PrenotazioneEvento == myEvento.ID).Select(s => s.PrenotazioneSala).FirstOrDefault();
            var sala = db.Sala.Where(r => r.ID == idSala).Select(s => s.Nome).FirstOrDefault();

            return new EventoTabella()
            {
                ID = myEvento.ID,
                Descrizione = myEvento.Descrizione,
                Anno = myEvento.Anno,
                Data = myEvento.Data,
                Risorse = eventoRisorsa,
                Prenotazione = eventoPrenotazione,
                Sala = sala
            };
        }

        public static IEnumerable<EventoDTO> EventoByDescrizione(string descrizione)
        {

            var db = new DAL.AperitivoEntities();
            var myEventi = from b in db.Evento.Where(r => r.Descrizione.Contains(descrizione)).ToList()
                            select new EventoDTO()
                            {
                                ID = b.ID,
                                Descrizione = b.Descrizione,
                                Anno = b.Anno
                            };

            return myEventi;
        }

        public static IEnumerable<EventoDTO> EventoByAnno(string anno)
        {

            var db = new DAL.AperitivoEntities();
            var myEventi = from b in db.Evento.Where(r => r.Anno == anno).ToList()
                           select new EventoDTO()
                           {
                               ID = b.ID,
                               Descrizione = b.Descrizione,
                               Anno = b.Anno
                           };

            return myEventi;
        }


        //metodo per inserire un evento in DB utilizzando i valori inviati dal client
        public static void AddEvento(EventoDetail evento)
        {
            var db = new DAL.AperitivoEntities();
            int anno = evento.Data.Value.Year;
            var myEvento = DTOtoEntity(evento);
            myEvento.Anno = anno.ToString();
            var result = db.Evento.Add(myEvento);
            Interaction.MsgBox("Evento '" + myEvento.Descrizione + "' inserito", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Inserimento Nuovo Evento");
            db.SaveChanges();
        }

        //metodo per cancellare un evento da DB utilizzando l' ID evento inviato dal client
        public static void DeleteEvento(int id)
        {
            var db = new DAL.AperitivoEntities();
            var myEvento = db.Evento.Where(r => r.ID == id).FirstOrDefault();
            var myResult = db.Evento.Remove(myEvento);
            db.SaveChanges();
        }

        //metodo per recuperare l 'ID massimo dalla tabella evento da DB 
        public static EventoDTO MaxIDEvento()
        {
            var db = new DAL.AperitivoEntities();
            var id = 0;
            try
            {
                id = db.Evento.Max(i => i.ID);
            }
            catch (Exception ex)
            {
                if (ex is null) { id = 0; }
            }

            var obj = new EventoDTO()
            {
                ID = id,
                Descrizione = "",
                Anno = ""
            };

            return obj;
        }

        public static DAL.Evento DTOtoEntity(EventoDetail evento)
        {
            return new DAL.Evento()
            {
                ID = evento.ID,
                Data = evento.Data,
                Descrizione = evento.Descrizione,
                Anno = evento.Anno,
                Risorse = evento.Risorse
            };

        }
    }
}
