using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
	

	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
		public CategoryController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			IEnumerable<Category> objCategories = _db.Categories;
			return View(objCategories);
		}

		//GET ACTION METHOD
        public IActionResult Create()
        {
            
            return View();
        }

		//POST ACTION METHOD
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Category obj)
		{
			if (obj.Name == obj.DisplayOrder.ToString())
			{
				//ModelState.AddModelError("Custom Error", "Display Order should not match the Name");

				ModelState.AddModelError("name", "Display Order should not match the Name");
			}

			if (ModelState.IsValid)
			{
				_db.Categories.Add(obj);
				_db.SaveChanges();
				TempData["success"] = "Category created successfully";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

			//GET ACTION METHOD
			public IActionResult Edit(int? id)
			{
				if(id == null || id == 0)
				{
					return NotFound();
				}
			var categoryFromDb = _db.Categories.Find(id);
			//var categoriesFromDbfirstMethod = _db.Categories.FirstOrDefault(u => u.Id == id);
			//var categoriesFromDbSingleMethod = _db.Categories.SingleOrDefault(u => u.Id == id);

			if(categoryFromDb == null)
				{
					return NotFound();
				}

			return View(categoryFromDb);
			}

			//POST ACTION METHOD
			[HttpPost]
			[ValidateAntiForgeryToken]
			public IActionResult Edit(Category obj)
			{
				if (obj.Name == obj.DisplayOrder.ToString())
				{
					//ModelState.AddModelError("Custom Error", "Display Order should not match the Name");

					ModelState.AddModelError("name", "Display Order should not match the Name");
				}

				if (ModelState.IsValid)
				{
					_db.Categories.Update(obj);
					_db.SaveChanges();
					TempData["success"] = "Category updated successfully";
					return RedirectToAction("Index");
				}
				return View(obj);
			}

        //GET ACTION METHOD
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);
            //var categoriesFromDbfirstMethod = _db.Categories.FirstOrDefault(u => u.Id == id);
            //var categoriesFromDbSingleMethod = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //POST ACTION METHOD
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.Categories.Find(id);

            
			if(obj == null)
			{
				return NotFound();
			}
         
            
                _db.Categories.Remove(obj);
                _db.SaveChanges();
				TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            
            
        }

    }
}
