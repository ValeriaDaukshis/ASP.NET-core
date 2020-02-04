using System.Collections.Generic;
using WebApiCommonn.DataModel;

namespace WebApiCommon.Interfaces
{
    public interface IScreenService
    {
        IEnumerable<Screen> GetScreens();
        Screen GetScreen(int id);
        void AddScreen(Screen screen);
        void UpdateScreen(int id, Screen screen);
        void DeleteScreen(int id);
    }
}
