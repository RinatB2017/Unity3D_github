using Leopotam.Ecs;
using UnityEngine;

#if !LEOECS_DISABLE_INJECT
[EcsInject]
#endif
sealed class DeadProcessing : IEcsRunSystem {
    EcsWorld _world = null;

    EcsFilter<Snake> _snakeFilter = null;

    EcsFilter<SnakeSegment> _snakeSegmentFilter = null;

    EcsFilter<Obstacle> _obstacleFilter = null;

    void IEcsRunSystem.Run () {
        foreach (var snakeIdx in _snakeFilter) {
            var snakeEntity = _snakeFilter.Entities[snakeIdx];
            var snake = _snakeFilter.Components1[snakeIdx];
            var snakeHead = snake.Body[snake.Body.Count - 1];
            var snakeCoords = snakeHead.Coords;
            foreach (var obstacleIdx in _obstacleFilter) {
                var obstacle = _obstacleFilter.Components1[obstacleIdx];
                if (snakeCoords.X == obstacle.Coords.X && snakeCoords.Y == obstacle.Coords.Y) {
                    snake.Body.Clear ();
                    _world.RemoveEntity (snakeEntity);
                    Debug.Log ("Snake killed");
                }
            }
            foreach (var snakeSegmentIdx in _snakeSegmentFilter) {
                var segment = _snakeSegmentFilter.Components1[snakeSegmentIdx];
                if (segment.Coords.X == snakeCoords.X && segment.Coords.Y == snakeCoords.Y && segment != snakeHead) {
                    snake.Body.Clear ();
                    _world.RemoveEntity (snakeEntity);
                    Debug.Log ("Snake killed");
                    break;
                }
            }
        }
        if (_snakeFilter.IsEmpty ()) {
            // no snakes - exit.
            Application.Quit ();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}