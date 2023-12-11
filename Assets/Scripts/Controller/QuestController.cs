using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class QuestController : IQuest
    {

        private InteractiveObjectView _player;

        private bool _active;
        private IQuestModel _model;
        private QuestObjectView _quest;
        public event EventHandler<IQuest> Completed;

        public bool IsCompleted { get; private set; }
        
        
        public QuestController(InteractiveObjectView player, IQuestModel model, QuestObjectView view)
        {
            _player = player;
            _model = model;
            _active = false;            
            _quest = view;
        }
        
        public void OnContact(QuestObjectView QuestItem)
        {
            if (QuestItem != null)
            {
                if (_model.TryComplete(QuestItem.gameObject))
                {
                    if (QuestItem == _quest)
                    {
                        OnCompleted();
                    }
                }


            }


        }

        public void OnCompleted()
        {
            if (!_active) return;
            _active = false;
            _player.OnComplete -= OnContact;
            _quest.ProcessComplete();
            Completed?.Invoke(this, this);
        }

        public void Reset()
        {
            if (_active) return;
            _active = true;
            _player.OnComplete += OnContact;
            _quest.ProcessActivate();

        }


        public void Dispose()
        {
            _player.OnComplete -= OnContact;
        }
    }
}