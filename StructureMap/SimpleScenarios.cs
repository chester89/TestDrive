using Machine.Specifications;
using StructureMap;
using Windsor.Models;
using Xunit;

namespace Drive.StructureMap
{
    public class SimpleScenarios
    {
        private IContainer container;

        public SimpleScenarios()
        {
            container = new Container();
        }

        [Fact]
        public void ShouldResolveTypeWithoutExplicitWiring()
        {
            container.GetInstance<SimpleConstruct>().ShouldNotBeNull();
        }
    }
}
