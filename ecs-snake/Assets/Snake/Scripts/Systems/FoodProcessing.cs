using Leopotam.Ecs;
using UnityEngine;

#if !LEOECS_DISABLE_INJECT
[EcsInject]
#endif
sealed class FoodProcessing : IEcsInitSystem, IEcsRunSystem {
    const string FoodTag = "Respawn";

    // TODO: get correct world size from scene.
    const int WorldWidth = 24;

    const int WorldHeight = 15;

    EcsWorld _world = null;

    EcsFilter<Food> _foodFilter = null;

    EcsFilter<Snake> _snakeFilter = null;

    void IEcsInitSystem.Initialize () {
        foreach (var unityObject in GameObject.FindGameObjectsWithTag (FoodTag)) {
            var tr = unityObject.transform;
            _world.CreateEntityWith<Food> (out var food);
            food.Coords.X = (int) tr.localPosition.x;
            food.Coords.Y = (int) tr.localPosition.y;
            food.Transform = tr;
        }
    }

    void IEcsInitSystem.Destroy () {
        foreach (var i in _foodFilter) {
            _foodFilter.Components1[i].Transform = null;
            _world.RemoveEntity (_foodFilter.Entities[i]);
        }
    }

    void IEcsRunSystem.Run () {
        foreach (var snakeEntityId in _snakeFilter) {
            var snake = _snakeFilter.Components1[snakeEntityId];
            var snakeCoords = snake.Body[snake.Body.Count - 1].Coords;
            foreach (var foodEntityId in _foodFilter) {
                var food = _foodFilter.Components1[foodEntityId];
                if (food.Coords.X == snakeCoords.X && food.Coords.Y == snakeCoords.Y) {
                    snake.ShouldGrow = true;

                    // create score change event.
                    _world.CreateEntityWith<ScoreChangeEvent> (out var changeScore);
                    changeScore.Amount = 1;

                    // respawn food at new position.
                    food.Coords.X = Random.Range (1, WorldWidth);
                    food.Coords.Y = Random.Range (1, WorldHeight);
                    food.Transform.localPosition = new Vector3 (food.Coords.X, food.Coords.Y, 0f);
                }
            }
        }
    }
}