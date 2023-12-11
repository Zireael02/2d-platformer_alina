using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ExitPlatformController : MonoBehaviour
    {
        [SerializeField] private QuestsConfigurator _questStores;
        [SerializeField] private int _questCount;
        bool flag = true;
        void FixedUpdate()
        {
            if (_questStores._questStoriesDone[_questCount] && flag)
            {
                flag = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255, 255, 255);
            }
        }
    }
}