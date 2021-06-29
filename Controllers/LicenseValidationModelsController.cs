using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicenseValidationAPI.Model;
using System.IO;
using System.Text;

namespace LicenseValidationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseValidationModelsController : ControllerBase
    {
        private readonly LicenseValidationContext _context;

        public LicenseValidationModelsController(LicenseValidationContext context)
        {
            _context = context;
        }

        // GET: api/LicenseValidationModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LicenseValidationModel>>> GetlicenseValidationItems()
        {
            return await _context.licenseValidationItems.ToListAsync();
        }

        // GET: api/LicenseValidationModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LicenseValidationModel>> GetLicenseValidationModel(long id)
        {
            var licenseValidationModel = await _context.licenseValidationItems.FindAsync(id);

            if (licenseValidationModel == null)
            {
                return NotFound();
            }

            return licenseValidationModel;
        }

        // PUT: api/LicenseValidationModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLicenseValidationModel(long id, LicenseValidationModel licenseValidationModel)
        {
            if (id != licenseValidationModel.id)
            {
                return BadRequest();
            }

            _context.Entry(licenseValidationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseValidationModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //POST: api/LicenseValidationModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostLicenseValidationModel(String LicenseNumber)
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                LicenseNumber= await reader.ReadToEndAsync();
            }
            string status=await Task.Run(() => DBManager.getLicenseStatus(LicenseNumber));
             return status;
        }

        // DELETE: api/LicenseValidationModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenseValidationModel(long id)
        {
            var licenseValidationModel = await _context.licenseValidationItems.FindAsync(id);
            if (licenseValidationModel == null)
            {
                return NotFound();
            }

            _context.licenseValidationItems.Remove(licenseValidationModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LicenseValidationModelExists(long id)
        {
            return _context.licenseValidationItems.Any(e => e.id == id);
        }
    }
}
