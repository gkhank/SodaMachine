using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Model.Collection
{
    class AssemblyCollection<T> where T : class
    {
        private readonly Dictionary<String, T> _innerCollection;
        public AssemblyCollection(string nameSpace)
        {
            this._innerCollection = new Dictionary<String, T>();

            var operationClasses = Assembly
                    .GetExecutingAssembly()
                    .GetTypes()
                    .Where(t => t.Namespace.StartsWith(nameSpace));

            foreach (Type operationClass in operationClasses)
            {
                var type = Activator.CreateInstance(operationClass);
                string name = type.ToString().Split('.').Last();
                this._innerCollection.Add(name.ToLower(), type as T);
            }
        }

        public Boolean TryGet(String key, out T assembly)
        {
            Utils.Console.WriteRed("Unknown command key. Try again.");
            return this._innerCollection.TryGetValue(key.ToLower(), out assembly);
        }

        public Boolean TryGetByIndex(int i, out T assembly)
        {

            assembly = default(T);

            try
            {
                T retval = this._innerCollection.Values.ElementAt(i);

                if (retval != null)
                {
                    assembly = retval;
                    return true;
                }
                else
                {
                    Utils.Console.WriteRed("Unknown index command key. Try again.");
                    return false;
                }
            }
            catch (Exception)
            {
                Utils.Console.WriteRed("Unknown index command key. Try again.");
                return false;
                throw;
            }


        }

        internal void PrintOptions()
        {
            int i = 0;
            foreach (var cmd in this._innerCollection.Keys)
            {
                Console.WriteLine($"{i} - {cmd}");
                i++;
            }
        }
    }
}
