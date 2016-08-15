﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPW.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using SPW.Model;
    
    public partial class SPWEntities : DbContext
    {
        public SPWEntities()
            : base("name=SPWEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<ACCOUNT_MAST> ACCOUNT_MAST { get; set; }
        public DbSet<AP_VEHICLE_TRANS> AP_VEHICLE_TRANS { get; set; }
        public DbSet<ASSET_TYPE> ASSET_TYPE { get; set; }
        public DbSet<CATEGORY> CATEGORY { get; set; }
        public DbSet<COLOR> COLOR { get; set; }
        public DbSet<COLOR_TYPE> COLOR_TYPE { get; set; }
        public DbSet<DELIVERY_INDEX> DELIVERY_INDEX { get; set; }
        public DbSet<DELIVERY_INDEX_DETAIL> DELIVERY_INDEX_DETAIL { get; set; }
        public DbSet<DELIVERY_ORDER> DELIVERY_ORDER { get; set; }
        public DbSet<DELIVERY_ORDER_DETAIL> DELIVERY_ORDER_DETAIL { get; set; }
        public DbSet<DEPARTMENT> DEPARTMENT { get; set; }
        public DbSet<EMP_GRADE_SET> EMP_GRADE_SET { get; set; }
        public DbSet<EMP_MEASURE_DT_TEMPLATE> EMP_MEASURE_DT_TEMPLATE { get; set; }
        public DbSet<EMP_MEASURE_HD_TEMPLATE> EMP_MEASURE_HD_TEMPLATE { get; set; }
        public DbSet<EMP_MEASURE_TRANS> EMP_MEASURE_TRANS { get; set; }
        public DbSet<EMP_MEASURE_WEIGHT> EMP_MEASURE_WEIGHT { get; set; }
        public DbSet<EMP_POSITION> EMP_POSITION { get; set; }
        public DbSet<EMP_SKILL> EMP_SKILL { get; set; }
        public DbSet<EMP_SKILL_TYPE> EMP_SKILL_TYPE { get; set; }
        public DbSet<EMPLOYEE> EMPLOYEE { get; set; }
        public DbSet<EMPLOYEE_HIST> EMPLOYEE_HIST { get; set; }
        public DbSet<FUNCTION> FUNCTION { get; set; }
        public DbSet<ORDER> ORDER { get; set; }
        public DbSet<ORDER_DETAIL> ORDER_DETAIL { get; set; }
        public DbSet<PAYIN_APPROVER> PAYIN_APPROVER { get; set; }
        public DbSet<PAYIN_TRANS> PAYIN_TRANS { get; set; }
        public DbSet<PO_DT_TRANS> PO_DT_TRANS { get; set; }
        public DbSet<PO_HD_TRANS> PO_HD_TRANS { get; set; }
        public DbSet<PR_DT_TRANS> PR_DT_TRANS { get; set; }
        public DbSet<PR_HD_TRANS> PR_HD_TRANS { get; set; }
        public DbSet<PRODUCT> PRODUCT { get; set; }
        public DbSet<PRODUCT_PRICELIST> PRODUCT_PRICELIST { get; set; }
        public DbSet<PRODUCT_PROMOTION> PRODUCT_PROMOTION { get; set; }
        public DbSet<PROVINCE> PROVINCE { get; set; }
        public DbSet<RAW_PACK_PRICE_HIST> RAW_PACK_PRICE_HIST { get; set; }
        public DbSet<RAW_PACK_SIZE> RAW_PACK_SIZE { get; set; }
        public DbSet<RAW_PRODUCT> RAW_PRODUCT { get; set; }
        public DbSet<RAW_TYPE> RAW_TYPE { get; set; }
        public DbSet<RECEIVE_RAW_TRANS> RECEIVE_RAW_TRANS { get; set; }
        public DbSet<ROAD> ROAD { get; set; }
        public DbSet<ROLE> ROLE { get; set; }
        public DbSet<ROLE_FUNCTION> ROLE_FUNCTION { get; set; }
        public DbSet<SECTOR> SECTOR { get; set; }
        public DbSet<STOCK_PRODUCT_COLOR> STOCK_PRODUCT_COLOR { get; set; }
        public DbSet<STOCK_PRODUCT_STOCK> STOCK_PRODUCT_STOCK { get; set; }
        public DbSet<STOCK_PRODUCT_TRANS> STOCK_PRODUCT_TRANS { get; set; }
        public DbSet<STOCK_PRODUCT_WITHDRAW_TRANS> STOCK_PRODUCT_WITHDRAW_TRANS { get; set; }
        public DbSet<STOCK_RAW_SETTING> STOCK_RAW_SETTING { get; set; }
        public DbSet<STOCK_RAW_STOCK> STOCK_RAW_STOCK { get; set; }
        public DbSet<STOCK_RAW_TRANS> STOCK_RAW_TRANS { get; set; }
        public DbSet<STOCK_TYPE> STOCK_TYPE { get; set; }
        public DbSet<STORE> STORE { get; set; }
        public DbSet<SUB_DELIVERY_INDEX> SUB_DELIVERY_INDEX { get; set; }
        public DbSet<SUB_DELIVERY_INDEX_DETAIL> SUB_DELIVERY_INDEX_DETAIL { get; set; }
        public DbSet<SUB_DELIVERY_ORDER> SUB_DELIVERY_ORDER { get; set; }
        public DbSet<SUB_DELIVERY_ORDER_DETAIL> SUB_DELIVERY_ORDER_DETAIL { get; set; }
        public DbSet<SUB_FUNCTION> SUB_FUNCTION { get; set; }
        public DbSet<TRANSPORT_LINE> TRANSPORT_LINE { get; set; }
        public DbSet<UNIT_TYPE> UNIT_TYPE { get; set; }
        public DbSet<USER> USER { get; set; }
        public DbSet<VEHICLE> VEHICLE { get; set; }
        public DbSet<VENDOR> VENDOR { get; set; }
        public DbSet<VENDOR_DEAL_DISCOUNT> VENDOR_DEAL_DISCOUNT { get; set; }
        public DbSet<ZONE> ZONE { get; set; }
        public DbSet<ZONE_DETAIL> ZONE_DETAIL { get; set; }
    }
}
