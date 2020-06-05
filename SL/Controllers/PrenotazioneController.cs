using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using log4net;
using log4net.Config;

namespace SL.Controllers
{
    //controller per Prenotazioni
    [RoutePrefix("api/prenotazione")]
    public class PrenotazioneController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(PrenotazioneController));
        //metodo GET che richiama PrenotazioneManager per recuperare tutte le prenotazioni da DB
        [HttpGet]
        [Route("GetPrenotazioni")]

        public IHttpActionResult GetPrenotazioni()
        {
            log.Info("Inizio API GetPrenotazioni");
            try
            {
                log.Info("Caricamento GetPrenotazioni");
                var result = BL.Prenotazione.PrenotazioneManager.PrenotazioneDTOList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API GetPrenotazioni" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama PrenotazioneManager per recuperare un evento da DB tramite ID prenotazione
        [HttpGet]
        [Route("GetPrenotazione/{id}")]
        public IHttpActionResult DettaglioPrenotazione(int id)
        {
            log.Info("Inizio API DettaglioPrenotazione");
            try
            {
                var result = BL.Prenotazione.PrenotazioneManager.PrenotazioneDTO(id);
                log.Info("DettaglioPrenotazione ID = "+result.ID+ " descrizione: " + result.Descrizione);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API DettaglioPrenotazione" + ex.Message);
                throw ex;
             }
        }

        //metodo GET che richiama PrenotazioneManager per recuperare il massimo ID degli oggetti prenotazione in DB
        [HttpGet]
        [Route("GetMaxIDPrenotazione")]
        public IHttpActionResult GetMaxIDPrenotazione()
        {
            log.Info("Inizio API GetMaxIDPrenotazione");
            try
            {
                var result = BL.Prenotazione.PrenotazioneManager.MaxIDPrenotazione();
                log.Info("GetMaxIDPrenotazione = "+result.ID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API GetMaxIDPrenotazione" + ex.Message);
                throw ex;
            }
        }

        // POST che richiama PrenotazioneManager per aggiungere una prenotazione in DB
        [HttpPost]
        [Route("AddPrenotazione")]
        public IHttpActionResult AddPrenotazione([FromBody] BL.Prenotazione.PrenotazioneDetail newPrenotazione)
        {
            log.Info("Inizio API AddPrenotazione");
            try
            {
                string opt = BL.Prenotazione.PrenotazioneManager.AddPrenotazione(newPrenotazione);
                if (opt == "ok") {
                    log.Info("Inserimento Prenotazione "+newPrenotazione.Descrizione +" OK");
                    return Ok("inserito");
                }
                else
                {
                    log.Info("Prenotazione non possibile per sala (ID Sala):" + newPrenotazione.PrenotazioneSala +" già occupata (AddPrenotazione)");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error("Errore in API AddPrenotazione" + ex.Message);
                return NotFound();
            }
        }

        // PUT per eseguire un update sugli oggetti prenotazione (prevista ma non implementata)
        [HttpPut]
        [Route("UpdatePrenotazione/{id}")]
        public IHttpActionResult UpdatePrenotazione(int id, [FromBody]BL.Prenotazione.PrenotazioneDetail newPrenotazione)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }


        // DELETE per cancellare un oggetto prenotazione da db
        [HttpDelete]
        [Route("DeletePrenotazione/{id}")]
        public IHttpActionResult DeletePrenotazione(int id, [FromBody]BL.Prenotazione.PrenotazioneDetail newPrenotazione)
        {
            log.Info("Inizio API DeletePrenotazione");
            try
            {
                BL.Prenotazione.PrenotazioneManager.DeletePrenotazione(id);
                log.Info("Prenotazione ID:"+id+" cancellata");
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Errore in API DeletePrenotazione" + ex.Message);
                return NotFound();
            }
        }
    }
}
