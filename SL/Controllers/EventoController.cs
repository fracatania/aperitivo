using System;
using System.Web.Http;
using log4net;
using log4net.Config;

namespace SL.Controllers
{
    //controller per Evento
    [RoutePrefix("api/evento")]
    public class EventoController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(EventoController));

        //metodo GET che richiama EventoManager per recuperare tutti gli eventi da DB
        [HttpGet]
        [Route("GetEventi")]
         public IHttpActionResult GetEventi()
        {
            log.Info("Inizio API GetEventi");
            try
            {
                log.Info("Caricamento GetEventi");
                var result = BL.Evento.EventoManager.EventoDTOList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API GetEventi" + ex.Message);
                throw ex;
            }
        }


        //metodo GET che richiama EventoManager per recuperare un evento da DB tramite ID evento
        [HttpGet]
        [Route("GetEvento/{id}")]
        public IHttpActionResult DettaglioEvento(int id)
        {
            log.Info("Inizio API DettaglioEvento");
            try
            {
                var result = BL.Evento.EventoManager.EventoDTO(id);
                log.Info("DettaglioEvento ID = " + result.ID + " descrizione: " + result.Descrizione);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API DettaglioEvento" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama EventoManager per recuperare gli eventi da DB tramite descrizione evento
        [HttpGet]
        [Route("GetEventoDescrizione/{descrizione}")]
        public IHttpActionResult GetEventoDescrizione(string descrizione)
        {
            log.Info("Inizio API GetEventoDescrizione: " + descrizione);
            try
            {
                var result = BL.Evento.EventoManager.EventoByDescrizione(descrizione);
                foreach (BL.Evento.EventoDTO valore in result)
                {
                    log.Info("GetEventoDescrizione Descrizione = " + valore.Descrizione);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in  API GetEventoDescrizione" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama EventoManager per recuperare gli eventi da DB tramite anno evento
        [HttpGet]
        [Route("GetEventoAnno/{anno}")]
        public IHttpActionResult GetEventoAnno(string anno)
        {
            log.Info("Inizio API GetEventoAnno: " + anno);
            try
            {
                var result = BL.Evento.EventoManager.EventoByDescrizione(anno);
                foreach (BL.Evento.EventoDTO valore in result)
                {
                    log.Info("GetEventoAnno Descrizione = " + valore.Anno);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in  API GetEventoAnno" + ex.Message);
                throw ex;
            }
        }


        //metodo GET che richiama RisorsaManager per recuperare una risorsa da DB tramite Cognome risorsa
        [HttpGet]
        [Route("GetEventoAnno/{anno}")]
        public IHttpActionResult CognomeRisorsa(string cognome)
        {
            log.Info("Inizio API GetRisorseCognome: " + cognome);
            try
            {
                var result = BL.Risorsa.RisorsaManager.RisorsaByCognome(cognome);
                foreach (BL.Risorsa.RisorsaDTO valore in result)
                {
                    log.Info("GetRisorseCognome Cognome = " + valore.Cognome);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in  API GetRisorseCognome" + ex.Message);
                throw ex;
            }
        }



        //metodo GET che richiama EventoManager per recuperare il massimo ID  degli oggetti evento in DB
        [HttpGet]
        [Route("GetMaxIDEvento")]
        public IHttpActionResult GetMaxIDEvento()
        {
            log.Info("Inizio API GetMaxIDEvento");
            try
            {
                var result = BL.Evento.EventoManager.MaxIDEvento();
                log.Info("GetMaxIDEvento = " + result.ID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API GetMaxIDPrenotazione" + ex.Message);
                throw ex;
            }
        }


        // POST per aggiungere un evento in DB
        [HttpPost]
        [Route("AddEvento")]
        public IHttpActionResult AddEvento([FromBody] BL.Evento.EventoDetail newEvento)
        {
            log.Info("Inizio API AddPrenotazione");
            try
            {
                BL.Evento.EventoManager.AddEvento(newEvento);
                log.Info("Inserimento Evento " + newEvento.Descrizione + " OK");
                return Ok();
            }
            catch (Exception ex)
            {
                log.Error("Errore in API AddEvento" + ex.Message);
                return NotFound();
            }
        }


        // PUT per eseguire un update sugli oggetti evento (prevista ma non implementata)
        [HttpPut]
        [Route("UpdateEvento/{id}")]
        public IHttpActionResult UpdateEvento(int id, [FromBody]BL.Evento.EventoDetail newEvento)
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

        // DELETE per cancellare un oggetto evento da db
        [HttpDelete]
        [Route("DeleteEvento/{id}")]
        public IHttpActionResult DeleteEvento(int id, [FromBody]BL.Evento.EventoDetail newEvento)
        {
            log.Info("Inizio API DeleteEvento");
            try
            {
                 BL.Evento.EventoManager.DeleteEvento(id);
                log.Info("Evento ID:" + id + " cancellato");
                return Ok("Cancellato");
            }
            catch (Exception ex)
            {
                log.Error("Errore in API DeletePrenotazione" + ex.Message);
                return NotFound();
            }
        }
    }
}
