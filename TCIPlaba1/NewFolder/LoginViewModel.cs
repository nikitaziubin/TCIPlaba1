using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;


namespace TCIPlaba1.NewFolder
{
	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Display(Name = "Запам'ятати?")]
		public bool RememberMe { get; set; }

		public string ReturnUrl { get; set; }


	}
}
