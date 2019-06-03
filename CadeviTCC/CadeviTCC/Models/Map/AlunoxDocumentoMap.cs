using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class AlunoxDocumentoMap : EntityTypeConfiguration<AlunoxDocumento>
    {
        public AlunoxDocumentoMap()
        {
            ToTable("alunoxdocumento");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.IdAluno).HasColumnName("aluno_Id");
            Property(x => x.IdDocumento).HasColumnName("documento_Id");

            HasRequired(x => x.Aluno)
                .WithMany(x => x.AlunoXDocumento)
                .HasForeignKey(x => x.IdAluno);

            HasRequired(x => x.Documento)
                .WithMany(x => x.AlunoxDocumento)
                .HasForeignKey(x => x.IdDocumento);


        }
    }
}