﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace perfume.fume
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class perfumeShopEntities : DbContext
    {
        public perfumeShopEntities()
            : base("name=perfumeShopEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<fume_CartStatus> fume_CartStatus { get; set; }
        public virtual DbSet<fume_Category> fume_Category { get; set; }
        public virtual DbSet<fume_MemberRole> fume_MemberRole { get; set; }
        public virtual DbSet<fume_Members> fume_Members { get; set; }
        public virtual DbSet<fume_Product> fume_Product { get; set; }
        public virtual DbSet<fume_ShippingDetails> fume_ShippingDetails { get; set; }
        public virtual DbSet<Tbl_Cart> Tbl_Cart { get; set; }
        public virtual DbSet<Tbl_Roles> Tbl_Roles { get; set; }
        public virtual DbSet<Tbl_SlideImage> Tbl_SlideImage { get; set; }
        public virtual DbSet<RegisterUser> RegisterUsers { get; set; }
        public virtual DbSet<Tbl_Admin> Tbl_Admin { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
    }
}
