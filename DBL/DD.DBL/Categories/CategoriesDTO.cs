using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DD.DBL
{
    public class CategoriesGetDTO
    {
        public string Name { get; set; }

        public int Id { get; set; }
    }
    public class CategoriesDTO
    {
        public string Name { get; set; }

        public int CreatedBy { get; set; }
    }
    public class UpdateCategoriesDTO: CategoriesDTO
    {
        public int Id { get; set; }
    }
    public class CategoriesGridDTO : CategoriesGetDTO
    {
        public long SNo { get; set; }
    }
}
