using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Machine.Specifications;
using Windsor.Models;
using Xunit;

namespace Windsor.ExplicitScanning
{
    public class SimpleScenarios
    {
        private IWindsorContainer container;

        public SimpleScenarios()
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

        [Fact]
        public void ShouldResolveNamedDependency()
        {
            container.Register(Component.For<IConnector>().ImplementedBy<LocalConnector>().Named("local"),
                               Component.For<IConnector>().ImplementedBy<NetworkConnector>().Named("network"));

            container.Resolve<IConnector>("local").ShouldBeOfType<LocalConnector>();
            container.Resolve<IConnector>("network").ShouldBeOfType<NetworkConnector>();
        }

        [Fact]
        public void ShouldResolveATypeNotWiredThroughContainer()
        {
            container.Register(AllTypes.FromThisAssembly().Pick().WithService.DefaultInterfaces()
                .Configure(c => c.LifestyleTransient()));
            container.Resolve<SimpleConstruct>().ShouldNotBeNull();
        }
    }
}
