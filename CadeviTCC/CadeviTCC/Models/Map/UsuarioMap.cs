using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuario");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Login).HasColumnName("Login");
            Property(x => x.Senha).HasColumnName("Senha");
            Property(x => x.Nome).HasColumnName("Nome");
            Property(x => x.IdTipoUsuario).HasColumnName("IdTipoUsuario");

            HasRequired(x => x.tipoUsuario)
                .WithMany(x => x.Usuarios)
                .HasForeignKey(x => x.IdTipoUsuario);

            

        }

    }
}