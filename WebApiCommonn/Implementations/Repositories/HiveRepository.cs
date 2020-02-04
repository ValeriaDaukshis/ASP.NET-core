using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WebApiCommon.DataModel;

namespace WebApiCommon.Implementations.Repositories
{
    public class HiveRepository : IHiveRepository
    {
        private ProductDbContext db;
        private DbSet<Hive> _dbSet;
        public HiveRepository(ProductDbContext context)
        {
            _dbSet = context.Hives;
            db = context;
        }
        public IEnumerable<Hive> GetHives()
        {
            return _dbSet;
        }

        public Hive GetHive(int id)
        {
            return _dbSet.Find(id);
        }

        public void AddHive(Hive hive)
        {
            _dbSet.Add(hive);
            db.SaveChanges();
        }

        public void UpdateHiveAddress(int id, Hive hive)
        {
            _dbSet.Update(hive);
            db.SaveChanges();
        }

        public void DeleteHive(int id)
        {
            var hiveToDelete = GetHive(id);
            _dbSet.Remove(hiveToDelete);
            db.SaveChanges();
        }
    }
}
