
namespace Store_Models.ViewModels
{
    public class DetailsVM
    {
        public DetailsVM()
        {
            Product = new Product();
        }
        public Product Product { get; set; }
        //товар добавлен в корзину?
        public bool ExistsInCart { get; set; }
    }
}
