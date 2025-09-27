using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDefaultServices<TDto>
    {
       Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int Id);
        Task<bool> CreateAsync(TDto dto);
        Task<bool> UpdateAsync(TDto dto, int Id);
        Task<bool> DeleteAsync(int Id);
    }
}
