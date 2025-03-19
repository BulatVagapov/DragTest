using UnityEngine;
using Zenject;

public abstract class AbstractSurface : MonoBehaviour
{
    [Inject] protected ItemCollidersHolder itemsHolder;
    protected DraggableItem item;

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    protected abstract void OnTriggerExit2D(Collider2D collision);
}
