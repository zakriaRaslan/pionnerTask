using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace pionnerTask.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertiesSevice _propertiesSevice;

        public PropertyController(IPropertiesSevice propertiesSevice)
        {
            _propertiesSevice = propertiesSevice;
        }

        public async Task<IActionResult> Index()
        {
            List<CustomProperty> properties = await _propertiesSevice.GetAllAsync();
            return View(properties);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomProperty propertie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _propertiesSevice.AddAsync(propertie);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            CustomProperty property = await _propertiesSevice.GetByIdAsync(id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        public async Task<IActionResult> Edit(int Id)
        {
            CustomProperty property = await _propertiesSevice.GetByIdAsync(Id);
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomProperty property)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _propertiesSevice.UpdateAsync(property);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            CustomProperty property = await _propertiesSevice.GetByIdAsync(Id);
            if (property == null)
            {
                return NotFound();
            }
            return View(property);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(CustomProperty property)
        {
            _propertiesSevice.Delete(property.Id);
            return RedirectToAction(nameof(Index));
        }
      
    }
}
