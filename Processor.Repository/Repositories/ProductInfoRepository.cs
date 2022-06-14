using Processor.Database;
using Processor.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Repository.Repositories
{
    public class ProductInfoRepository : BaseRepository<Battery>, IProductInfoRepository
    {
        public ProductInfoRepository(InventoryDBEntities context) : base(context)
        {
        }
    }
}
