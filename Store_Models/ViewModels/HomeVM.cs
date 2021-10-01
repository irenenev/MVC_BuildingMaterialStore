using System.Collections.Generic;

namespace Store_Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Product> Products{ get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
