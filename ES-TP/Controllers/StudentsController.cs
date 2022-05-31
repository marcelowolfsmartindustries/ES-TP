using ES_TP.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ES_TP.Controllers
{
    public class StudentsController : Controller
    {
        public StudentsController()
        {

        }

        public IActionResult Index()
        {
            List<Student> student = new() { new Student { Name = "Marcelo" }, new Student { Name = "João" } };
            return View(student);
        }
    }
}
