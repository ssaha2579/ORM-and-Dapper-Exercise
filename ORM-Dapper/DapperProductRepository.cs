using Dapper;
using System.Collections.Generic;
using System.Data;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;

        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void Delete(int id)
        {
            _conn.Execute("DELETE FROM sales WHERE ProductID = @id;", new {id = id});
            _conn.Execute("DELETE FROM reviews WHERE ProductID = @id;", new { id = id });
            _conn.Execute("DELETE FROM products WHERE ProductID = @id;", new { id = id });
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("SELECT * FROM products;");
        }

        public Product GetProduct(int id)
        {
            return _conn.QuerySingleOrDefault<Product>(
            "SELECT * FROM products WHERE ProductID = @id;",
            new { id });
        }

        public void Update(Product product)
        {
            _conn.Execute(
                "UPDATE products SET " +
                "Name = @Name, " +
                "Price = @Price, " +
                "CategoryID = @CategoryID, " +
                "OnSale = @OnSale, " +
                "StockLevel = @StockLevel " +
                "WHERE ProductID = @ProductID;",
                new
                {
                    product.Name,
                    product.ProductID,
                    product.Price,
                    product.CategoryID,
                    product.OnSale,
                    product.StockLevel
                });
        }
    }
}