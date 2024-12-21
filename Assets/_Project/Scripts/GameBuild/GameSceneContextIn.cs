using Cinemachine;
using Controller;
using Inventory; 
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSceneContextIn : MonoInstaller
{
    public Transform TransformCamera;
    public GameObject PlayerPrefab;
    public AddSpriteForItem AddSpriteForItem;
    public List<InventoryCell> InventoryCells;
    public CinemachineFreeLook CinemachineFreeLook;
    public Camera CameraPlayer;
    public GameObject AimTargetForCamera;
    public RectTransform RectTransformAimImage;
    public List<GameObject> ItemsData;
    public override void InstallBindings()
    {
        RectTransformImage();

        PlayerCamera();

        CameraAimTarget();
          
        CameraTransform();

        CinemachineFreeLookForCanvas();
            
        AddSpriteItem();

        ListInventoryCell();

        ListGameObjectsInventoryCells();

        Player();  
    }

    public void CameraTransform()
    {
        Container.Bind<Transform>().FromInstance(TransformCamera).AsSingle();
    }

    public void PlayerCamera()
    {
        Container.Bind<Camera>().FromInstance(CameraPlayer).AsSingle();
    }

    public void CameraAimTarget()
    {
        Container.Bind<GameObject>().FromInstance(AimTargetForCamera).AsSingle();
    }

    public void RectTransformImage()
    {
        Container.Bind<RectTransform>().FromInstance(RectTransformAimImage).AsSingle();
    }

    public void Player()
    {
        PlayerController playerController = Container.InstantiatePrefabForComponent<PlayerController>(PlayerPrefab);
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
    }
    public void CinemachineFreeLookForCanvas()
    {
        Container.Bind<CinemachineFreeLook>().FromInstance(CinemachineFreeLook).AsSingle();
    }
    public void AddSpriteItem()
    {
        Container.Bind<AddSpriteForItem>().FromInstance(AddSpriteForItem).AsSingle();
    }

    public void ListInventoryCell()
    {
        Container.Bind<List<InventoryCell>>().FromInstance(InventoryCells).AsSingle();
    }

    public void ListGameObjectsInventoryCells()
    {
        Container.Bind<List<GameObject>> ().FromInstance(ItemsData).AsSingle();
    }
}
