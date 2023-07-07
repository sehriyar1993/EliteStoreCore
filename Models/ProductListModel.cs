using Elite.EntityLayer.Concreate;
using System.Collections.Generic;

namespace EliteStoreCore.Models
{
    public class ProductListModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Product> Products { get; set; }
    }
}
