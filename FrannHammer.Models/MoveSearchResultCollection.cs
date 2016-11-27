using System.Collections.Generic;

namespace FrannHammer.Models
{
    public class MoveSearchResultCollection<T>
    {
        public IList<T> Items { get; private set; }

        public bool IsInitialized { get; private set; }

        public MoveSearchResultCollection()
        {
            Items = new List<T>();
        }

        public void Initialize()
        {
            IsInitialized = true;
        }

        public void SetItems(IList<T> items)
        {
            Items = items;   
        }
    }
}
