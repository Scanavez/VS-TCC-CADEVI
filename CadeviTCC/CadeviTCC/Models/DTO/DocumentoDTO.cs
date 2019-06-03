using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CadeviTCC.Models.DTO
{
    public class DocumentoDTO
    {
        //[HiddenInput(DisplayValue = false)]
        public int IdDocumento { get; set; }

        public string Descricao { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public int? IdAluno { get; set; }
    }
}