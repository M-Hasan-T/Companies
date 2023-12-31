﻿using System.ComponentModel.DataAnnotations;

namespace Companies.Shared.Dtos.CompaniesDtos
{
    public class CompanyForCreationDto : CompanyForManipulationDto
    {
        [MaxLength(30, ErrorMessage = "Maximum length for the {0} is {1} characters.")]
        public string Country { get; set; } = string.Empty;
    }
}
