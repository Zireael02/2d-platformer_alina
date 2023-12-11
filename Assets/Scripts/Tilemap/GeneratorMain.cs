using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GeneratorMain : MonoBehaviour
    {

        [SerializeField] private LevelObjectView _playerView;
        [SerializeField] private GeneratorLevelView _generatorLevelView;
        public Transform _playerT;

        private PlayerBirdController _playerBirdController;
        private GeneratorController _generatorController;
        private CameraController _cameraController;


        void Awake()
        {
            _playerBirdController = new PlayerBirdController(_playerView);
            _generatorController = new GeneratorController(_generatorLevelView, _playerT);
            _cameraController = new CameraController(_playerView, Camera.main.transform);
            _generatorController.Start();
        }

        void Update()
        {
            _playerBirdController.Update();
            _cameraController.Update();
        }
    }
}