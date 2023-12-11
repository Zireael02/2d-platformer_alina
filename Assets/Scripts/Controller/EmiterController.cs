using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class EmiterController 
    {
        private List<BulletController> _bulletControllers = new List<BulletController>();
        private Transform _transform;

        private int _index;
        private float _timeTillNextBull;
        private float _startSpeed = 20;
        private float _delay = 2;
        private float _distanceAttack = 10;

        public EmiterController(List<BulletView> bulletViews, Transform emitterTransform)
        {
            _transform = emitterTransform;
            foreach (BulletView bulletView in bulletViews)
            {
                _bulletControllers.Add(new BulletController(bulletView));
            }
        }


        public void Update(GameObject target)
        {
            if (_timeTillNextBull > 0)
            {
                _bulletControllers[_index].Active(false);
                _timeTillNextBull -= Time.deltaTime;
            }
            else
            {
                float distance = Vector2.Distance(_transform.position, target.transform.position);
                if (distance < _distanceAttack)
                {
                    RaycastHit2D hitPlayer = Physics2D.Raycast(_transform.position, _transform.TransformDirection(Vector2.up), 10f);
                    if (hitPlayer.collider != null && hitPlayer.collider.tag == "Player")
                    {
                        _timeTillNextBull = _delay;
                        _bulletControllers[_index].Trow(_transform.position, _transform.up * _startSpeed);
                        _index++;
                        if (_index >= _bulletControllers.Count)
                        {
                            _index = 0;
                        }
                    }
                }
            }
        }
    }
}