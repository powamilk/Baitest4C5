using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repository
{
    public interface IGiaoDichTaiChinhRepo
    {
        Task<IEnumerable<GiaoDichTaiChinh>> GetAllAsync();
        Task<GiaoDichTaiChinh> GetByIdAsync(Guid id);
        Task AddAsync(GiaoDichTaiChinh giaoDich);
        Task UpdateAsync(GiaoDichTaiChinh giaoDich);
        Task DeleteAsync(Guid id);
    }
}
