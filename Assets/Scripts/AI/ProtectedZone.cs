using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ProtectedZone
    {
        private List<IProtector> _protectors;
        private LevelObjectTrigger _view;

        public ProtectedZone(LevelObjectTrigger view, List<IProtector> protectors)
        {
            _view = view;
            _protectors = protectors;
        }

        public void Init()
        {
            _view.TriggerEnter += OnContact;
            _view.TriggerExit += OnExit;
        }

        public void DeInit()
        {
            _view.TriggerEnter -= OnContact;
            _view.TriggerExit -= OnExit;
        }

        private void OnContact(GameObject player)
        {
            foreach (var prtector in _protectors)
                prtector.StartProtection(player);
        }

        private void OnExit(GameObject player)
        {
            foreach (var prtector in _protectors)
                prtector.FinishProtection(player);
        }


    }
}