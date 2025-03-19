using UnityEngine;

public class VerticalSurface : AbstractSurface
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemsHolder.TryGetItem(collision, out item)) item.OnVerticalSurfaceEnter();
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (itemsHolder.TryGetItem(collision, out item))
        {
            item.OnVerticalSurfaceExit();
        } 
    }
}
