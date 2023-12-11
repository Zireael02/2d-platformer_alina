using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HealthController
    {
        private LevelObjectView _healthView;
        private Sprite[] _sprites;

        public HealthController(LevelObjectView healthView, Sprite[] sprites)
        {
            _healthView = healthView;
            _sprites = sprites;
        }

        public void Update(int health)
        {
            if (health == 50) _healthView._spriteRenderer.sprite = _sprites[5];
            else if (health == 40) _healthView._spriteRenderer.sprite = _sprites[4];
            else if (health == 30) _healthView._spriteRenderer.sprite = _sprites[3];
            else if (health == 20) _healthView._spriteRenderer.sprite = _sprites[2];
            else if (health == 10) _healthView._spriteRenderer.sprite = _sprites[1];
            else if (health == 0) _healthView._spriteRenderer.sprite = _sprites[0];
        }
    }
}