using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Context.Entities
{
    [Table("Documento")]
    public class Documento
    {
        public long Id { get; set; }

        public string Descricao { get; set; }

        public DateTime HoraRegistro { get; set; }
    }
}