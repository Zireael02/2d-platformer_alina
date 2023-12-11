using UnityEngine;

namespace Platformer
{
    public class BirdController 
    {       
        private AnimationsConfig _config;
        private SpriteAnimController _birdAnimator;
        private LevelObjectView _BirdView;

        private int _speed = 1;
        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private float _sightTreshold = 2f;
        private float _animationSpeed = 10f;

        private Transform _birdT;
        private Transform _playerT;

        public BirdController(LevelObjectView bird, InteractiveObjectView player)
        {
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _birdAnimator = new SpriteAnimController(_config);
            _birdAnimator.StartAnimation(bird._spriteRenderer, AnimState.BirdFly, true, _animationSpeed);
            _BirdView = bird;
            _birdT = bird._transform;
            _playerT = player._transform;
        }

        public void Update()
        {
            _birdAnimator.Update();
            _birdAnimator.StartAnimation(_BirdView._spriteRenderer, AnimState.BirdFly, true, _animationSpeed);
            float distance = Vector2.Distance(_birdT.position, _playerT.transform.position);
            if (distance > 5f) _speed = 3;
            else _speed = 1;

            if (distance > _sightTreshold)
            {
                Move();
            }
        }

        void Move()
        {
            _birdT.localScale = (int)(_birdT.position.x  - _playerT.transform.position.x) > 0 ? _leftScale : _rightScale;
            _birdT.position = Vector2.MoveTowards(_birdT.position, _playerT.transform.position, _speed * Time.deltaTime);
        }
    }
}