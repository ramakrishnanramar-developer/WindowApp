using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DBL
{
    public class ProductsListDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }


    }
    public class GetProductsDTO : ProductsListDTO
    {
        public long SNo { get; set; }

        public string Categories { get; set; }
    }
    public class GetProductDTO : ProductsListDTO
    {
        public long SNo { get; set; }

        public string Description { get; set; }

        public int CategoriesId { get;set; }
    }
    public class ProductsDTO
    {
        public string Name { get; set; }

        public int CategoriesId { get; set; }

        public string Description { get; set; }
    }
    public class UpdateProductsDTO : ProductsDTO
    {
        public int Id { get; set; }
    }
}
