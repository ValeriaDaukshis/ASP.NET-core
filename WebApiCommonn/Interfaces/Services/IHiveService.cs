using System;
using System.Collections.Generic;
using System.Text;
using WebApiCommon.DataModel;

namespace WebApiCommon.Interfaces.Services
{
    public interface IHiveService
    {
        IEnumerable<Hive> GetHives();
        Hive GetHive(int id);
        void AddHive(Hive hive);
        void UpdateHiveAddress(int id, Hive hive);
        void DeleteHive(int id);
    }
}
