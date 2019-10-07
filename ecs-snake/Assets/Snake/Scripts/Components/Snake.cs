using System.Collections.Generic;
using Leopotam.Ecs;

sealed class Snake {
    [EcsIgnoreNullCheck]
    public readonly List<SnakeSegment> Body = new List<SnakeSegment> (256);

    public SnakeDirection Direction = SnakeDirection.Up;

    public bool ShouldGrow;
}