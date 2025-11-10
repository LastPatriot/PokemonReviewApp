using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;

        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CountryExists(int countryId)
        {
            return _context.Countries.Any(c => c.Id == countryId);
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            var countries = _context.Countries.ToList();
            return countries;
        }

        public Country GetCountry(int countryId)
        {
            var country = _context.Countries.Where(c => c.Id == countryId).FirstOrDefault();
            return country;
        }

        public Country GetCountryByOwner(int ownerId)
        {
            var country = _context.Owners
                .Where(o => o.id == ownerId)
                .Select(o => o.Country)
                .FirstOrDefault();
            return country;
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            var owners = _context.Owners
                .Where(o => o.Country.Id == countryId)
                .ToList();
            return owners;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}