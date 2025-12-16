using System;
using System.Collections.Generic;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid
{
    public class GridsStorage: IDisposable
    {
        public event Action<GridItem> OnGridAdded;
        public event Action<GridItem> OnGridRemoved;
        
        private readonly List<GridItem> _items = new ();

        public List<GridItem> Items => _items;

        public void AddItem(GridItem item)
        {
            _items.Add(item);
            OnGridAdded?.Invoke(item);
        }

        public void RemoveItem(GridItem item)
        {
            _items.Remove(item);
            OnGridRemoved?.Invoke(item);
        }

        public void Dispose()
        {
            _items.Clear();
        }
    }
}