using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWebMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    //1° declarar dependência para o context

    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //prepara o objeto IQueryable
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(y => y.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) //join das tabelas
                .Include(x => x.Seller.Department) //join das tabelas
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Department,SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj; //prepara o objeto IQueryable
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(y => y.Date <= maxDate.Value);
            }

            return await result
                .Include(x => x.Seller) //join das tabelas
                .Include(x => x.Seller.Department) //join das tabelas
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Department) //Retorna lista do tipo IGrouping
                .ToListAsync();
        }
    }
}
