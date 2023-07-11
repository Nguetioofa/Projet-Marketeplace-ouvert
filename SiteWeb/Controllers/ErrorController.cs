using Microsoft.AspNetCore.Mvc;

namespace SiteWeb.Controllers
{
	public class ErrorController : Controller
	{
		[Route("/Error/{statusCode}")]
		public IActionResult Error(int statusCode)
		{
			if (statusCode == 404)
			{
				return View("Error404");
			}

			return View();
		}
	}

}
