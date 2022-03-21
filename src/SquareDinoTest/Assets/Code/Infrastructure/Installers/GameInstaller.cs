using Code.Enemy;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Logic.Waypoints;
using Code.Infrastructure.ObjectPooling;
using Code.Infrastructure.Services.Factory;
using Code.Infrastructure.Services.Input;
using Code.Infrastructure.Services.StaticData;
using Code.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Code.Infrastructure.Installers
{
  public class GameInstaller : MonoInstaller
  {
    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private ObjectPool _objectPool;

    public override void InstallBindings()
    {
      DeclareZenjectSignals();
      BindAssetProvider();
      BindStaticDataService();
      BindInputService();
      BindMainCamera();
      BindWaypoints();
      BindGameFactory();
      BindGameStates();
      BindGameStateMachine();
      BindObjectPool();
    }

    private void BindObjectPool() =>
      Container.Bind<ObjectPool>().FromInstance(_objectPool).AsSingle();

    private void DeclareZenjectSignals()
    {
      SignalBusInstaller.Install(Container);
      Container.DeclareSignal<EnemyDeadSignal>();
    }

    private void BindMainCamera() =>
      Container.Bind<Camera>().FromInstance(Camera.main).AsSingle();

    private void BindInputService()
    {
      if (SystemInfo.deviceType == DeviceType.Handheld)
        Container.BindInterfacesAndSelfTo<MobileInput>().AsSingle();
      else
        Container.BindInterfacesAndSelfTo<MouseInput>().AsSingle();
    }

    private void BindAssetProvider() =>
      Container.BindInterfacesAndSelfTo<AssetProvider>().AsSingle();

    private void BindStaticDataService() =>
      Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle();

    private void BindWaypoints() =>
      Container.Bind<Waypoints>().FromInstance(_waypoints).AsSingle();

    private void BindGameFactory() =>
      Container.BindInterfacesAndSelfTo<GameFactory>().AsSingle();

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<ReadyGameState>().AsTransient();
      Container.BindInterfacesAndSelfTo<LoadLevelState>().AsTransient();
      Container.BindInterfacesAndSelfTo<MoveState>().AsTransient();
      Container.BindInterfacesAndSelfTo<BattleState>().AsTransient();
      Container.BindInterfacesAndSelfTo<EndGameState>().AsTransient();
    }

    private void BindGameStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
      Container.BindInterfacesAndSelfTo<GameStateMachineInitializer>().AsSingle();
    }
  }
}