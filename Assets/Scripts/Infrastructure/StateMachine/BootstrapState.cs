﻿using Assets.Scripts.Factory;
using Assets.Scripts.Infrastructure;
using Assets.Scripts.Services.AssetManagement;

public class BootstrapState : IState
{
    private const string BootstrapSceneName = "BootstrapScene";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly ServiceLocator _serviceLocator;

    public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, ServiceLocator serviceLocator)
    {
        _stateMachine = gameStateMachine;
        _sceneLoader = sceneLoader;
        _serviceLocator = serviceLocator;

        RegisterServices();
    }

    public void Enter()
    {
        _sceneLoader.Load(BootstrapSceneName, EnterLoadLevel);
    }

    public void Exit()
    {

    }

    private void RegisterServices()
    {
        _serviceLocator.RegisterSingle<IAssetProvider>(new AssetProvider());
        _serviceLocator.RegisterSingle<IGameFactory>(new GameFactory(_serviceLocator.Single<IAssetProvider>()));
        _serviceLocator.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
        _serviceLocator.RegisterSingle<ISaveLoadService>(new SaveLoadService(_serviceLocator.Single<IGameFactory>(), _serviceLocator.Single<IPersistentProgressService>()));
    }

    private void EnterLoadLevel() 
        => _stateMachine.Enter<LoadProgressState>();
}
