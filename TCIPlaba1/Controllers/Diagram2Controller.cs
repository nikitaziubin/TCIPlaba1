using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCIPlaba1.Models;
using Microsoft.EntityFrameworkCore;

namespace TCIPlaba1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Diagram2Controller : ControllerBase
	{
		private readonly Ictplaba1Context _context;
		private List<int> teamGoals = new List<int>();
		public Diagram2Controller(Ictplaba1Context context)
		{
			_context = context;
		}

		[HttpGet("JsonData")]
		public JsonResult JsonData()
		{
			//System.FormattableString sequenceMaxQuery = $"SELECT name, ID,(SELECT COUNT(stadium)FROM Matches WHERE stadium = Stadium.ID)C FROM Stadium";

			var Matches = _context.Matches.ToList();

			var categories = _context.Participants.Include(p => p.MatchNavigation)
				.Include(p => p.TeamNavigation).ToList();
			var categories1 = _context.Teams.ToList();
			//var categories = _context.Matches.ToList();
			List<object> catBook = new List<object>();
			catBook.Add(new[] { "Имя", "Кількість учасників" });
			foreach (var c in categories1)
			{
				//catBook.Add(new object[] { c.Name, c.Id});
				catBook.Add(new object[] { c.Name, c.Participants.Count });
			}

			return new JsonResult(catBook);
		}
	}
}
