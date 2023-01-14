using DOTNETCOREEXAMPLE.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETCOREEXAMPLE.DataContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        }
        public virtual DbSet<Class_PROJECT_USER_MASTER> obj_PROJECT_USER_MASTER { get; set; }
        public virtual DbSet<Class_PATIENT_MASTER> obj_PATIENT_MASTER { get; set; }
        public virtual DbSet<Class_REGISTRATION_TYPE> obj_REGISTRATION_TYPE { get; set; }
        public virtual DbSet<Class_SLOT_MASTER> obj_SLOT_MASTER { get; set; }
        public virtual DbSet<Class_SERVICE_GROUP_MASTER> obj_SERVICE_GROUP_MASTER { get; set; }
        public virtual DbSet<Class_SERVICE_MASTER> obj_SERVICE_MASTER { get; set; }
        public virtual DbSet<Class_VISIT_TYPE_MASTER> obj_VISIT_TYPE_MASTER { get; set; }
        public virtual DbSet<Class_SLOT_BOOKING_MASTER> obj_SLOT_BOOKING_MASTER { get; set; }
        public virtual DbSet<Class_OTP_MASTER> obj_OTP_MASTER { get; set; }
        public virtual DbSet<Class_SERVICE_PROVIDER_MASTER> obj_SERVICE_PROVIDER_MASTER { get; set; }
        public virtual DbSet<Class_CONTACT_US_MASTER> obj_CONTACT_US_MASTER { get; set; }
        public virtual DbSet<Class_Price_Rate_Master> obj_Price_Rate_Master { get; set; }
        public virtual DbSet<Class_Offer_Master> obj_Offer_Master { get; set; }
        public virtual DbSet<Class_Location_Master> obj_Location_Master { get; set; }
        public virtual DbSet<Class_Slot_Booking_Details> obj_Slot_Booking_Details { get; set; }
        public virtual DbSet<Class_SLOT_BOOKING_OFFER_DETAILS> obj_SLOT_BOOKING_OFFER_DETAILS { get; set; }

        public DbSet<DOTNETCOREEXAMPLE.Models.Class_Price_Rate_Master> Class_Price_Rate_Master { get; set; }
        public DbSet<DOTNETCOREEXAMPLE.Models.Class_Machine_Master> obj_Machine_Master { get; set; }
        public DbSet<DOTNETCOREEXAMPLE.Models.Class_SERVICE_FILES_DETAILS> Class_SERVICE_FILES_DETAILS { get; set; }
        public DbSet<DOTNETCOREEXAMPLE.Models.Class_SERVICE_FILES_DETAILS> obj_SERVICE_FILES_DETAILS { get; set; }
        public DbSet<DOTNETCOREEXAMPLE.Models.Class_REPORT_FILES_DETAILS> obj_REPORT_FILES_DETAILS { get; set; }
        public DbSet<DOTNETCOREEXAMPLE.Models.Class_Offer_Option_Master> obj_Offer_Option_Master { get; set; }
        //   public virtual DbSet<SlotBookingMasterViewModel> slotBookingMasterViewModel { get; set; }

    }
}
