using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppMVC.App_Start
{
    public class NinjectBootstraper : NinjectModules
    {
        private static IKernel kernel;
        internal void Init_Bootstrap()
        {
            NinjectBootstraper bootstraper = new NinjectBootstraper();
            _kernel = new StandatrKernel(bootstraper);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }

        public override void Load()
        {
            Bind<IScreenRepository>.To<ScreenRepository>();
            Bind<IScreenService>.To<ScreenService>();
        }
    }
}