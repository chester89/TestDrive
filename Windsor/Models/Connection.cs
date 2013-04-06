using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windsor.Models
{
    public interface INetworkConnection
    {
        void Connect(string address);
    }

    public class NetworkConnection : INetworkConnection
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
