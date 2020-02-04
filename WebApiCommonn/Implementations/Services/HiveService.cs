using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApiCommon.DataModel;
using WebApiCommon.Implementations.Repositories;
using WebApiCommon.Interfaces;
using WebApiCommon.Interfaces.Services;

namespace WebApiCommon.Implementations.Services
{
    public class HiveService : IHiveService
    {
        private const string AllHives = "hives";
        private const string SingleHive = "hive_";

        private readonly ILogger<HiveService> _logger;
        private readonly ICaching<Hive> _cache;
        private readonly IHiveRepository _hiveRepository;
        public HiveService(IHiveRepository hiveRepository, ILogger<HiveService> logger, ICaching<Hive> cache)
        {
            _hiveRepository = hiveRepository;
            _logger = logger;
            _cache = cache;
        }
        public IEnumerable<Hive> GetHives()
        {
            if (!_cache.CheckInCache(AllHives))
            {
                Func<IEnumerable<Hive>> scr = () => _hiveRepository.GetHives(); ;
                _cache.SetInCache(AllHives, scr);
                _logger.LogInformation("Set hives in cache");
            }

            return _cache.ReturnValueByKey(AllHives);
        }

        public Hive GetHive(int id)
        {
            string key = SingleHive + id;
            if (!_cache.CheckInCache(key))
            {
                Func<int, Hive> scr = d => _hiveRepository.GetHive(d);
                _cache.SetInCache(key, scr, id);
                _logger.LogInformation($"Set hive {id} in cache");
            }
            return _cache.ReturnSingleValueByKey(key);
        }

        public void AddHive(Hive hive)
        {
            _hiveRepository.AddHive(hive);
            _cache.RemoveValueFromCache(AllHives);
            _logger.LogInformation("Remove all hives from cache");
        }

        public void UpdateHiveAddress(int id, Hive hive)
        {
            _hiveRepository.UpdateHiveAddress(id, hive);
            _cache.RemoveValueFromCache(AllHives);
            _cache.RemoveValueFromCache(SingleHive + id);
            _logger.LogInformation($"Remove all hives and hive {id} from cache");
        }

        public void DeleteHive(int id)
        {
            _hiveRepository.DeleteHive(id);
            _cache.RemoveValueFromCache(AllHives);
            _cache.RemoveValueFromCache(SingleHive + id);
            _logger.LogInformation($"Remove all hives and hive {id} from cache");
        }
    }
}
