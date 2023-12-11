using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class QuestObjectView : LevelObjectView
    {
        public int Id => _id;

        public Image _image;

        public Color _comlpetedColor;
        public int _id;
        public bool _isComplete;
        private Color _defaultColor;


        private void Awake()
        {
            if (_spriteRenderer != null) _defaultColor = _spriteRenderer.color;
            //if (_image != null) _defaultColor = _image.color;
        }

        public void ProcessComplete()
        {

            if (_image != null) _image.color = _comlpetedColor;
            if (_spriteRenderer != null) _spriteRenderer.color = _comlpetedColor;
            _isComplete = true;
        }

        public void ProcessActivate()
        {
            if (_image != null) _image.color = Color.white;
            if (_spriteRenderer != null) _spriteRenderer.color = _defaultColor;
            _isComplete = false;
        }
    }
}