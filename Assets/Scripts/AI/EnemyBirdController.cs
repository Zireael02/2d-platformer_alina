using UnityEngine;

namespace Platformer
{
    public class EnemyBirdController
    {
        private LevelObjectView _view;
        private AnimationsConfig _config;
        private SpriteAnimController _birdAnimator;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Transform _birdT;
        private float _animationSpeed = 10f;
        private float _oldPositionX;

        public EnemyBirdController(LevelObjectView bird)
        {
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _birdAnimator = new SpriteAnimController(_config);
            _birdAnimator.StartAnimation(bird._spriteRenderer, AnimState.EnemyBirdFly, true, _animationSpeed);
            _view = bird;
            _birdT = bird._transform;
            _oldPositionX = _view._rb.position.x;
        }

        public void Update()
        {
            _birdAnimator.Update();
            _birdAnimator.StartAnimation(_view._spriteRenderer, AnimState.EnemyBirdFly, true, _animationSpeed);
            _birdT.localScale = _view._rb.position.x < _oldPositionX ? _leftScale : _rightScale;
        }

        public void FixedUpdate()
        {
            _oldPositionX = _view._rb.position.x;
        }

    }
}