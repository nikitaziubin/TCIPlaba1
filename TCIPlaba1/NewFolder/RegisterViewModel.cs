using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace TCIPlaba1.NewFolder
{
	public class RegisterViewModel
	{

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }


		[Display(Name = "Рік народження")]
		public int Year { get; set; }

		[Display(Name = "Пароль")]
		public string Password { get; set; }

		[Compare("Password", ErrorMessage ="Паролі не співпадають")]
		[Display(Name = "Підтвердження пароля")]
		[DataType(DataType.Password)]
		public string PasswordConfirm { get; set; }

	}
}
