using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mock_BestBuy_API
{
    public interface IProductRepo
    {
        public IEnumerable<Product> GetProducts();
        public Product GetProduct(int productID);
        public void InsertProduct(Product product);
        public void UpdateProduct(Product product);
        public void DeleteProduct(Product product);
    }
}
