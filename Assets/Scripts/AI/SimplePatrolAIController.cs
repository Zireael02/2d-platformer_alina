using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class SimplePatrolAIController
    {
        private LevelObjectView _view;
        private SimplePatrolAIModel _model;

        private AnimationsConfig _config;
        private SpriteAnimController _birdAnimator;

        private Vector3 _leftScale = new Vector3(-1, 1, 1);
        private Vector3 _rightScale = new Vector3(1, 1, 1);
        private Transform _birdT;
        private float _animationSpeed = 10f;



        public SimplePatrolAIController(LevelObjectView view, SimplePatrolAIModel model)
        {
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _birdAnimator = new SpriteAnimController(_config);
            _birdAnimator.StartAnimation(view._spriteRenderer, AnimState.PatrolBirdFly, true, _animationSpeed);
            _view = view;
            _model = model;
            _birdT = view._transform;
        }

        public void Update()
        {
            _birdAnimator.Update();
            _birdAnimator.StartAnimation(_view._spriteRenderer, AnimState.PatrolBirdFly, true, _animationSpeed);
            _birdT.localScale = _view._rb.velocity.x < 0 ? _leftScale : _rightScale;
        }



        public void FixedUpdate()
        {
            _view._rb.velocity = _model.CalculateVelocity(_view.transform.position) * Time.deltaTime;
        }
    }
}