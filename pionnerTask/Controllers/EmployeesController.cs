using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using pionnerTask.Models;
using System.Threading.Tasks;

namespace pionnerTask.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesService _employeesService;
        private readonly IPropertiesSevice _propertiesSevice;
        private readonly ApplicationDbContext _context;
        public EmployeesController(IEmployeesService employeesService, IPropertiesSevice propertiesSevice, ApplicationDbContext context)
        {
            _employeesService = employeesService;
            _propertiesSevice = propertiesSevice;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee>? employees = await _employeesService.GetAllAsync();

            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CustomProperties = await _propertiesSevice.GetAllAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee, Dictionary<int, string>? Properties)
        {
             if (!ModelState.IsValid)
                return BadRequest();

            await _employeesService.AddAsync(employee);
            if (Properties != null)
            {
                foreach (var prop in Properties)
                {
                    _context.EmployeePropertiesValue.Add(new EmployeePropertieValue
                    {
                        EmployeeId = employee.Id,
                        CustomPropertyId = prop.Key,
                        Value = prop.Value
                    });
                }

            }
           
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int Id)
        {
           
            var employee = await _employeesService.GetByIdAsync(Id);
            if (employee == null)
                return NotFound();

           
            var allProperties = await _propertiesSevice.GetAllAsync();

          
            foreach (var prop in allProperties)
            {
                var existing = employee.PropertiesValues
                    .FirstOrDefault(v => v.CustomPropertyId == prop.Id);

                if (existing == null)
                {
                    
                    employee.PropertiesValues.Add(new EmployeePropertieValue
                    {
                        EmployeeId = employee.Id,
                        CustomPropertyId = prop.Id,
                        Value = null,
                        CustomProperty = prop
                    });
                }
            }

           
            ViewBag.AllProperties = allProperties;

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Dictionary<int, string> Properties)
        {
            var employee = await _context.Employees
                .Include(e => e.PropertiesValues)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
                return NotFound();

            
            foreach (var item in Properties)
            {
                int propertyId = item.Key;   
                string value = item.Value;   

                var existing = employee.PropertiesValues
                    .FirstOrDefault(v => v.CustomPropertyId == propertyId);

                if (existing != null)
                {
                    existing.Value = value;
                }
                else
                {
                   
                    employee.PropertiesValues.Add(new EmployeePropertieValue
                    {
                        EmployeeId = employee.Id,
                        CustomPropertyId = propertyId,
                        Value = value
                    });
                }
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int Id)
        {
            Employee? employee = await _context.Employees
                                 .Include(e => e.PropertiesValues)
                                     .ThenInclude(v => v.CustomProperty)
                                         .FirstOrDefaultAsync(e => e.Id == Id);

            if (employee == null)
                return NotFound();

            return View(employee);

        }

        public async Task<IActionResult> Delete(int id)
        {
            Employee? employee = await _employeesService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }


        [HttpPost , ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            Employee? employee = await _employeesService.GetByIdAsync(id);
            if (employee == null)
            {
                return BadRequest();
            }
            await _employeesService.Delete(employee);
            return RedirectToAction(nameof(Index));
        }
    }
}
