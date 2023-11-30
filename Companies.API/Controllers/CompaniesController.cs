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
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompany()
        {
            //var companies = _context.Companies;

            //var companyDtos = companies.Select(c => new CompanyDto
            //{
            //    Name = c.Name,
            //    Address = c.Address,
            //    Id = c.Id,
            //   // Country = c.Country,
            //});

            return Ok(await mapper.ProjectTo<CompanyDto>(_context.Companies).ToListAsync());
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(Guid id)
        {
            var company = await _context.Companies.FindAsync(id);

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
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCompany(Guid id, Company company)
        //{
        //    if (id != company.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(company).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CompanyExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Companies
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(CompanyForCreationDto dto)
        {

            var company = new Company
            {
                Name = dto.Name,
                Address = dto.Address!,
                Country = dto.Country!,
            };

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();

            var companyToReturn = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Address = company.Address,
                //Country = company.Country,
            };

            return CreatedAtAction(nameof(GetCompany), new { id = company.Id }, companyToReturn);
        }

        //// DELETE: api/Companies/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCompany(Guid id)
        //{
        //    var company = await _context.Companies.FindAsync(id);
        //    if (company == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Companies.Remove(company);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool CompanyExists(Guid id)
        //{
        //    return _context.Companies.Any(e => e.Id == id);
        //}
    }
}
