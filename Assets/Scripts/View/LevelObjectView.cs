using System;
using UnityEngine;

namespace Platformer
{
    public class LevelObjectView : MonoBehaviour
    {
        public Transform _transform;
        public SpriteRenderer _spriteRenderer;
        public Collider2D _collider;
        public Rigidbody2D _rb;

        //public Action<LevelObjectView> OnLevelObjectContact { get; set; }

        //private void OnTriggerEnter2D(Collider2D collider)
        //{
        //    var levelObject = collider.gameObject.GetComponent<LevelObjectView>();
        //    OnLevelObjectContact?.Invoke(levelObject);
        //}
    }
}
