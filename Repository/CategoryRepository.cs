using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int categoryId)
        {
            return _context.Categories.Any(c => c.Id == categoryId);
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.ToList();
            return categories;
        }

        public Category GetCategory(int Id)
        {
            return _context.Categories.Where(c => c.Id == Id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonsByCategory(int categoryId)
        {
            var pokemons = _context.PokemonCategories
                .Where(pc => pc.CategoryId == categoryId)
                .Select(pc => pc.Pokemon)
                .ToList();
            return pokemons;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}