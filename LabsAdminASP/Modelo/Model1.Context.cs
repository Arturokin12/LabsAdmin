﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LabsAdminASP.Modelo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LabsAdminEntities1 : DbContext
    {
        public LabsAdminEntities1()
            : base("name=LabsAdminEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<actividad> actividad { get; set; }
        public virtual DbSet<actividad_computador> actividad_computador { get; set; }
        public virtual DbSet<computadora> computadora { get; set; }
        public virtual DbSet<laboratorio> laboratorio { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<config> config { get; set; }
    }
}
