﻿using System;
using System.Collections.Generic;

namespace ProyectoCRM.Models3
{
    public partial class TareasSinCerrar
    {
        public DateTime FechaCreacion { get; set; }
        public string Informacion { get; set; } = null!;
        public string Nombre { get; set; } = null!;
    }
}