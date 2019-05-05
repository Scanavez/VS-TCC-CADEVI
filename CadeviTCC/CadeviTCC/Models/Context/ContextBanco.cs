﻿using CadeviTCC.Models.Context.Entities;
using CadeviTCC.Models.Entities;
using CadeviTCC.Models.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace CadeviTCC.Models.Context
{
    public class ContextBanco : DbContext
    {

        public DbSet<Documento> Documentos { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Aluno> Alunos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add<Usuario>(new UsuarioMap());

            modelBuilder.Configurations.Add<TipoUsuario>(new TipoUsuarioMap());

            modelBuilder.Configurations.Add<Documento>(new DocumentoMap());

            modelBuilder.Configurations.Add<Aluno>(new AlunoMap());

            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<CadeviTCC.Models.Entities.TipoUsuario> TipoUsuarios { get; set; }
    }
}