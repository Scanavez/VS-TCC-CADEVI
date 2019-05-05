using CadeviTCC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace CadeviTCC.Models.Map
{
    public class TipoUsuarioMap : EntityTypeConfiguration<TipoUsuario>
    {
        public TipoUsuarioMap()
        {
            ToTable("TipoUsuario");

            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("idTipoUsuario");
            Property(x => x.Descricao).HasColumnName("Descricao");

        }
    }
}