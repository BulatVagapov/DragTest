using UnityEngine;

public class ItemScaler
{
    private Transform bottomPoint;
    private Transform topPoint;
    private Vector3 maxScale;
    private Vector3 minScale;

    private float distanse;
    private float scaleIntarval;

    private Vector3 currentScale;
    private float curentScaleFloat;
    private float treveledDistance;
    private float traveledScaleIntarval;

    public ItemScaler(Transform bottomPoint, Transform topPoint, Vector3 maxScale, Vector3 minScale)
    {
        this.bottomPoint = bottomPoint;
        this.topPoint = topPoint;
        this.maxScale = maxScale;
        this.minScale = minScale;

        distanse = topPoint.position.y - bottomPoint.position.y;
        scaleIntarval = maxScale.x - minScale.x;
    }

    public void SetScale(Transform transform)
    {
        if(transform.position.y >= topPoint.position.y)
        {
            transform.localScale = minScale;
        }
        else if(transform.position.y <= bottomPoint.position.y)
        {
            transform.localScale = maxScale;
        }
        else
        {
            treveledDistance = transform.position.y - bottomPoint.position.y;

            traveledScaleIntarval = (treveledDistance * scaleIntarval) / distanse;
            curentScaleFloat = scaleIntarval * traveledScaleIntarval;
            currentScale = maxScale;
            currentScale.x -= traveledScaleIntarval;
            currentScale.y -= traveledScaleIntarval;
            transform.localScale = currentScale;
        }
    }
}
