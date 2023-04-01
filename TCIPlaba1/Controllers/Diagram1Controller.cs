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
			//var sequenceQueryResult = _context.Database.SqlQuery<string>(sequenceMaxQuery);


			//var categories = _context.Matches.Include(c => c.Goals).ToList();
			var categories = _context.Stadia.ToList();
			List<object> catBook = new List<object>();
			catBook.Add(new[] { "Категорія", "Кількість книжок" });

			foreach (var c in categories)
			{
				catBook.Add(new object[] { c.Name, c.Id});
			}

			return new JsonResult(catBook);
		}
	}
}

