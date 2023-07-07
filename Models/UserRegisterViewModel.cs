using System.ComponentModel.DataAnnotations;

namespace EliteStoreCore.Models
{
	public class UserRegisterViewModel
	{
		[Required(ErrorMessage = "Zəhmət olmasa adınızı qeyd edin")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Zəhmət olmasa soy adınızı qeyd edin")]
		public string Surname { get; set; }

		[Required(ErrorMessage = "Zəhmət olmasa istifadəçi adınızı qeyd edin")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Zəhmət olmasa mail ünvanınızı qeyd edin")]
		public string Mail { get; set; }
		[Required(ErrorMessage = "Zəhmət olmasa nömrənizi qeyd edin")]
		public string PhoneNumber { get; set; }

		[Required(ErrorMessage = "Zəhmət olmasa şifrənizi qeyd edin")]
		public string Password { get; set; }

		[Required(ErrorMessage = "Zəhmət olmasa şifrənizi təkrarlayın")]

		[Compare("Password", ErrorMessage = "Şifrələr uyğun deyil")]
		public string ConfirmPassword { get; set; }
		public string City { get; set; }
		public string Adress { get; set; }
		public string Gender { get; set; }
		public string About { get; set; }
		public string ImageUrl { get; set; }
	}
}
