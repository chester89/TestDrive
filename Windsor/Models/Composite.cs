using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windsor.Models
{
    public interface IComposite
    {
        int One { get; }
        ICalculation Calculation { get; }
        IEnumerable<IConnector> Connectors { get; }
        void DoStuff();
    }

    public class Composite: IComposite
    {
        public int One { get; set; }
        public ICalculation Calculation { get; set; }
        public IEnumerable<IConnector> Connectors { get; set; }

        public void DoStuff()
        {
            throw new NotImplementedException();
        }
    }
}
