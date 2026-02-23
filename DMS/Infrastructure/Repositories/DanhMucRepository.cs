using DMS.Domain.Entities;
using DMS.Domain.Interfaces;
using DMS.Infrastructure.Data;
using DMS.Infrastructure.Repositories;

namespace DMS.Infrastructure.Repositories
{
    public class DanhMucRepository : GenericRepository<DanhMuc>, IDanhMucRepository
    {
        public DanhMucRepository(DMSContext context) : base(context)
        {
        }
    }
}
