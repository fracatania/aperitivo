﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Sala
{
    public class SalaDetail
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public Nullable<int> NumeroPosti { get; set; }
        public bool Prenotabile { get; set; }
    }
}
