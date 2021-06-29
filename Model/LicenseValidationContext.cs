using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseValidationAPI.Model
{
    public class LicenseValidationContext:DbContext
    {
        public LicenseValidationContext(DbContextOptions<LicenseValidationContext> options) : base(options)
        {

        }
        public DbSet<LicenseValidationModel> licenseValidationItems { get; set; }
    }
}
