using La_mia_pizzeria_1_n.Database;
using Microsoft.AspNetCore.Mvc;
using La_mia_pizzeria_1_n.Models;
using Microsoft.Extensions.Hosting;

namespace La_mia_pizzeria_1_n.Controllers {
    public class PizzaController : Controller {

        public IActionResult Index() {

            using (PizzaContext db = new PizzaContext()) {

                List<Pizza> ListaPizze = db.Pizze.ToList<Pizza>();

                return View("Index", ListaPizze);
            }

        }

        public IActionResult Details(int id) {

            using (PizzaContext db = new PizzaContext()) {

                Pizza PizzaTrovata = db.Pizze
                    .Where(SingolaPizzaNelDb => SingolaPizzaNelDb.Id == id)
                    .FirstOrDefault();

                if (PizzaTrovata != null) {

                    return View(PizzaTrovata);
                }

                return NotFound("La pizza con l'id cercato non esiste");
            }
        }

        [HttpGet]
        public IActionResult Create() {

            using (PizzaContext db = new PizzaContext()) {
                List<Category> categoriesFromDb = db.Categories.ToList<Category>();

                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = new Pizza();

                modelForView.Categories = categoriesFromDb;

                return View("Create", modelForView);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaCategoriesView formData) {
            if (!ModelState.IsValid) {
                using (PizzaContext db = new PizzaContext()) {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }


                return View("Create", formData);
            }

            using (PizzaContext db = new PizzaContext()) {
                db.Pizze.Add(formData.Pizza);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id) {
            using (PizzaContext db = new PizzaContext()) {
                Pizza postToUpdate = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (postToUpdate == null) {
                    return NotFound("La pizza non è stata trovata");
                }

                List<Category> categories = db.Categories.ToList<Category>();

                PizzaCategoriesView modelForView = new PizzaCategoriesView();
                modelForView.Pizza = postToUpdate;
                modelForView.Categories = categories;

                return View("Update", modelForView);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaCategoriesView formData) {
            if (!ModelState.IsValid) {

                using (PizzaContext db = new PizzaContext()) {
                    List<Category> categories = db.Categories.ToList<Category>();

                    formData.Categories = categories;
                }

                return View("Update", formData);
            }

            using (PizzaContext db = new PizzaContext()) {
                Pizza pizzaToUpdate = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToUpdate != null) {

                    pizzaToUpdate.Name = formData.Pizza.Name;
                    pizzaToUpdate.Description = formData.Pizza.Description;
                    pizzaToUpdate.Img = formData.Pizza.Img;
                    pizzaToUpdate.CategoryId = formData.Pizza.CategoryId;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                } else {
                    return NotFound("La pizza che volevi modificare non è stata trovata!");
                }
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id) {
            using (PizzaContext db = new PizzaContext()) {
                Pizza pizzaToDelete = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaToDelete != null) {
                    db.Pizze.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                } else {
                    return NotFound("La pizza da eliminare non è stata trovata!");
                }
            }
        }
    }
}
