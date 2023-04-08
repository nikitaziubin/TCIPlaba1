using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCIPlaba1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace TCIPlaba1.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class Diagram3Controller : ControllerBase
	{
		private readonly Ictplaba1Context _context;
		Dictionary<string, int> teamsWins = new Dictionary<string, int>();

		public Diagram3Controller(Ictplaba1Context context)
		{
			_context = context;
		}

		[HttpGet("JsonData")]
		public JsonResult JsonData()
		{

			var matches = _context.Matches.ToList();

			var participants = _context.Participants.Include(p => p.MatchNavigation)
				.Include(p => p.TeamNavigation).ToList();
			var teams = _context.Teams.ToList();

			foreach (var team in teams)
			{
				teamsWins.Add(team.Name, 0);
			}
			//var categories = _context.Matches.ToList();
			List<object> catBook = new List<object>();
			catBook.Add(new[] { "Имя", "Кількість перемог" });
			foreach (var match in matches)
			{
				if (match.Participants.ElementAt(0).Goals > match.Participants.ElementAt(1).Goals)
				{
					int wins;
					if (teamsWins.TryGetValue(match.Participants.ElementAt(0).TeamNavigation.Name, out wins))
					{
						wins++;
						teamsWins[match.Participants.ElementAt(0).TeamNavigation.Name] = wins;

					}
				}
				else
				{
					int wins;
					if (teamsWins.TryGetValue(match.Participants.ElementAt(1).TeamNavigation.Name, out wins))
					{
						wins++;
						teamsWins[match.Participants.ElementAt(1).TeamNavigation.Name] = wins; // записываем измененное значение в словарь

					}
				}

			}
			foreach (var team in teams)
			{
				int wins;
				if (teamsWins.TryGetValue(team.Name, out wins))
				{
					catBook.Add(new object[] {team.Name, wins});
				}
			}
			//catBook.Add(new object[] { team.Name, team.Participants.});


			return new JsonResult(catBook);
		}
	}
}
