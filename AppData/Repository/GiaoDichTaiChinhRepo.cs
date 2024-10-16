using AppData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public class GiaoDichTaiChinhRepo : IGiaoDichTaiChinhRepo
    {
        private readonly AppDbContext _context; 

        public GiaoDichTaiChinhRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(GiaoDichTaiChinh giaoDich)
        {
            await _context.GiaoDichTaiChinhs.AddAsync(giaoDich);
            await _context.SaveChangesAsync(); 
        }

        public async Task DeleteAsync(Guid id)
        {
            var giaoDich = await _context.GiaoDichTaiChinhs.FindAsync(id);
            if(giaoDich != null && giaoDich.TrangThai != "Đang xử lý")
            {
                _context.GiaoDichTaiChinhs.Remove(giaoDich);
                _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GiaoDichTaiChinh>> GetAllAsync()
        {
            return await _context.GiaoDichTaiChinhs.ToListAsync(); 
        }

        public async Task<GiaoDichTaiChinh> GetByIdAsync(Guid id)
        {
            return await _context.GiaoDichTaiChinhs.FindAsync(id);
        }

        public Task UpdateAsync(GiaoDichTaiChinh giaoDich)
        {
            _context.GiaoDichTaiChinhs.Update(giaoDich);
            return _context.SaveChangesAsync();
        }
    }
}
