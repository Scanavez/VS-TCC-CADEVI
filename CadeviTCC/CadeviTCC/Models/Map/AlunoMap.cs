using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class AlunoMap : EntityTypeConfiguration<Aluno>
    {
        public AlunoMap()
        {
            ToTable("Aluno");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Nome).HasColumnName("Nome");
            Property(x => x.IdUsuario).HasColumnName("IdUsuario");



            HasRequired(x => x.usuario)
            .WithMany(x => x.Alunos)
            .HasForeignKey(x => x.IdUsuario);

        }
    }
}