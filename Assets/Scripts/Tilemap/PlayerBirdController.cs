using UnityEngine;

namespace Platformer
{
    public class PlayerBirdController
    {
        private AnimationsConfig _config;
        private SpriteAnimController _playerAnimator;
        private LevelObjectView _playerView;

        private Transform _playerT;
        private Rigidbody2D _rb;

        private float _walkSpeed = 150f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);

        private bool _isMoving;

        private float _xVelocity = 0;
        private float _yVelocity = 0;
        private float _xAxisInput;
        private float _yAxisInput;

        public PlayerBirdController(LevelObjectView player)
        {
            _playerView = player;
            _playerT = player._transform;
            _rb = player._rb;
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.EnemyBirdFly, true, _animationSpeed);
        }

        private void MoveTowards()
        {
            int xMove;
            if (_xAxisInput < 0) xMove = -1;
            else if (_xAxisInput > 0) xMove = 1;
            else xMove = 0;
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (xMove);
            int yMove;
            if (_yAxisInput < 0) yMove = -1;
            else if (_yAxisInput > 0) yMove = 1;
            else yMove = 0;
            _yVelocity = Time.fixedDeltaTime * _walkSpeed * (yMove);
            _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public void Update()
        {
            _playerAnimator.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _yAxisInput = Input.GetAxis("Vertical");
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold || Mathf.Abs(_yAxisInput) > _movingTreshold;
            
            if (_isMoving)
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _yVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            }
        }
    }
}