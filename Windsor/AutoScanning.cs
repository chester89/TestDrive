using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Windsor
{
    public class AutoScanning
    {
        private IWindsorContainer container;

        public AutoScanning()
        {
            container = new WindsorContainer().Install(FromAssembly.This());
        }
    }
}
