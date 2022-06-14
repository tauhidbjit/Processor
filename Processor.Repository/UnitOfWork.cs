using Processor.Database;
using Processor.Repository.Interfaces;
using Processor.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processor.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly InventoryDBEntities _inventoryDBContext;
        public UnitOfWork()
        {
            //InitializeDbContext(connectionString);
            _inventoryDBContext = new InventoryDBEntities();
            Products = new ProductInfoRepository(_inventoryDBContext);
        }

        //private void InitializeDbContext(string connectionString)
        //{
        //    inventoryDBContext = new InventoryDBContext();
        //}

        public IProductInfoRepository Products { get; private set; }

        public int Commit()
        {
            return _inventoryDBContext.SaveChanges();
        }

        public void Dispose()
        {
            _inventoryDBContext.Dispose();
        }
    }
}
