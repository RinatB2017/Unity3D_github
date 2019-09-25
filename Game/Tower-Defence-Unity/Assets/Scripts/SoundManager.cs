using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager> {

    [SerializeField] private AudioClip arrow;
    [SerializeField] private AudioClip death;
    [SerializeField] private AudioClip fireball;
    [SerializeField] private AudioClip gameover;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip level;
    [SerializeField] private AudioClip newgame;
    [SerializeField] private AudioClip rock;
    [SerializeField] private AudioClip towerbuilt;

    public AudioClip Arrow { get { return arrow; } }
    public AudioClip Death { get { return death; } }
    public AudioClip Fireball { get { return fireball; } }
    public AudioClip Gameover { get { return gameover; } }
    public AudioClip Hit { get { return hit; } }
    public AudioClip Level { get { return level; } }
    public AudioClip Newgame { get { return newgame; } }
    public AudioClip Rock { get { return rock; } }
    public AudioClip Towerbuilt { get { return towerbuilt; } }


}
