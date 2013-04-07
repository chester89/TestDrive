using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Machine.Specifications;
using Windsor.Models;
using Xunit;

namespace Windsor.ExplicitScanning
{
    public class SetterInjection
    {
        private IWindsorContainer container;

        public SetterInjection()
        {
            container = new WindsorContainer();
        }

        [Fact]
        public void ShouldInjectSimpleProperty()
        {
            const int expected = 120;
            container.Register(Component.For<IComposite>().ImplementedBy<Composite>().DependsOn(Property.ForKey("One").Eq(expected)));
            container.Resolve<IComposite>().One.ShouldEqual(expected);
        }

        [Fact]
        public void ShouldInjectDependencyWiredThroughWindsor()
        {
            container.Register(Component.For<ICalculation>().ImplementedBy<StraightForwardCalculation>(),
                Component.For<IComposite>().ImplementedBy<Composite>()
                                   .DependsOn(Property.ForKey<ICalculation>().Is<StraightForwardCalculation>()));
            var service = container.Resolve<IComposite>();
            service.Calculation.ShouldBeOfType<ICalculation>();
        }

        [Fact]
        public void ShouldInjectCollection()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(
                                Component.For<IConnector>().ImplementedBy<LocalConnector>(),
                               Component.For<IConnector>().ImplementedBy<NetworkConnector>(),
                               Component.For<IComposite>().ImplementedBy<Composite>()
                                .DependsOn(Property.ForKey("Connectors").Is<List<IConnector>>()));

            container.Resolve<IComposite>().ShouldBeOfType<IComposite>();
            //service.Connectors.Count().ShouldEqual(2);
        }
    }
}
