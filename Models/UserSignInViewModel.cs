using System.ComponentModel.DataAnnotations;

namespace EliteStoreCore.Models
{
	public class UserSignInViewModel
	{
		[Required(ErrorMessage = "İstifadəçi Adını Daxil Edin")]
		public string username { get; set; }

		[Required(ErrorMessage = "Parolunuzu Daxil Edin")]
		public string password { get; set; }
	}
}
