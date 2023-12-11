using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PortalView : LevelObjectView
    {
        private Vector2 _portalExitPosition;

        private void Awake()
        {
            _portalExitPosition = new Vector2(gameObject.transform.GetChild(0).transform.transform.position.x, gameObject.transform.GetChild(0).transform.transform.position.y);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out InteractiveObjectView contactView))
            {
                contactView._rb.position= _portalExitPosition;
            }
        }
    }
}