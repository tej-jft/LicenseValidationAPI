using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LicenseValidationAPI.Model
{
    public class LicenseValidationModel
    {
        public long id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime StartDate { get; set; }
        public string LicenseStatus { get; set; }
    }
}
