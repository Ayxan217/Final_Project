﻿using FinalProject.Application.Abstractions.Services;
using FinalProject.Application.DTOs.Patient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {

        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int page = 1,int take = 3)
        {
            var patients = await _patientService.GetAllAsync(page,take);
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var patient = await _patientService.GetByIdAsync(id);
            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePatientDto createPatientDto)
        {
             await _patientService.CreateAsync(createPatientDto);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdatePatientDto updatePatientDto)
        {
            if (id < 1)
                return BadRequest();
            await _patientService.UpdateAsync(id,updatePatientDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _patientService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string searchTerm)
        {
            var patients = await _patientService.SearchAsync(searchTerm);
            return Ok(patients);
        }
    }
}
