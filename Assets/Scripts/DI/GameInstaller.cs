using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private DragHandler dragHandler;

    [SerializeField] private Transform bottomPoint;
    [SerializeField] private Transform topPoint;

    [SerializeField] private Vector3 minScale;
    [SerializeField] private Vector3 maxScale;

    public override void InstallBindings()
    {
        Container.Bind<ItemCollidersHolder>().AsSingle();
        Container.Bind<DragHandler>().FromInstance(dragHandler).AsSingle();
        Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();
        Container.Bind<CameraMovement>().FromInstance(cameraMovement).AsSingle();
        Container.Bind<ItemScaler>().AsSingle().WithArguments(bottomPoint, topPoint, maxScale, minScale);
    }
}