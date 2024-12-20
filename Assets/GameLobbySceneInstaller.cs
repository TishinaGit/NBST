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
        Container.Bind<CinemachineFreeLook>().FromInstance(CinemachineFreeLook).AsSingle(); 
        Container.Bind<Transform>().FromInstance(TransformCamera).AsSingle();
         
        PlayerController playerController = Container.InstantiatePrefabForComponent<PlayerController>(PlayerPrefab);
        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle(); 

        Container.Bind<AddSpriteForItem>().FromInstance(AddSpriteForItem).AsSingle();

        Container.Bind<List<InventoryCell>>().FromInstance(_inventoryCells).AsSingle();
    }
}

 