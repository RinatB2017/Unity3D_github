using Leopotam.Ecs;
using UnityEngine;

#if !LEOECS_DISABLE_INJECT
[EcsInject]
#endif
public class ObstacleProcessing : IEcsInitSystem {
    const string ObstacleTag = "Finish";

    EcsWorld _world = null;

    EcsFilter<Obstacle> _obstacles = null;

    void IEcsInitSystem.Initialize () {
        foreach (var unityObject in GameObject.FindGameObjectsWithTag (ObstacleTag)) {
            var tr = unityObject.transform;
            _world.CreateEntityWith<Obstacle> (out var obstacle);
            obstacle.Coords.X = (int) tr.localPosition.x;
            obstacle.Coords.Y = (int) tr.localPosition.y;
            obstacle.Transform = tr;
        }
    }

    void IEcsInitSystem.Destroy () {
        foreach (var i in _obstacles) {
            _obstacles.Components1[i].Transform = null;
            _world.RemoveEntity (_obstacles.Entities[i]);
        }
    }
}