﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Companies.API.Data;
using Companies.API.Entities;
using Companies.API.Dtos.CompaniesDtos;
using AutoMapper;

namespace Companies.API.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly APIContext _context;
        private readonly IMapper mapper;

        public CompaniesController(APIContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Companies
        [HttpGet(Name = "RouteName")]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany(bool includeEmployees = false)
        {
            // Don't work!!
            //var dtos = includeEmployees ? mapper.ProjectTo<CompanyDto>(_context.Companies.Include(c => c.Employees)) :
            //                              mapper.ProjectTo<CompanyDto>(_context.Companies);

            var dtos = includeEmployees ? mapper.Map<IEnumerable<CompanyDto>>(await _context.Companies.Include(c => c.Employees).ThenInclude(e => e.Department).ToListAsync()) :
                                          mapper.Map<IEnumerable<CompanyDto>>(await GetAsync());

            return Ok(dtos);
        }

        private async Task<List<Company>> GetAsync()
        {
            return await _context.Companies.ToListAsync();
        }
        private async Task<Company?> GetAsync(Guid id)
        {
            return await _context.Companies.FirstOrDefaultAsync(c => c.Id == id);
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            Company? company = await GetAsync(id);

            if (company == null)
            {
                return NotFound();
            }

            //var companyDto = new CompanyDto
            //{
            //    Id = company.Id,
            //    Name = company.Name,
            //    Address = company.Address,
            //    //Country = company.Country,
            //};

            var companyDto = mapper.Map<CompanyDto>(company);

            return Ok(companyDto);
        }


        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(Guid id, CompanyForUpdateDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var existingCompany = await GetAsync(id);

            if (existingCompany == null) return NotFound();

            mapper.Map(dto, existingCompany);
            await _context.SaveChangesAsync();

            return Ok(mapper.Map<CompanyDto>(existingCompany)); //Only for demo
            //return NoContent();
        }

        //// POST: api/Companies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyForCreationDto dto)
        {

            //var company = new Company
            //{
            //    Name = dto.Name,
            //    Address = dto.Address!,
            //    Country = dto.Country!,
            //};

            var company = mapper.Map<Company>(dto);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            //var companyToReturn = new CompanyDto
            //{
            //    Id = company.Id,
            //    Name = company.Name,
            //    Address = company.Address,
            //    //Country = company.Country,
            //};

            var companyToReturn = mapper.Map<CompanyDto>(company);

            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyToReturn);
        }

        //// DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var company = await GetAsync(id);
            if (company == null)
            {
                return NotFound();
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //private bool CompanyExists(Guid id)
        //{
        //    return _context.Companies.Any(e => e.Id == id);
        //}
    }
}
