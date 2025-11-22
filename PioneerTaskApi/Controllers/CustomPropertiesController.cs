using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PioneerTaskApi.DTO;
using PioneerTaskApi.Models;
using PioneerTaskApi.Repositories.Interfaces;

namespace PioneerTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomPropertiesController : ControllerBase
    {
        private readonly ICustomPropertiesRepository _customPropertyRepository;
        public CustomPropertiesController(ICustomPropertiesRepository customPropertyRepository)
        {
            _customPropertyRepository = customPropertyRepository;
        }
        List<string> allowedTypes = new List<string> { "text", "number", "date", "checkbox", "dropdown" };

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _customPropertyRepository.GetAllCustomPropertiesAsync();
            if (result.Count() == 0 || result == null)
            {
                return NotFound("No Custom Properties Found");
            }
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var result = await _customPropertyRepository.GetCustomPropertyByIdWithPropValuesAsync(Id);
            if (result == null)
            {
                return NotFound($"Custom Property with Id {Id} Not Found");
            }
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCustomPropertyDTO createCustomPropertyDTO)
        {
            if (createCustomPropertyDTO.DataType.ToLower() == "dropdown" && createCustomPropertyDTO.Options == null)
            {
                return BadRequest("Options are required for Dropdown data type");
            }
            if (!allowedTypes.Contains(createCustomPropertyDTO.DataType.ToLower()))
            {
                return BadRequest("Invalid Data Type. Allowed types are: text, number, date, checkbox, dropdown");
            }

            CustomProperty customProperty = new CustomProperty
            {
                Name = createCustomPropertyDTO.Name,
                DataType = createCustomPropertyDTO.DataType.ToLower(),
                IsRequired = createCustomPropertyDTO.IsRequierd,
                Options = createCustomPropertyDTO.Options
            };

            await _customPropertyRepository.CreateCustomPropertyAsync(customProperty);
            return Ok($"Created Successfully With Id = {customProperty.Id}");

        }


        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, [FromBody] CreateCustomPropertyDTO updateCustomPropertyDTO)
        {
            var existingCustomProperty = await _customPropertyRepository.GetCustomPropertyByIdAsync(Id);
            if (existingCustomProperty == null)
            {
                return NotFound($"Custom Property with Id {Id} Not Found");
            }
            if (updateCustomPropertyDTO.DataType.ToLower() == "dropdown" && updateCustomPropertyDTO.Options == null)
            {
                return BadRequest("Options are required for Dropdown data type");
            }
            if (!allowedTypes.Contains(updateCustomPropertyDTO.DataType.ToLower()))
            {
                return BadRequest("Invalid Data Type. Allowed types are: text, number, date, checkbox, dropdown");
            }
            existingCustomProperty.Name = updateCustomPropertyDTO.Name;
            existingCustomProperty.DataType = updateCustomPropertyDTO.DataType.ToLower();
            existingCustomProperty.IsRequired = updateCustomPropertyDTO.IsRequierd;
            existingCustomProperty.Options = updateCustomPropertyDTO.Options;
            await _customPropertyRepository.UpdateCustomPropertyAsync(existingCustomProperty);
            return Ok("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCustomProperty = await _customPropertyRepository.GetCustomPropertyByIdAsync(id);
            if (existingCustomProperty == null)
            {
                return NotFound($"Custom Property with Id {id} Not Found");
            }
            await _customPropertyRepository.DeleteCustomPropertyAsync(existingCustomProperty);
            return Ok("Deleted Successfully");
        }
    }
}
