using System.Collections.Generic;
using WebApiCommon.Interfaces;
using WebApiCommonn.DataModel;

namespace WebApiCommon.Implementations.Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly List<Screen> Screens;
        public ScreenRepository()
        {
            Screens = new List<Screen>
            {
                new Screen
                {
                    Id = 1,
                    Name = "Name 1"
                },

                new Screen
                {
                    Id = 2,
                    Name = "Name 2"
                },

                new Screen
                {
                    Id = 3,
                    Name = "Name 3"
                }
            };
        }

        public IEnumerable<Screen> GetScreens()
        {
            return Screens;
        }

        public Screen GetScreen(int id)
        {
            return Screens.Find(x => x.Id == id);
        }

        public void AddScreen(Screen screen)
        {
            Screens.Add(screen);
        }

        public void UpdateScreen(int id, Screen screen)
        {
            var screenToUpdate = GetScreen(id);
            screenToUpdate.Name = screen.Name;
        }

        public void DeleteScreen(int id)
        {
            var screenToDelete = GetScreen(id);
            Screens.Remove(screenToDelete);
        }
    }
}
