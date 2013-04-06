using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windsor.Models
{
    public interface ICalculation
    {
        int Sum(int x, int y);
    }

    public class StraightForwardCalculation : ICalculation
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

    public class FinancialStrategy : IFinancialStrategy
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
}
