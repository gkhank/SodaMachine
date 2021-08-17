using ConsoleApplication1.Model.Collection;

namespace ConsoleApplication1.Model

{
    public interface IVendingOperation
    {
        void DoOperation(SodaCollection sodaCollection, ref decimal credit);
    }
}
