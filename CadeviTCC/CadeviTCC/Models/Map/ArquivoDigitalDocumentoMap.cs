using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class ArquivoDigitalDocumentoMap : EntityTypeConfiguration<ArquivoDigitalDocumento>
    {
        public ArquivoDigitalDocumentoMap()
        {
            ToTable("ArqDigitalDocumento");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");

            Property(x => x.NomeArquivo).HasColumnName("NomeArquivo");

            Property(x => x.Arquivo).HasColumnName("Arquivo");

            Property(x => x.IdDocumento).HasColumnName("IdDocumento");


            HasRequired(x => x.documento)
            .WithMany(x => x.arquivoDigitalDocumentos)
            .HasForeignKey(x => x.IdDocumento);
        }

    }
}