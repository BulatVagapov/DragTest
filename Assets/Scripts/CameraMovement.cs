using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    private Vector3 startDragPosition;
    private Vector3 startCameraPosition;
    private Vector3 currentCameraPosition;
    private float xDist;
    [Inject] private Camera cam;

    [SerializeField] private Transform leftExtrimePosition;
    [SerializeField] private Transform rightExtrimePosition;
    [SerializeField] private float speed;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startDragPosition = eventData.position;
        currentCameraPosition = cam.transform.position;
        startCameraPosition = cam.transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        xDist = cam.ScreenToWorldPoint(eventData.position).x - cam.ScreenToWorldPoint(startDragPosition).x;

        if (xDist > 0)
        {
            if (startCameraPosition.x - xDist < leftExtrimePosition.position.x)
            {
                xDist = startCameraPosition.x - leftExtrimePosition.position.x;
            }


        }
        else if (xDist < 0)
        {

            if (startCameraPosition.x - xDist > rightExtrimePosition.position.x)
            {
                xDist = startCameraPosition.x - rightExtrimePosition.position.x;
            }
        }

        currentCameraPosition.x = startCameraPosition.x - xDist;

        cam.transform.position = currentCameraPosition;
    }

    public void MoveWithItem(Vector3 targetPosition, float timeDelta)
    {
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPosition, speed * timeDelta);
    }
}
