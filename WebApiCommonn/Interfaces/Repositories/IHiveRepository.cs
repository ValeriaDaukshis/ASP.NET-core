using System;
using System.Collections.Generic;
using System.Text;
using WebApiCommon.DataModel;
using WebApiCommonn.DataModel;

namespace WebApiCommon.Implementations.Repositories
{
    public interface IHiveRepository
    {
        IEnumerable<Hive> GetHives();
        Hive GetHive(int id);
        void AddHive(Hive hive);
        void UpdateHiveAddress(int id, Hive hive);
        void DeleteHive(int id);
    }
}
