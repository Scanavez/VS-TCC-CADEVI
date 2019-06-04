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

            //Property(x => x.IdDocumento).HasColumnName("IdDocumento");

            Property(x => x.IdAlunoXDocumento).HasColumnName("IdAlunoXDocumento");

            HasRequired(x => x.alunoxDocumento)
            .WithMany(x => x.ArquivoDigitalDocumentos)
            .HasForeignKey(x => x.IdAlunoXDocumento);
        }

    }
}