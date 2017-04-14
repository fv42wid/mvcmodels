using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MvcModels.Models;

namespace MvcModels.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }

        public IActionResult Index([FromQuery]int? id)
        {
            Person person;
            if(id.HasValue && (person = repository[id.Value]) != null)
            {
                return View(person);
            } else
            {
                return NotFound();
            }
        }

        public ViewResult Create() => View(new Person());

        [HttpPost]
        public ViewResult Create(Person model) => View("Index", model);

        public ViewResult DisplaySummary(
            [Bind(Prefix = nameof(Person.HomeAddress))]AddressSummary summary) => View(summary);

        public ViewResult Names(IList<string> names) => View(names ?? new string[0]);

        public ViewResult Address(IList<AddressSummary> addresses) =>
            View(addresses ?? new List<AddressSummary>());

        public ViewResult Header(HeaderModel model) => View(model);
    }
}
