using UnityEngine;

namespace Platformer
{
    public class CameraController
    {
        private LevelObjectView _player;
        private Transform _playerT;
        private Transform _cameraT;

        //private float _cameraSpeed = 1.2f;

        private float X;
        private float Y;

        private float offsetX;
        private float offsetY;

        private float _xAxisInput;
        private float _yAxisInput;

        private float _treshhold;

        public CameraController(LevelObjectView player, Transform camera)
        {
            _player = player;
            _playerT = player._transform;
            _cameraT = camera;
            _treshhold = 0.5f;
            
        }

    public void Update()
        {
            _xAxisInput = Input.GetAxis("Horizontal");
            //_yAxisInput = _player._rb.velocity.y;
            _yAxisInput = Input.GetAxis("Vertical");

            X = _playerT.position.x;
            Y = _playerT.position.y;

            if (_xAxisInput > _treshhold) offsetX = 4;
            else if (_xAxisInput < -_treshhold) offsetX = -4;
            else offsetX = 0;

            if (_yAxisInput > _treshhold) offsetY = 4;
            else if (_yAxisInput < -_treshhold) offsetY = -4;
            else offsetY = 0;

            _cameraT.position = Vector3.MoveTowards(_cameraT.position, new Vector3(X + offsetX, Y + offsetY, _cameraT.position.z), Time.deltaTime * 4);

            //_cameraT.position = Vector3.Lerp(_cameraT.position, new Vector3(X + offsetX, Y + offsetY, _cameraT.position.z), Time.deltaTime * _cameraSpeed);
        }
    }
}