using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PioneerTaskApi.Data;
using PioneerTaskApi.DTO;
using PioneerTaskApi.Models;
using PioneerTaskApi.Repositories.Interfaces;

namespace PioneerTaskApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ICustomPropertiesRepository _customPropertiesRepository;
        private readonly ApplicationDbContext _context;
        public EmployesController(IEmployeesRepository employeesRepository,
            ICustomPropertiesRepository customPropertiesRepository,
            ApplicationDbContext context)
        {
            _employeesRepository = employeesRepository;
            _customPropertiesRepository = customPropertiesRepository;
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Employee> employees = await _employeesRepository.GetAllEmployeesAsync();
            if (employees.Count() == 0 || employees == null)
            {
                return NotFound("There is no employees ");
            }

            var result = employees.Select(emp => new EmployeeResponseDTO
            {
                Id = emp.Id,
                Name = emp.Name,
                Code = emp.Code,
                CustomProperties = emp.EmployeeCustomPropValues
                        .ToDictionary(x => x.CustomPropertyId, x => x.Value ?? string.Empty)
            }).ToList();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            Employee? employee = await _employeesRepository.GetEmployeeByIdWithPropertiesAsync(Id);
            if (employee == null)
            {
                return NotFound("There is no employee with this Id");
            }
            EmployeeResponseDTO employeDto = new EmployeeResponseDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Code = employee.Code,
                CustomProperties = employee.EmployeeCustomPropValues
                             .ToDictionary(x => x.CustomPropertyId, x => x.Value ?? string.Empty)
            };
            return Ok(employeDto);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(CreateEmployeeDTO employeDTO)
        {
            List<CustomProperty>? existingCustomProperties = await _customPropertiesRepository.GetAllCustomPropertiesAsync();
            Employee newEmployee = new Employee
            {
                Code = employeDTO.Code,
                Name = employeDTO.Name,
                EmployeeCustomPropValues = new List<EmployeeCustomPropValue>()
            };

            foreach (var customProp in existingCustomProperties)
            {
                if (employeDTO.CustomProperties != null && employeDTO.CustomProperties.ContainsKey(customProp.Id))
                {
                    string? value = employeDTO.CustomProperties[customProp.Id];
                    EmployeeCustomPropValue employeeCustomPropValue = new EmployeeCustomPropValue
                    {
                        CustomPropertyId = customProp.Id,
                        Value = value
                    };
                    newEmployee.EmployeeCustomPropValues.Add(employeeCustomPropValue);
                }
                else
                {
                    if (customProp.IsRequired)
                    {
                        return BadRequest($"The custom property '{customProp.Name}' is required.");
                    }
                }
            }

            //await _context.EmployeeCustomPropValues.AddRangeAsync(newEmployee.EmployeeCustomPropValues);
            await _employeesRepository.AddEmployeeAsync(newEmployee);
            await _context.SaveChangesAsync();
            return Ok("Created Successfully");

        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Update(int Id, CreateEmployeeDTO employeDTO)
        {
            Employee? existingEmployee = await _employeesRepository.GetEmployeeByIdWithPropertiesAsync(Id);
            if (existingEmployee == null)
            {
                return NotFound("There is no employee with this Id");
            }
            existingEmployee.Name = employeDTO.Name;
            existingEmployee.Code = employeDTO.Code;
            List<CustomProperty>? existingCustomProperties = await _customPropertiesRepository.GetAllCustomPropertiesAsync();
            foreach (var existCustomProp in existingCustomProperties)
            {
                var existingPropValue = existingEmployee.EmployeeCustomPropValues?
                    .FirstOrDefault(ev => ev.CustomPropertyId == existCustomProp.Id);
                if (employeDTO.CustomProperties != null && employeDTO.CustomProperties.ContainsKey(existCustomProp.Id))
                {
                    string? value = employeDTO.CustomProperties[existCustomProp.Id];
                    if (existingPropValue != null)
                    {
                        existingPropValue.Value = value;
                    }
                    else
                    {
                        EmployeeCustomPropValue newPropValue = new EmployeeCustomPropValue
                        {
                            CustomPropertyId = existCustomProp.Id,
                            EmployeeId = existingEmployee.Id,
                            Value = value
                        };
                        existingEmployee.EmployeeCustomPropValues.Add(newPropValue);
                    }
                }
                else
                {
                    if (existCustomProp.IsRequired)
                    {
                        return BadRequest($"The custom property '{existCustomProp.Name}' is required.");
                    }
                }
            }
            await _employeesRepository.UpdateEmployeeAsync(existingEmployee);
            await _context.SaveChangesAsync();
            return Ok("Updated Successfully");

        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            Employee? existingEmployee = await _employeesRepository.GetEmployeeByIdWithPropertiesAsync(Id);
            if (existingEmployee == null)
            {
                return NotFound("There is no employee with this Id");
            }
            await _employeesRepository.DeleteEmployeeAsync(existingEmployee);
            return Ok("Deleted Successfully");

        }
    }
}
