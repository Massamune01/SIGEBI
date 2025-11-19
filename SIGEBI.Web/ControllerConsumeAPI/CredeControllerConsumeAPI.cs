using Microsoft.AspNetCore.Mvc;

namespace SIGEBI.Web.ControllerConsumeAPI
{
    public class CredeControllerConsumeAPI : Controller
    {
        // GET: CredeControllerConsumeAPI
        public ActionResult Index()
        {
            return View();
        }

        // GET: CredeControllerConsumeAPI/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CredeControllerConsumeAPI/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CredeControllerConsumeAPI/Create
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

        // GET: CredeControllerConsumeAPI/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CredeControllerConsumeAPI/Edit/5
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

        // GET: CredeControllerConsumeAPI/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CredeControllerConsumeAPI/Delete/5
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
