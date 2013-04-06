using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Machine.Specifications;
using Windsor.Models;
using Xunit;

namespace Windsor.ExplicitScanning
{
    public class SimpleRetrieving
    {
        private IWindsorContainer container;

        public SimpleRetrieving()
        {
            container = new WindsorContainer();
        }

        [Fact]
        public void ShouldRegisterSimpleDependency()
        {
            container.Register(Component.For<ICalculation>().ImplementedBy<StraightForwardCalculation>());
            container.Resolve<ICalculation>().ShouldBeOfType<ICalculation>();
        }

        [Fact]
        public void ShouldRegisterWithAutoWiredPararameter()
        {
            container.Register(Component.For<IFinancialStrategy>().ImplementedBy<FinancialStrategy>(),
                               Component.For<ICalculation>().ImplementedBy<StraightForwardCalculation>());
            container.Resolve<IFinancialStrategy>().ShouldBeOfType<IFinancialStrategy>();
        }

        [Fact]
        public void ShouldRegisterWithExplicitParameters()
        {
            container.Register(Component.For<INetworkConnection>()
                                   .ImplementedBy<NetworkConnection>().DependsOn(new Dependency[]
                                                                                     {
                                                                                         Dependency.OnValue<int>(5)
                                                                                     }));
            container.Resolve<INetworkConnection>().ShouldBeOfType<INetworkConnection>();
        }

        [Fact]
        public void ShouldResolveCollectionParameter()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register(Component.For<IConnectorReader>().ImplementedBy<ConnectorReader>(),
                               Component.For<IConnector>().ImplementedBy<NetworkConnector>(),
                               Component.For<IConnector>().ImplementedBy<LocalConnector>());
            container.Resolve<IConnectorReader>().ShouldBeOfType<IConnectorReader>();
        }
    }
}
