using Microsoft.AspNetCore.Http;

namespace EliteStoreCore.Models
{
	public class UserEditViewModel
	{
		public string name { get; set; }
		public string surname { get; set; }
		public string username { get; set; }
		public string password { get; set; }
		public string confirmpassword { get; set; }
		public string phonenumber { get; set; }
		public string mail { get; set; }
		public string imageurl { get; set; }
		public string city { get; set; }
		public string adress { get; set; }
		public string gender { get; set; }
		public string about { get; set; }
		public IFormFile Image { get; set; }
	}
}
