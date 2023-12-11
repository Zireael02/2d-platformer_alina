using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ChestController : MonoBehaviour
    {
        [SerializeField] private QuestsConfigurator _questStores;
        [SerializeField] private int _questCount;
        [SerializeField] private GameObject _canvasChestCode;

        bool flag = true;

        private AnimationsConfig _config;
        private SpriteAnimController _animator;

        private void Awake()
        {
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _animator = new SpriteAnimController(_config);
        }

        private void Update()
        {
            _animator.Update();
        }

        void FixedUpdate()
        {            
            if (_questStores._questStoriesDone[_questCount] && flag)
            {
                flag = false;
                _animator.StartAnimation(gameObject.GetComponent<LevelObjectView>()._spriteRenderer, AnimState.Chest, false, 10f);
                _canvasChestCode.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (flag) _canvasChestCode.SetActive(true);
            _questStores._questStories[_questCount].ResetQuests();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _canvasChestCode.SetActive(false);
        }


    }
}