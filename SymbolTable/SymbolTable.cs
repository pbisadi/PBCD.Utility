/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBCD.Algorithms.DataStructure
{
    /// <summary>
    /// Conventions:
    /// Values are not null.
    /// The indexer get() returns null if the key not present.
    /// The indexer set() overrides old value with new value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSymbolTable<T> where T : IComparable
    {
        public abstract object this[T index] { get; set; }
        public virtual void Delete(T key) { this[key] = null; }
        public virtual bool Contains(T key) { return this[key] != null; }
        public abstract int Size();
        public abstract IEnumerable<T> Keys();
    }

    public class SymbolTable<Key> : BaseSymbolTable<Key> where Key : IComparable
    {
        public override object this[Key index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override int Size() { throw new NotImplementedException(); }
        public override IEnumerable<Key> Keys() { throw new NotImplementedException(); }
    }
}
*/