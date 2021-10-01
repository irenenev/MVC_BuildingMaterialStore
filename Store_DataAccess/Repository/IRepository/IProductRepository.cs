using Microsoft.AspNetCore.Mvc.Rendering;
using Store_Models;
using System.Collections.Generic;

namespace Store_DataAccess.Repository.IRepository
{
    public interface IProductRepository:IRepository<Product>
    {
        void Update(Product obj);
        IEnumerable<SelectListItem> GetAllDropdownList(string obj);
    }
}
