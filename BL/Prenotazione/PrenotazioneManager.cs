using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.VisualBasic;

namespace BL.Prenotazione
{
    //classe per gestire le diverse funzionalità dell'oggetto prenotazione
    public class PrenotazioneManager
    {
        //metodo per recuperare tutte le prenotazioni da DB
        public static IEnumerable<PrenotazioneDTO> PrenotazioneDTOList()
        {
            var db = new DAL.AperitivoEntities();
            var myPrenotazione = from b in db.Prenotazione.ToList()
                           select new PrenotazioneDTO()
                           {
                               ID = b.ID,
                               Descrizione = b.Descrizione,
                           };

            return myPrenotazione;
        }

        //metodo per recuperare una prenotazione da DB  utilizzando il valore ID inviato dal client,
        //ritornando i valori descrittivi, e non l' ID, di Sala, Evento e Risorsa 
        public static PrenotazioneTabella PrenotazioneDTO(int id)
        {
            var db = new DAL.AperitivoEntities();
            var myPrenotazione = db.Prenotazione.Where(r => r.ID == id).FirstOrDefault();
            var prenotazioneRisorsaNome = db.Risorsa.Where(r => r.ID == myPrenotazione.PrenotazioneRisorsa).Select(s => s.Nome).FirstOrDefault();
            var prenotazioneRisorsaCognome = db.Risorsa.Where(r => r.ID == myPrenotazione.PrenotazioneRisorsa).Select(s => s.Cognome).FirstOrDefault();
            var prenotazioneRisorsa = prenotazioneRisorsaCognome + ' ' + prenotazioneRisorsaNome;
            var prenotazioneEvento = db.Evento.Where(r => r.ID == myPrenotazione.PrenotazioneEvento).Select(s => s.Descrizione).FirstOrDefault();
            var prenotazioneSala = db.Sala.Where(r => r.ID == myPrenotazione.PrenotazioneSala).Select(s => s.Nome).FirstOrDefault();

            return new PrenotazioneTabella()
            {
                ID = myPrenotazione.ID,
                Descrizione = myPrenotazione.Descrizione,
                DataInizio = myPrenotazione.DataInizio,
                DataFine = myPrenotazione.DataFine,
                PrenotazioneRisorsa = prenotazioneRisorsa,
                PrenotazioneEvento = prenotazioneEvento,
                PrenotazioneSala = prenotazioneSala

            };
        }

        //metodo per inserire una prenotazione in DB utilizzando i valori inviati dal client,
        //verificando se esiste una prenotazione che si sovrappone per orario nella stessa sala
        public static string AddPrenotazione(PrenotazioneDetail prenotazione)
        {
            var db = new DAL.AperitivoEntities();

            var dbPrenotazioniInizio = from p in db.Prenotazione
                                  where p.PrenotazioneSala == prenotazione.PrenotazioneSala
                                  select p.DataInizio;
            var dbPrenotazioniFine = from p in db.Prenotazione
                                       where p.PrenotazioneSala == prenotazione.PrenotazioneSala
                                       select p.DataFine;
            var verifica_prima = "ok";
            var verifica_dopo = "ok";
            foreach (var datai in dbPrenotazioniInizio)
            {
                if ((prenotazione.DataInizio < datai) && (prenotazione.DataFine < datai))
                {
                    verifica_prima = "ok";
                }
                else
                {
                    verifica_prima = "ko";
                    break;
                }
            }

            if (verifica_prima == "ko") {
               
                foreach (var dataf in dbPrenotazioniFine)
              {
                if ((prenotazione.DataInizio > dataf) && (prenotazione.DataFine > dataf))
                {
                    verifica_dopo = "ok";
                }
                else
                {
                    verifica_dopo = "ko";
                    break;
                }
              }
            }

            if ((verifica_prima == "ok") || (verifica_dopo == "ok"))
            {
                var myPrenotazione = DTOtoEntity(prenotazione);
                var result = db.Prenotazione.Add(myPrenotazione);
                db.SaveChanges();
                Interaction.MsgBox(result.Descrizione + " inserita", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Inserimento Nuova Prenotazione");
                return "ok";
            }
            else
            {
              return "ko";
            }
        }

        //metodo per cancellare una prenotazione da DB utilizzando l' ID evento inviato dal client
        public static void DeletePrenotazione(int id)
        {
            var db = new DAL.AperitivoEntities();
            var myPrenotazione = db.Prenotazione.Where(r => r.ID == id).FirstOrDefault();
            var myResult = db.Prenotazione.Remove(myPrenotazione);
            Interaction.MsgBox(myResult.Descrizione + " cancellata", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Cancellazione Prenotazione");
            db.SaveChanges();
        }

        public static PrenotazioneDTO MaxIDPrenotazione()
        {
            var db = new DAL.AperitivoEntities();
            var id = 0;
            try
            {
                id = db.Prenotazione.Max(i => i.ID);
            }
            catch (Exception ex)
            {
                if (ex is null) { id = 0; }
            }
            var obj = new PrenotazioneDTO()
            {
                ID = id,
                Descrizione = ""
            };

            return obj;
        }


        public static DAL.Prenotazione DTOtoEntity(PrenotazioneDetail prenotazione)
        {
            return new DAL.Prenotazione()
            {
                ID = prenotazione.ID,
                DataInizio = prenotazione.DataInizio,
                DataFine = prenotazione.DataFine,
                Descrizione = prenotazione.Descrizione,
                PrenotazioneEvento = prenotazione.PrenotazioneEvento,
                PrenotazioneRisorsa = prenotazione.PrenotazioneRisorsa,
                PrenotazioneSala = prenotazione.PrenotazioneSala
            };

        }
    }
}
