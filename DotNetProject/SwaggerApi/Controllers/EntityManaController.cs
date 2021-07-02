using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SwaggerApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[Controller]")]
    [ApiController]
    public class EntityManaController : Controller
    {
        [HttpGet]
        // GET: EntityManaController
        [Route("Index")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Details")]
        // GET: EntityManaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        [HttpGet]
        [Route("Create")]
        // GET: EntityManaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EntityManaController/Create
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
        [HttpGet]
        // GET: EntityManaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EntityManaController/Edit/5
        [HttpPost]
        [Route("Edit")]
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
        [HttpGet]
        [Route("Delete")]
        // GET: EntityManaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EntityManaController/Delete/5
        [HttpPost]
        [Route("Delete")]
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
