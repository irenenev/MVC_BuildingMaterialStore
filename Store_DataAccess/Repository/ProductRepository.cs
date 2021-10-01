using Microsoft.AspNetCore.Mvc.Rendering;
using Store_DataAccess.Repository.IRepository;
using Store_Models;
using Store_Utility;
using System.Collections.Generic;
using System.Linq;

namespace Store_DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetAllDropdownList(string obj)
        {
            if (obj == WebConstants.CategoryName)
            {
                return _db.Categories.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            if (obj == WebConstants.ApplicationTypeName)
            {
                return _db.ApplicationTypes.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            }
            return null;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
