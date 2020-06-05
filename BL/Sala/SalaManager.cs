using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using log4net;
using log4net.Config;
namespace BL.Sala
{
    //classe per gestire le diverse funzionalità dell'oggetto sala
    public static class SalaManager
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SalaManager));

        //metodo per recuperare tutte le sale da DB
        public static IEnumerable<SalaDetail> SalaDTOList()
        {
            log.Info("SalaManager SalaDTOList");

            var db = new DAL.AperitivoEntities();
            var mySala = from b in db.Sala.ToList()
                            select new SalaDetail()
                            {
                                ID = b.ID,
                                Nome = b.Nome,
                                NumeroPosti = b.NumeroPosti,
                                Prenotabile = b.Prenotabile
                            };

            return mySala;
        }

        //metodo per recuperare una sala da DB utilizzando il valore ID inviato dal client
        public static SalaDetail SalaDTO(int id)
        {
            var db = new DAL.AperitivoEntities();
            var mySala = db.Sala.Where(s => s.ID == id).FirstOrDefault();
            return new SalaDetail()
            {
                ID = mySala.ID,
                Nome = mySala.Nome,
                NumeroPosti = mySala.NumeroPosti,
                Prenotabile = mySala.Prenotabile
             };
        }

        //metodo per inserire una prenotazione in DB utilizzando i valori inviati dal client,
        public static string AddSala(SalaDetail sala)
        {
            var db = new DAL.AperitivoEntities();

            var nomeSala = db.Sala.Where(s => s.Nome == sala.Nome).FirstOrDefault(); 
            if (nomeSala is null) { 
               var mySala = DTOtoEntity(sala);
              var result = db.Sala.Add(mySala);
                Interaction.MsgBox("Sala '" + sala.Nome + "' inserita", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Inserimento Nuova Sala");
                db.SaveChanges();
                return "ok";
            }
            else
            {
                Interaction.MsgBox("Nome Sala '" + sala.Nome +"' già presente",MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Inserimento Nuova Sala");
                return "ko";
            }
        }

        //metodo per cancellare una sala da DB utilizzando l' oggetto SalaDetail (non utilizzato attualmente)
        public static void DeleteSala(SalaDetail sala)
        {
            var db = new DAL.AperitivoEntities();
            var mySala = DTOtoEntity(sala);
            var result = db.Sala.Add(mySala);
            db.SaveChanges();
        }

        //metodo per settare una sala da disponible o meno alla prenotazione di un evento
        public static void UpdateSala(int id, bool value)
        {
            var nome = "";
            var db = new DAL.AperitivoEntities();
            var query =
                from s in db.Sala
                where s.ID == id
                select s;

            foreach (DAL.Sala sala in query)
            {
                nome = sala.Nome.ToString();
                sala.Prenotabile = value;
            }

                Interaction.MsgBox("Sala '" + nome +"' aggiornata", MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "Prenotabilità Sala");
                db.SaveChanges();

        }

        //metodo per recuperare l 'ID massimo dalla tabella sala da DB 
        public static SalaDTO MaxIDSala()
        {
            var db = new DAL.AperitivoEntities();
            var id = 0;
            try { 
                id = db.Sala.Max(i => i.ID);
                }
            catch (Exception ex)
            {
                if (ex is null) { id = 0; }
            }
            var obj = new SalaDTO()
            {
                ID = id,
                Nome = "",
                NumeroPosti = 0

            };
           
            return obj;
        }

        public static DAL.Sala DTOtoEntity(SalaDetail sala)
        {
            return new DAL.Sala()
            {
                ID = sala.ID,
                Nome = sala.Nome,
                NumeroPosti = sala.NumeroPosti,
                Prenotabile = sala.Prenotabile
               
            };

        }
    }
}
