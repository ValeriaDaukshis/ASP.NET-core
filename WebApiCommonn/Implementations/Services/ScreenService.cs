using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WebApiCommon.Interfaces;
using WebApiCommonn.DataModel;

namespace WebApiCommon.Implementations.Services
{
    public class ScreenService : IScreenService
    {
        private const string AllScreens = "screens";
        private const string SingleScreen = "screen_";

        private readonly IScreenRepository _screenRepository;
        private readonly ILogger<ScreenService> _logger;
        private readonly ICaching<Screen> _cache;
        public ScreenService(IScreenRepository screenRepository, ILogger<ScreenService> logger, ICaching<Screen> cache)
        {
            _screenRepository = screenRepository;
            _cache = cache;
            _logger = logger;
        }
        public IEnumerable<Screen> GetScreens()
        {
            if (!_cache.CheckInCache(AllScreens))
            {
                Func<IEnumerable<Screen>> scr = () => _screenRepository.GetScreens();
                _cache.SetInCache(AllScreens, scr);
                _logger.LogInformation("Set screens in cache");
            }

            return _cache.ReturnValueByKey(AllScreens);
        }

        public Screen GetScreen(int id)
        {
            string key = SingleScreen + id;
            if (!_cache.CheckInCache(key))
            {
                Func<int, Screen> scr = d => _screenRepository.GetScreen(d);
                _cache.SetInCache(key, scr, id);
                _logger.LogInformation($"Set screen {id} in cache");
            }
            return _cache.ReturnSingleValueByKey(key);
        }

        public void AddScreen(Screen screen)
        {
            _logger.LogInformation($"AddScreen, Name = {screen.Name}");
            screen.Name += $"#{screen.Id}";
            _screenRepository.AddScreen(screen);
            _cache.RemoveValueFromCache(AllScreens);
            _logger.LogInformation("Remove all screens from cache");
        }

        public void UpdateScreen(int id, Screen screen)
        {
            _screenRepository.UpdateScreen(id, screen);
            _cache.RemoveValueFromCache(AllScreens);
            _cache.RemoveValueFromCache(SingleScreen+id);
            _logger.LogInformation($"Remove all screens and screen {id} from cache");
        }

        public void DeleteScreen(int id)
        {
           _screenRepository.DeleteScreen(id);
           _cache.RemoveValueFromCache(AllScreens);
           _cache.RemoveValueFromCache(SingleScreen + id);
           _logger.LogInformation($"Remove all screens and screen {id} from cache");
        }
    }
}
