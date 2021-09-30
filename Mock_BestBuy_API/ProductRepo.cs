using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Mock_BestBuy_API
{
    public class ProductRepo : IProductRepo
    {
        private readonly IDbConnection _connection;

        public ProductRepo(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Return all products from the bestbuy.products table.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            return _connection.Query<Product>("SELECT * FROM bestbuy.products;");
        }

        /// <summary>
        /// Return a single product based on the ProductID passed in.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        {
            return _connection.QuerySingleOrDefault<Product>(
                "SELECT * FROM bestbuy.products WHERE ProductID = @id;", new { id });
        }

        /// <summary>
        /// Insert the provided product into the bestbuy.products table.
        /// </summary>
        /// <param name="product"></param>
        public void InsertProduct(Product product)
        {
            _connection.Execute( "INSERT INTO bestbuy.products (Name, Price, CategoryID, OnSale, StockLevel)" +
                                 " VALUES (@Name, @Price, @CategoryID, @OnSale, @StockLevel);",
                    new { product.Name, product.Price, product.CategoryID, product.OnSale, product.StockLevel });
        }

        /// <summary>
        /// Update the 
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE bestbuy.products SET Name = @Name, Price = @Price, OnSale = @OnSale, StockLevel = @StockLevel WHERE ProductID = @ProductID;",
                new { product.Name, product.Price, product.OnSale, product.StockLevel, product.ProductID });
        }

        /// <summary>
        /// Deletes the product provided from the bestbuy.products, bestbuy.reviews, and bestbuy.sales tables.
        /// </summary>
        /// <param name="product"></param>
        public void DeleteProduct(Product product)
        {
            _connection.Execute("DELETE FROM bestbuy.products WHERE ProductID = @ProductID", new { product.ProductID });
            _connection.Execute("DELETE FROM bestbuy.reviews WHERE ProductID = @ProductID", new { product.ProductID });
            _connection.Execute("DELETE FROM bestbuy.sales WHERE ProductID = @ProductID", new { product.ProductID });
        }


    }
}
