using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using iReception.DataAccess;
using iReception.Models.Entities;
using iReception.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace iReception.Repository
{
    public class HotelCompanyRepository : IHotelCompanyRepository
    {
        private readonly iReceptionDbContext _db;

        public HotelCompanyRepository(iReceptionDbContext db)
        {
            _db = db;
        }

        public async Task<int> AddAsync()
        {
            var existingHotelCompany = await _db.HotelCompanies.FirstAsync();
            if (existingHotelCompany != null)
            {
                throw new InvalidOperationException("HotelCompany object is already created");
            }
            var hotelCompany = new HotelCompany();

            await _db.HotelCompanies.AddAsync(hotelCompany);
            await _db.SaveChangesAsync();

            return hotelCompany.Id;
        }

        public async Task<HotelCompany> GetAsync()
        {
            var hotelCompany = await _db.HotelCompanies.FirstAsync();
            if (hotelCompany == null)
            {
                throw new KeyNotFoundException("There's no hotel company object to retrieve");
            }

            return hotelCompany;
        }

        public async Task<int> UpdateAsync(HotelCompany hotelCompany)
        {
            var hotelCompanyToUpdate = await _db.HotelCompanies.FirstAsync();
            if (hotelCompanyToUpdate == null)
            {
                throw new KeyNotFoundException("There's no hotel company object to update");
            }

            foreach (PropertyInfo prop in hotelCompany.GetType().GetProperties())
            {
                var value = prop.GetValue(hotelCompany);
                if (value != null && prop.Name.ToLower() != "id")
                {
                    prop.SetValue(hotelCompanyToUpdate, value);
                }
            }

            _db.HotelCompanies.Update(hotelCompanyToUpdate);
            await _db.SaveChangesAsync();

            return hotelCompanyToUpdate.Id;
        }
    }
}
