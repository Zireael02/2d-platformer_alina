using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class StoneDoorController : MonoBehaviour
    {
        [SerializeField] QuestObjectView button;
        bool flag = true;
        void FixedUpdate()
        {
            if (button._isComplete && flag)
            {
                flag = false;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
}