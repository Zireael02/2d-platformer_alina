using System;
using UnityEngine;

namespace Platformer
{
    public class InteractiveObjectView : LevelObjectView
    {
        public Action<BulletView> TakeDamage { get; set; } //создаем событие
        public Action<QuestObjectView> OnComplete { get; set; }
        
        public bool _isDeathDamage;
        

        public void startActionOnComplete(QuestObjectView contactView)
        {
            OnComplete?.Invoke(contactView);
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out LevelObjectView contactView))
            {
                
                if(contactView is QuestObjectView)
                {
                    OnComplete?.Invoke((QuestObjectView)contactView);
                    
                }

                if (contactView is BulletView)
                {
                    TakeDamage?.Invoke((BulletView)contactView); //вызываем событие
                }
            }                        

            if (collision.gameObject.tag == "DeathZone")
            {
                _isDeathDamage = true;
            }
        }
    }
}