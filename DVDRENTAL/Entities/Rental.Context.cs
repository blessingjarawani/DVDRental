﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DVDRENTAL.Entities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class rentalEntities : DbContext
    {
        public rentalEntities()
            : base("name=rentalEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<actor> actors { get; set; }
        public virtual DbSet<client> clients { get; set; }
        public virtual DbSet<copy> copies { get; set; }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<movy> movies { get; set; }
        public virtual DbSet<rental> rentals { get; set; }
    }
}
