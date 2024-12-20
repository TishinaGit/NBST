using Cinemachine;
using Controller;
using Inventory;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
 
public class GameLobbySceneInstaller : MonoInstaller
{
    public CinemachineFreeLook CinemachineFreeLook;
    public Transform TransformCamera;
    public GameObject PlayerPrefab;
    public AddSpriteForItem AddSpriteForItem;
    public List<InventoryCell> _inventoryCells; 
   

    public override void InstallBindings()
    { 
        CameraTransform();
          
        CinemachineFreeLookForCanvas();

        Player();

        AddSpriteItem();

        ListInventoryCell(); 
    }

   

    public void CameraTransform()
    { 
        Container.Bind<Transform>().FromInstance(TransformCamera).AsSingle();
    }

    public void Player()
    { 
        PlayerController playerController = Container.InstantiatePrefabForComponent<PlayerController>(PlayerPrefab);
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle(); 
    }
    public void CinemachineFreeLookForCanvas()
    { 
        Container.Bind<CinemachineFreeLook>().FromInstance(CinemachineFreeLook).AsSingle().NonLazy(); 
    }
    public void AddSpriteItem()
    {
        Container.Bind<AddSpriteForItem>().FromInstance(AddSpriteForItem).AsSingle(); 
    }

    public void ListInventoryCell()
    {
        Container.Bind<List<InventoryCell>>().FromInstance(_inventoryCells).AsSingle(); 
    }
}
 


 