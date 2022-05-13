using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace PrimeGenCS
{


    public class Program
    {
        static void Main(string[] args)
        {
            IDataAccess _dataAccess = new MockDataAccess();
            PrimeLogic _logic = new PrimeLogic(_dataAccess);

            Stopwatch sw = new Stopwatch();
            long previousSecondsElapsed = 0;
            sw.Start();

            _logic.GeneratePrimes(sw, previousSecondsElapsed);

            sw.Stop();
        }

    }
}
