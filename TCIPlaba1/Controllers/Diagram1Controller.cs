using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TCIPlaba1.Models;

namespace TCIPlaba1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Diagram1Controller : ControllerBase
	{
		private readonly Ictplaba1Context _context;
		public Diagram1Controller(Ictplaba1Context context)
		{
			_context = context;
		}

		[HttpGet("JsonData")]
		public JsonResult JsonData()
		{
			//System.FormattableString sequenceMaxQuery = $"SELECT name, ID,(SELECT COUNT(stadium)FROM Matches WHERE stadium = Stadium.ID)C FROM Stadium";

			//var categories = _context.Matches.Include(c => c.Goals).ToList();
			var ictplaba1Context =  _context.Matches.Include(p => p.StadiumNavigation).ToList();
			var categories = _context.Stadia.ToList();
			List<object> catBook = new List<object>();
			catBook.Add(new[] { "Матч", "Кількість матчів" });

			foreach (var c in categories)
			{
				//catBook.Add(new object[] { c.Name, c.Id});
				catBook.Add(new object[] { c.Name, c.Matches.Count});
			}

			return new JsonResult(catBook);
		}
	}
}

