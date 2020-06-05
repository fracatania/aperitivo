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
    //controller per l' oggetto Sala
    [RoutePrefix("api/sala")]
    public class SalaController : ApiController
    {
       private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SalaController));

        //metodo GET che richiama SalaManager per recuperare tutte le sale da DB
        [HttpGet]
        [Route("GetSale")]
        public IHttpActionResult GetSale()
        {
            XmlConfigurator.Configure();
            log.Info("Inizio GetSale");
                     
            try
            {
                log.Info(" Caricamento Sale");
                var result = BL.Sala.SalaManager.SalaDTOList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in GetSale" + ex.Message);
                throw ex;
            }

        }

        //metodo GET che richiama SalaManager per recuperare una sala da DB tramite ID (implementata ma non usata)
        [HttpGet]
        [Route("GetSale/{id}")]
        public IHttpActionResult DettaglioSala(int id)
        {
            log.Info("Inizio DettaglioSala");
            try
            {
                var result = BL.Sala.SalaManager.SalaDTO(id);
                log.Info("DettaglioSala ID = " + result.ID + " Nome: " + result.Nome);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in DettaglioSala" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama SalaManager per recuperare il massimo ID degli oggetti sala in DB
        [HttpGet]
        [Route("GetMaxIDSale")]
        public IHttpActionResult GetMaxIDSale()
        {
            log.Info("Inizio GetMaxIDSale");
            try
            {
                var result = BL.Sala.SalaManager.MaxIDSala();
                log.Info("GetMaxIDSale = " + result.ID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in GetMaxIDSale" + ex.Message);
                throw ex;
            }
        }

        // POST che richiama SalaManager per aggiungere una sala in DB
        [HttpPost]
        [Route("AddSala")]
        public IHttpActionResult AddSala([FromBody] BL.Sala.SalaDetail newSala)
        {
            try
            {
                log.Info("Inizio AddSala");
                string opt = BL.Sala.SalaManager.AddSala(newSala);
                if (opt == "ok")
                {
                    log.Info("Inserimento Sala " + newSala.Nome + " Posti:" + newSala.NumeroPosti + " OK");
                    return Ok();
                }
                else
                {
                    log.Info("Sala "+newSala.Nome+" già presente (addSala)");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                log.Error("Errore in AddSala" + ex.Message);
                return NotFound();
            }
        }

        // PUT per eseguire un update sugli oggetti sala (prevista ma non implementata)
        [HttpPut]
        [Route("UpdateSala/{id}")]
          public IHttpActionResult UpdateSala(int id, [FromBody]BL.Sala.SalaDetail newSala)
          {
            log.Info("Inizio UpdateSala");
            try
            {
                BL.Sala.SalaManager.UpdateSala(id, newSala.Prenotabile);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
