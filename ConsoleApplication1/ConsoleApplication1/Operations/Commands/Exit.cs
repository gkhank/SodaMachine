using ConsoleApplication1.Model;
using ConsoleApplication1.Model.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Operations.Commands
{
    public class Exit : IVendingOperation
    {
        public void DoOperation(SodaCollection sodaCollection, ref decimal credit)
        {
            System.Environment.Exit(0);
        }
    }
}
