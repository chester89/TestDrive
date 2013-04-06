using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windsor.Models
{
    public interface IConnectorReader
    {
        void TryVeryHard();
    }

    public interface IConnector
    {
        void Connect(string settings);
    }

    public class NetworkConnector : IConnector
    {
        public void Connect(string settings)
        {

        }
    }

    public class LocalConnector : IConnector
    {
        public void Connect(string settings)
        { }
    }

    public class ConnectorReader : IConnectorReader
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
}
