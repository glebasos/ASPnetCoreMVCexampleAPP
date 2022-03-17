using Microsoft.AspNetCore.Mvc;
using LeranMVC.Data;
using LeranMVC.Models;

namespace LeranMVC.Controllers
{
    public class CategoryController : Controller
    {

        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _context.Categories.OrderBy(o => o.DisplayOrder);
            return View(objCategoryList);
        }
        
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Категория успешно добавлена!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.FirstOrDefault(o => o.Id == id);
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _context.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Категория успешно изменена!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _context.Categories.FirstOrDefault(o => o.Id == id);
            return View(category);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var category = _context.Categories.FirstOrDefault(o => o.Id == id);
            if (category is not null)
            {
                _context.Remove(category);
                _context.SaveChanges();
                TempData["success"] = "Категория успешно удалена!";
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
