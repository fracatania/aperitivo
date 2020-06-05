using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using DAL;

/// <summary>
/// 
/// </summary>
namespace BL.Risorsa
{   //classe per gestire le diverse funzionalità dell'oggetto risorsa
    public static class RisorsaManager
    {
        //metodo per recuperare tutte le risorse da DB
        public static IEnumerable<RisorsaDTO> risorsaDTOList()
        {
            var db = new DAL.AperitivoEntities();
            var myRisorse = from b in db.Risorsa.ToList()
                        select new RisorsaDTO()
                        {
                            ID = b.ID,
                            Nome = b.Nome,
                            Cognome = b.Cognome,
                            Username = b.Username
                        };

            return myRisorse;
        }

        //metodo per recuperare una risorsa da DB utilizzando il valore ID inviato dal client
        public static RisorsaDetail risorsaDTO(int id)
        {

            var db = new DAL.AperitivoEntities();
            var myRisorsa = db.Risorsa.Where(r => r.ID == id).FirstOrDefault();
            return new RisorsaDetail()
            {
                ID = myRisorsa.ID,
                Nome = myRisorsa.Nome,
                Cognome = myRisorsa.Cognome,
                Username = myRisorsa.Username,
                Mail = myRisorsa.Mail               
            };
        }

        //metodo per recuperare una risorsa da DB utilizzando il valore nome inviato dal client
        public static IEnumerable<RisorsaDTO> RisorsaByNome (string nome)
        {

            var db = new DAL.AperitivoEntities();
            var myRisorse = from b in db.Risorsa.Where(r => r.Nome.Contains(nome)).ToList()
                            select new RisorsaDTO()
                            {
                                ID = b.ID,
                                Nome = b.Nome,
                                Cognome = b.Cognome,
                                Username = b.Username
                            };

            return myRisorse;
        }


        //metodo per recuperare una risorsa da DB utilizzando il valore cognome inviato dal client
        public static IEnumerable<RisorsaDTO> RisorsaByCognome(string cognome)
        {

            var db = new DAL.AperitivoEntities();
            var myRisorse = from b in db.Risorsa.Where(r => r.Cognome.Contains(cognome)).ToList()
                            select new RisorsaDTO()
                            {
                                ID = b.ID,
                                Nome = b.Nome,
                                Cognome = b.Cognome,
                                Username = b.Username
                            };

            return myRisorse;
        }


        //metodo per inserire una risorsa in DB utilizzando i valori inviati dal client
        public static string AddRisorsa(RisorsaDetail risorsa)
        {
            var db = new DAL.AperitivoEntities();
            var dbRisorsaMail = from p in db.Risorsa
                                select p.Mail;
                
                ;
            var dbRisorsaUsername = from p in db.Risorsa
                                    select p.Username;

            var verifica_mail = "ok";
            var verifica_username = "ok";
            foreach (var mail in dbRisorsaMail)
            {
                if (risorsa.Mail != mail)
                {
                    verifica_mail = "ok";
                }
                else
                {
                    verifica_mail = "ko";
                    break;
                }
            }

            if (verifica_mail == "ok")
            {

                foreach (var username in dbRisorsaUsername)
                {
                    if (risorsa.Username != username)
                    {
                        verifica_username = "ok";
                    }
                    else
                    {
                        verifica_username = "ko";
                        break;
                    }
                }
            }

            if ((verifica_mail == "ok") && (verifica_username == "ok"))
            {
                var myRisorsa = DTOtoEntity(risorsa);
                var result = db.Risorsa.Add(myRisorsa);
                db.SaveChanges();
                Interaction.MsgBox(myRisorsa.Nome +" " +myRisorsa.Cognome +" inserito", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Inserimento Nuova Risorsa");
                return "ok";
            }
            else
            {
                return "ko";
            }
           
        }

        //metodo per cancellare una risorsa da DB utilizzando l' oggetto RisorsaDetail (non utilizzato attualmente)
        public static void DeleteRisorsa(RisorsaDetail risorsa)
        {
            var db = new DAL.AperitivoEntities();
            var myRisorsa = DTOtoEntity(risorsa);
            var result = db.Risorsa.Add(myRisorsa);
            db.SaveChanges();

        }

        //metodo per recuperare l 'ID massimo dalla tabella risorsa da DB 
        public static RisorsaDTO MaxIDRisorsa()
        {
            var db = new DAL.AperitivoEntities();
            var id = 0;
            try
            {
                id = db.Risorsa.Max(i => i.ID);
            }
            catch (Exception ex)
            {
                if (ex is null) { id = 0; }
            }
            var obj = new RisorsaDTO()
            {
                ID = id
            };
            return obj;

        }

        public static DAL.Risorsa DTOtoEntity(RisorsaDetail risorsa)
        {
            return new DAL.Risorsa()
            {
                ID = risorsa.ID,
                Nome = risorsa.Nome,
                Cognome = risorsa.Cognome,
                Username = risorsa.Username,
                Mail = risorsa.Mail
            };

        }
    }
}
