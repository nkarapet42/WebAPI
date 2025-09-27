using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Domain.Entities
{
    public class Product
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        private readonly List<Category> _categories = new List<Category>();
        public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
        private Product() { }

        public Product(string name, IEnumerable<Category> category)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Fill the name.", nameof(name));
            }
            else if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Categories cannot be null.");
            }
            var categoriesList = category.ToList();
            if (categoriesList.Count != 2 && categoriesList.Count != 3)
            {
                throw new ArgumentException("Product must have 2 or 3 categories.", nameof(category));
            }
            else if (categoriesList.Distinct().Count() != categoriesList.Count)
            {
                throw new ArgumentException("Categories must be unique.", nameof(category));
            }
            Name = name;
            _categories.AddRange(categoriesList);
        }
        public void AddCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (_categories.Contains(category))
            {
                throw new ArgumentException("Category already exists in product.", nameof(category));
            }
            if (_categories.Count >= 3)
            {
                throw new InvalidOperationException("Product cannot have more than 3 categories.");
            }
            _categories.Add(category);
        }

        public void RemoveCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            if (!_categories.Contains(category))
            {
                throw new ArgumentException("Category not found in product.", nameof(category));
            }
            if (_categories.Count <= 2)
            {
                throw new InvalidOperationException("Product must have at least 2 categories.");
            }
            _categories.Remove(category);
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Fill the name, to update.", nameof(name));
            }
            Name = name;
        }

        public void UpdateCategories(IEnumerable<Category> categories)
        {
            if (categories == null)
            {
                throw new ArgumentNullException(nameof(categories), "Categories cannot be null.");
            }
            var categoriesList = categories.ToList();
            if (categoriesList.Count != 2 && categoriesList.Count != 3)
            {
                throw new ArgumentException("Product must have 2 or 3 categories.", nameof(categories));
            }
            else if (categoriesList.Distinct().Count() != categoriesList.Count)
            {
                throw new ArgumentException("Categories must be unique.", nameof(categories));
            }
            _categories.Clear();
            _categories.AddRange(categoriesList);
        }
    }
}
