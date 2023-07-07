using Elite.EntityLayer.Concreate;
using System.Collections.Generic;

namespace EliteStoreCore.Models
{
	public class CategoryListViewModel
	{
		public string SelectedCategory { get; set; }

		public List<Category> Categories { get; set; }
	}
}
