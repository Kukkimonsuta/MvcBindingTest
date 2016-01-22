using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MvcBindingTest.Web.Models;

namespace MvcBindingTest.Web.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index(RootModel model)
		{
			return Json(new
			{
				length = ModelState.Count
			});
		}
	}
}
