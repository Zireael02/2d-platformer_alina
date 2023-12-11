using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public enum AnimState
    {
        PlayerIdle = 0,
        PlayerRun = 1,
        PlayerJump = 2,
        PlayerAttack = 3,
        PlayerMove = 4,
        PlayerDeath = 5,
        BirdFly = 6,
        Waterfall = 7,
        Water = 8,
        Coin = 9,
        Moss1 = 10,
        Moss2 = 11,
        Moss3 = 12,
        LianaIdle = 13,
        LianaAttack = 14,
        PredatoryFlowerIdle = 15,
        PredatoryFlowerAttack = 16,
        PredatoryFlowerDeath = 17,
        Portal = 18,
        EnemyBirdFly = 19,
        PatrolBirdFly = 20,
        Chest = 21



    }
    [CreateAssetMenu(fileName ="SpriteAnimatorCfg", menuName = "Configs / Animation", order = 1)]
    
    public class AnimationsConfig : ScriptableObject
    {
        [Serializable]
        public class SpriteSequence
        {
            public AnimState Track;
            public List<Sprite> Sprites = new List<Sprite>();
        }

        public List<SpriteSequence> Sequences = new List<SpriteSequence>();
    }
}
