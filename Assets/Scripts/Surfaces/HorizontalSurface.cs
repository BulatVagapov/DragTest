using UnityEngine;

public class HorizontalSurface : AbstractSurface
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemsHolder.TryGetItem(collision, out item))
        {
            item.OnHorizontalSurfaceEnter();
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (itemsHolder.TryGetItem(collision, out item)) item.OnHorizontalSurfaceExit();
    }
}
