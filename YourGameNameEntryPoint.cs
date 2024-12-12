using System;
using System.Threading.Tasks;
using com.appidea.MiniGamePlatform.CommunicationAPI;
using Managers;
using UnityEngine;

public class YourGameNameEntryPoint : BaseMiniGameEntryPoint
{
    [SerializeField] private GameObject gamePrefab;
    protected override Task LoadInternal()
    {
        var gameManager = Instantiate(gamePrefab);
        gameManager.GetComponent<GameManager>().SetEntryPoint(this);
        return Task.CompletedTask;
    }

    protected override Task UnloadInternal()
    {
        return Task.CompletedTask;
    }
}
