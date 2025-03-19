using UnityEngine;
using Zenject;

public class CameraMovemetTrigger : MonoBehaviour
{
    private Transform itemParent;
    private ItemCollidersHolder itemsHolder;
    private CameraMovement cameraMovement;
    private DragHandler dragHandler;
    protected DraggableItem item;
    [SerializeField] private Transform target;

    [Inject]
    private void Constract(Camera cam, ItemCollidersHolder itemsHolder, CameraMovement cameraMovement, DragHandler dragHandler)
    {
        itemParent = cam.transform;
        this.itemsHolder = itemsHolder;
        this.cameraMovement = cameraMovement;
        this.dragHandler = dragHandler;
    }

    private void Awake()
    {
        dragHandler.ItemIsDraggedEvent.AddListener(OnItemDragEvent);
        dragHandler.ItemIsDropedEvent.AddListener(OnDropItemEvent);
        gameObject.SetActive(false);
        enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (itemsHolder.TryGetDraggableItem(collision, out item))
        {
            if (item.IsDragging)
            {
                enabled = true;
                item.transform.parent = itemParent;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (item != null)
        {
            enabled = false;
            item.transform.parent = null;
            item = null;
        }
    }

    private void FixedUpdate()
    {
        if (item != null)
        {
            if (item.IsDragging)
            {
                cameraMovement.MoveWithItem(target.position, Time.fixedDeltaTime);
            }
        }
    }

    private void OnItemDragEvent()
    {
        gameObject.SetActive(true);
    }

    private void OnDropItemEvent()
    {
        enabled = false;

        if(item != null)
        {
            item.transform.parent = null;
            item = null;
        }

        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        dragHandler.ItemIsDraggedEvent.RemoveListener(OnItemDragEvent);
        dragHandler.ItemIsDropedEvent.RemoveListener(OnDropItemEvent);
    }
}
