using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Machine.Specifications;
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
                .ImplementedBy<NetworkConnection>().DependsOn(new Dependency[] { Dependency.OnValue<int>(5)}));
            container.Resolve<INetworkConnection>().ShouldBeOfType<INetworkConnection>();
        }

        [Fact]
        public void ShouldResolveCollectionParameter()
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Register( Component.For<IConnectorReader>().ImplementedBy<ConnectorReader>(),
                                Component.For<IConnector>().ImplementedBy<NetworkConnector>(),
                                Component.For<IConnector>().ImplementedBy<LocalConnector>());
            container.Resolve<IConnectorReader>().ShouldBeOfType<IConnectorReader>();
        }
    }

    public interface IConnectorReader
    {
        void TryVeryHard();
    }

    public interface IConnector
    {
        void Connect(string settings);
    }

    public class NetworkConnector: IConnector
    {
        public void Connect(string settings)
        {
            
        }
    }

    public class LocalConnector: IConnector
    {
        public void Connect(string settings)
        {}
    }

    public class ConnectorReader: IConnectorReader
    {
        private readonly IEnumerable<IConnector> _connectors;

        public ConnectorReader(IEnumerable<IConnector> connectors)
        {
            _connectors = connectors;
        }

        public void TryVeryHard()
        {
            
        }
    }

    public interface ICalculation
    {
        int Sum(int x, int y);
    }

    public class StraightForwardCalculation: ICalculation
    {
        public int Sum(int x, int y)
        {
            return x + y;
        }
    }

    public interface IFinancialStrategy
    {
        void Speak();
    }

    public class FinancialStrategy: IFinancialStrategy
    {
        private readonly ICalculation calculation;

        public FinancialStrategy(ICalculation calculation)
        {
            this.calculation = calculation;
        }

        public void Speak()
        {
            calculation.Sum(10, 5);
        }
    }

    public interface INetworkConnection
    {
        void Connect(string address);
    }

    public class NetworkConnection: INetworkConnection
    {
        private readonly int _defaultMode;

        public NetworkConnection(int defaultMode)
        {
            _defaultMode = defaultMode;
        }

        public void Connect(string address)
        {
            
        }
    }
}
