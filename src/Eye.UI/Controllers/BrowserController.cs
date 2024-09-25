using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eye.UI.Controllers
{
    public class BrowserController : Controller
    {
        // GET: BrowserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BrowserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BrowserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrowserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrowserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrowserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BrowserController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrowserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
