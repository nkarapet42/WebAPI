using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.DTOs
{
    public class ProductCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public List<int> CategoryIds { get; set; } = new();
    }

    public class ProductReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<CategoryReadDto> Categories { get; set; } = new();
    }
}
