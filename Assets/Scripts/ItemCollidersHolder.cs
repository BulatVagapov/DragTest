using System.Collections.Generic;
using UnityEngine;

public class ItemCollidersHolder
{
    private Dictionary<Collider2D, DraggableItem> itemsByCollider = new();
    private Dictionary<Collider2D, DraggableItem> itemsByDraggableCollider = new();

    public void AddItem(Collider2D collider, DraggableItem item)
    {
        if (!itemsByCollider.ContainsKey(collider))
        {
            itemsByCollider.Add(collider, item);
        }
    }
    
    public void RemoveItem(Collider2D collider)
    {
        if (itemsByCollider.ContainsKey(collider))
        {
            itemsByCollider.Remove(collider);
        }
    }

    public bool TryGetItem(Collider2D collider, out DraggableItem item)
    {
        if (itemsByCollider.ContainsKey(collider))
        {
            item = itemsByCollider[collider];
            return true;
        }

        item = null;
        return false;
    }

    public void AddDraggableItem(Collider2D collider, DraggableItem item)
    {
        if (!itemsByDraggableCollider.ContainsKey(collider))
        {
            itemsByDraggableCollider.Add(collider, item);
        }
    }

    public void RemoveDraggableItem(Collider2D collider)
    {
        if (itemsByDraggableCollider.ContainsKey(collider))
        {
            itemsByDraggableCollider.Remove(collider);
        }
    }

    public bool TryGetDraggableItem(Collider2D collider, out DraggableItem item)
    {
        if (itemsByDraggableCollider.ContainsKey(collider))
        {
            item = itemsByDraggableCollider[collider];
            return true;
        }

        item = null;
        return false;
    }
}
