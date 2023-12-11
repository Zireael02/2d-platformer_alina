using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BulletView : LevelObjectView
    {
        private int _damagePoint = 10;

        public int DamagePoint { get => _damagePoint; set => _damagePoint = value; }

    }
}