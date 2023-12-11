using UnityEngine;

namespace Platformer
{
    public interface IProtector
    {
        void StartProtection(GameObject invader);
        void FinishProtection(GameObject invader);
    }
}