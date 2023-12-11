using UnityEngine;

namespace Platformer
{
    public class PlayerController
    {        
        private AnimationsConfig _config;
        private SpriteAnimController _playerAnimator;
        private InteractiveObjectView _playerView;
        private ContactPooler _contactPooler;

        private Transform _playerT;
        private Rigidbody2D _rb;

        private float _walkSpeed = 150f;
        private float _animationSpeed = 10f;
        private float _movingTreshold = 0.1f;
        public int _health = 50;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Vector2 _startPosition;

        private bool _isJump;
        private bool _isMoving;

        private float _jumpForce = 9f;
        private float _jumpTreshold = 1f;

        private float _xVelocity = 0;
        private float _yVelocity = 0;
        private float _xAxisInput;

        public PlayerController(InteractiveObjectView player)
        {
            _playerView = player;
            _playerT = player._transform;
            _rb = player._rb;
            _startPosition = _rb.position;
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _playerAnimator = new SpriteAnimController(_config);
            _contactPooler = new ContactPooler(_playerView._collider);
            _playerAnimator.StartAnimation(player._spriteRenderer, AnimState.PlayerIdle, true, _animationSpeed);

            player.TakeDamage += TakeBullet; // подписались на событие
        }

        private void TakeBullet(BulletView bullet)
        {
            _health -= bullet.DamagePoint;
        }

        private void MoveTowards()
        {
            _xVelocity = Time.fixedDeltaTime * _walkSpeed * (_xAxisInput < 0 ? -1 : 1);
            _rb.velocity = new Vector2(_xVelocity, _yVelocity);
            _playerT.localScale = _xAxisInput < 0 ? _leftScale : _rightScale;
        }

        public void Update()
        {
            if (_playerView._isDeathDamage)
            {
                _health = 0;
                _playerView._isDeathDamage = false;
            }

            if (_health <= 0)
            {
                _health = 50;
                _playerView._rb.position = _startPosition;
            }

            if (_health >= 50) _health = 50;


            _playerAnimator.Update();
            _contactPooler.Update();
            _xAxisInput = Input.GetAxis("Horizontal");
            _isJump = Input.GetAxis("Vertical") >0;
            _yVelocity = _rb.velocity.y;
            _isMoving = Mathf.Abs(_xAxisInput) > _movingTreshold;
            _playerAnimator.StartAnimation(_playerView._spriteRenderer, _isMoving ? AnimState.PlayerRun : AnimState.PlayerIdle, true, _animationSpeed);


            if (_isMoving) 
            {
                MoveTowards();
            }
            else
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.PlayerAttack, false, _animationSpeed);
            }
            if (_contactPooler.IsGrounded)

            {              
                if(_isJump && _yVelocity <= _jumpTreshold)
                {
                    _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }                
            }
            else
            {
                if (Mathf.Abs(_yVelocity) > _jumpTreshold)
                {
                    _playerAnimator.StartAnimation(_playerView._spriteRenderer, AnimState.PlayerJump, false, _animationSpeed);
                }
            }

            if ((_contactPooler.LeftContact && !_contactPooler.IsGrounded) || (_contactPooler.RigthContact && !_contactPooler.IsGrounded))
            {
                _xVelocity = 0;
                _rb.velocity = new Vector2(_xVelocity, _rb.velocity.y);
            }

        }
    }
}