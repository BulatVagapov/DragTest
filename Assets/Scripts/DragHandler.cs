using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class DragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RaycastHit2D hit;
    private Vector3 rayPosition;
    [Inject] private Camera cam;
    [Inject] private ItemCollidersHolder itemsHolder;
    [Inject] private CameraMovement cameraMovement;
    private DraggableItem item;

    public UnityEvent ItemIsDraggedEvent = new();
    public UnityEvent ItemIsDropedEvent = new();
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        rayPosition = cam.ScreenToWorldPoint(eventData.position);

        hit = Physics2D.Raycast(rayPosition, Vector3.forward);

        itemsHolder.TryGetDraggableItem(hit.collider, out item);
        if (item != null)
        {
            item.OnBeginDrag(eventData);
            ItemIsDraggedEvent?.Invoke();
        }            
        else cameraMovement.OnBeginDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null) item.OnDrag(eventData);
        else cameraMovement.OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            item.OnEndDrag(eventData);
            ItemIsDropedEvent?.Invoke();
        }
    }
}
