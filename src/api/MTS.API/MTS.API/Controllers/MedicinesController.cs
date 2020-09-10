using Microsoft.AspNetCore.Mvc;
using MTS.API.Domain;
using MTS.API.Service.Medicine;
using MTS.API.Service.Medicine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MTS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicinesController(IMedicineService medicineService)
        {
            _medicineService = medicineService ?? throw new ArgumentNullException(nameof(medicineService));
        }

        // GET: api/Medicines
        [HttpGet]
        public IEnumerable<MedicineViewModel> GetMedicine()
        {
            return _medicineService.GetMedicinesViewModel();
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public IActionResult GetMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicine = _medicineService.GetMedicine(id);

            if (medicine == null)
            {
                return NotFound();
            }

            return Ok(medicine);
        }

        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine([FromRoute] int id, [FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.Id)
            {
                return BadRequest();
            }

            var existingMedicine = _medicineService.GetMedicine(id);
            if (existingMedicine == null)
            {
                return NotFound();
            }


            await _medicineService.PutMedicineAsync(medicine);

            return NoContent();
        }

        // POST: api/Medicines
        [HttpPost]
        public async Task<IActionResult> PostMedicine([FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            int id = await _medicineService.PostMedicineAsync(medicine);

            return CreatedAtAction("GetMedicine", new { id = id }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medicine = _medicineService.GetMedicine(id);
            if (medicine == null)
            {
                return NotFound();
            }

            await _medicineService.DeleteMedicineAsync(id);

            return Ok(medicine);
        }
    }
}
