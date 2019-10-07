using Leopotam.Ecs;
using UnityEngine;

public class GameStartup : MonoBehaviour {
    EcsWorld _world;

    EcsSystems _systems;

    void OnEnable () {
        _world = new EcsWorld ();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
#endif  
        _systems = new EcsSystems (_world)
            .Add (new ObstacleProcessing ())
            .Add (new UserInputProcessing ())
            .Add (new MovementProcessing ())
            .Add (new FoodProcessing ())
            .Add (new DeadProcessing ())
            .Add (new ScoreProcessing ());
        _systems.Initialize ();
#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
    }

    void Update () {
        _systems.Run ();
    }

    void OnDisable () {
        _systems.Dispose ();
        _systems = null;
        _world.Dispose ();
        _world = null;
    }
}