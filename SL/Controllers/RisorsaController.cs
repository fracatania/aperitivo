using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BL;
using log4net;
using log4net.Config;

namespace SL.Controllers
{
    //controller per Risorsa
    [RoutePrefix("api/risorsa")]
    public class RisorsaController : ApiController
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(RisorsaController));


        //metodo GET che richiama RisorsaManager per recuperare tutte le risorse da DB
        [HttpGet]
        [Route("GetRisorse")]
        public IHttpActionResult GetRisorse()
        {
            log.Info("Inizio API GetRisorse");
            try
            {
                log.Info(" Caricamento Risorse");
                var result = BL.Risorsa.RisorsaManager.risorsaDTOList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in API GetRisorse" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama RisorsaManager per recuperare una risorsa da DB tramite ID risorsa
        [HttpGet]
        [Route("GetRisorse/{id}")]
        public IHttpActionResult DettaglioRisorsa(int id)
        {
            log.Info("Inizio API DettaglioRisorsa");
            try
            {
                var result = BL.Risorsa.RisorsaManager.risorsaDTO(id);
                log.Info("DettaglioRisorsa ID = " + result.ID + " Nome: " + result.Nome + " Cognome: " + result.Cognome);
                return Ok(result);
             }
            catch (Exception ex)
            {
                log.Error("Errore in  API DettaglioRisorsa" + ex.Message);
                throw ex;
            }
        }

        //metodo GET che richiama RisorsaManager per recuperare una risorsa da DB tramite Nome risorsa
        [HttpGet]
        [Route("GetRisorseNome/{nome}")]
        public IHttpActionResult NomeRisorsa(string nome)
        {
            log.Info("Inizio API GetRisorseNome: "+ nome);
            try
            {
                var result = BL.Risorsa.RisorsaManager.RisorsaByNome(nome);
                foreach (BL.Risorsa.RisorsaDTO valore in result)
                {
                    log.Info("GetRisorseNome Nome = " + valore.Nome);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in  API GetRisorseNome" + ex.Message);
                throw ex;
            }
        }


        //metodo GET che richiama RisorsaManager per recuperare una risorsa da DB tramite Cognome risorsa
        [HttpGet]
        [Route("GetRisorseCognome/{cognome}")]
        public IHttpActionResult CognomeRisorsa(string cognome)
        {
            log.Info("Inizio API GetRisorseCognome: "+ cognome);
            try
            {
                var result = BL.Risorsa.RisorsaManager.RisorsaByCognome(cognome);
                foreach (BL.Risorsa.RisorsaDTO valore in  result) { 
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


        //metodo GET che richiama RisorsaManager per recuperare il massimo ID degli oggetti evento in DB
        [HttpGet]
        [Route("GetMaxIDRisorse")]
        public IHttpActionResult GetMaxIDRisorse()
        {
            log.Info("Inizio API GetMaxIDRisorse");
            try
            {
                var result = BL.Risorsa.RisorsaManager.MaxIDRisorsa();
                log.Info("GetMaxIDRisorse = " + result.ID);
                return Ok(result);
            }
            catch (Exception ex)
            {
                log.Error("Errore in  API GetMaxIDRisorse" + ex.Message);
                throw ex;
            }
        }

        // POST che richiama RisorsaManager per aggiungere un evento in DB
        [HttpPost]
        [Route("AddRisorsa")]
        public IHttpActionResult AddRisorsa([FromBody] BL.Risorsa.RisorsaDetail newrisorsa)
        {
            log.Info("Inizio API AddRisorsa");
            try
            {
                string opt = BL.Risorsa.RisorsaManager.AddRisorsa(newrisorsa);
                if (opt == "ok")
                {
                    log.Info("Inserimento Risorsa "+ newrisorsa.Cognome + " " + newrisorsa.Nome+ " OK");
                    return Ok();
                }
                else
                {
                    log.Info("Mail o Username della risorsa " + newrisorsa.Cognome + " "+ newrisorsa.Nome + " già presente (AddRisorsa)");
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                log.Error("Errore in  API AddRisorsa" + ex.Message);
                return NotFound();
            }
        }

    }
}