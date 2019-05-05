using CadeviTCC.Models.Context.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class DocumentoMap : EntityTypeConfiguration<Documento>
    {
        public DocumentoMap()
        {
            ToTable("Documento");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");

            Property(x => x.Descricao).HasColumnName("Descricao");

            Property(x => x.HoraRegistro).HasColumnName("HoraRegistro");
        }

    }
}