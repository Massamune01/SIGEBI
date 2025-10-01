﻿using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities.Configuration
{
    public class Libro
    {
        public int ISBN { get; set; }
        public string titulo { get; set; }
        public string autor {  get; set; }
        public string editorial { get; set; }
        public int añoPublicacion { get; set; }
        public string categoria { get; set; }
        public int numPaginas { get; set; }
        public int cantidad { get; set; }
        public bool disponible { get; set; }

        public Status Status { get; set; }  
        public int? IdLgOpLibro { get; set; }
    }
}
