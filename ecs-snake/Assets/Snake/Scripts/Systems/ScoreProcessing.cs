using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

#if !LEOECS_DISABLE_INJECT
[EcsInject]
#endif
public class ScoreProcessing : IEcsRunSystem, IEcsInitSystem {
    EcsWorld _world = null;

    EcsFilter<Score> _scoreUiFilter = null;

    EcsFilter<ScoreChangeEvent> _scoreChangeFilter = null;

    void IEcsInitSystem.Initialize () {
        foreach (var ui in GameObject.FindObjectsOfType<Text> ()) {
            _world.CreateEntityWith<Score> (out var score);
            score.Amount = 0;
            score.Ui = ui;
            score.Ui.text = FormatText (score.Amount);
        }
    }

    void IEcsInitSystem.Destroy () {
        foreach (var i in _scoreUiFilter) {
            _scoreUiFilter.Components1[i].Ui = null;
            _world.RemoveEntity (_scoreUiFilter.Entities[i]);
        }
    }

    string FormatText (int v) {
        return string.Format ("Score: {0}", v);
    }

    void IEcsRunSystem.Run () {
        foreach (var scoreChangeIdx in _scoreChangeFilter) {
            var amount = _scoreChangeFilter.Components1[scoreChangeIdx].Amount;
            foreach (var scoreUiIdx in _scoreUiFilter) {
                var score = _scoreUiFilter.Components1[scoreUiIdx];
                score.Amount += amount;
                score.Ui.text = FormatText (score.Amount);
            }
            _world.RemoveEntity (_scoreChangeFilter.Entities[scoreChangeIdx]);
        }
    }
}