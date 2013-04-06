using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Machine.Specifications;
using Xunit;

namespace Windsor.ExplicitScanning
{
    public class Scope
    {
        private IWindsorContainer container;

        public Scope()
        {
            container = new WindsorContainer();
        }

        [Fact]
        public void TheDefaultIsSingleton()
        {
            container.Register(Component.For<ICalculation>().ImplementedBy<StraightForwardCalculation>());

            var first = container.Resolve<ICalculation>();
            var second = container.Resolve<ICalculation>();
            first.ShouldEqual(second);
        }

        [Fact]
        public void CanRegisterWithTransient()
        {
            container.Register(Component.For<ICalculation>().ImplementedBy<StraightForwardCalculation>().LifestyleTransient());

            var first = container.Resolve<ICalculation>();
            var second = container.Resolve<ICalculation>();

            first.ShouldNotBeTheSameAs(second);
        }
    }
}
