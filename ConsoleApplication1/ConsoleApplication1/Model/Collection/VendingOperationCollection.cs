using ConsoleApplication1.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model.Collection
{
    internal class VendingOperationCollection
    {
        private readonly Dictionary<Operation, IVendingOperation> _innerCollection;
        public VendingOperationCollection()
        {
            this._innerCollection = new Dictionary<Operation, IVendingOperation>();
            this._innerCollection.Add(Operation.Insert, new Insert());
            this._innerCollection.Add(Operation.Order, new Order());
            this._innerCollection.Add(Operation.Return, new Return());
            this._innerCollection.Add(Operation.Exit, new Exit());
        }

        public Boolean TryGetOperation(Operation operation, out IVendingOperation vendingOperation)
        {
            return this._innerCollection.TryGetValue(operation, out vendingOperation);
        }

    }
}
