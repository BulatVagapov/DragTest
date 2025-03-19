using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class DraggableItem : MonoBehaviour
{
    private ItemCollidersHolder itemsHolder;
    private Camera cam;
    private ItemScaler itemScaler;

    private Rigidbody2D rb;

    private Vector3 settingPosition;

    [SerializeField] private Collider2D bottomCollider;
    [SerializeField] private Collider2D draggingColider;

    public bool IsDragging { get; private set; }
    public bool OnVerticalSurface { get; private set; }
    public bool IsNeedToStop { get; private set; }

    [Inject]
    private void Constract(ItemCollidersHolder itemsHolder, Camera cam, ItemScaler itemScaler)
    {
        this.itemsHolder = itemsHolder;
        this.cam = cam;
        this.itemScaler = itemScaler;
    }

    private void Awake()
    {
        bottomCollider = gameObject.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        itemsHolder.AddItem(bottomCollider, this);
        itemsHolder.AddDraggableItem(draggingColider, this);
    }
    private void Start()
    {
        itemScaler.SetScale(transform);
    }

    private void OnDestroy()
    {
        itemsHolder.RemoveItem(bottomCollider);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
        rb.gravityScale = 0;
        bottomCollider.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            settingPosition = cam.ScreenToWorldPoint(eventData.position);
            settingPosition.z = -0.1f;
            transform.position = settingPosition;
            itemScaler.SetScale(transform);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
        rb.gravityScale = 1;
        bottomCollider.enabled = true;
        draggingColider.enabled = false;
    }

    private void StopFalling()
    {
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0;
        draggingColider.enabled = true;
    }

    public void OnVerticalSurfaceEnter()
    {
        OnVerticalSurface = true;
        rb.gravityScale = 1;
    }

    public void OnVerticalSurfaceExit()
    {
        OnVerticalSurface = false;
        StopFalling();
    }

    public void OnHorizontalSurfaceEnter()
    {
        IsNeedToStop = true;

        if (!OnVerticalSurface)
        {
            StopFalling();
        }
    }

    public void OnHorizontalSurfaceExit()
    {
        IsNeedToStop = false;
    }
}
