using System;
using Pathfinding;

namespace Platformer
{
    public class AIPatrolPath : AIPath
    {
        public Action TargetReached;

        public override void OnTargetReached()
        {
            base.OnTargetReached();
            DispatchTargetReached();
        }

        protected virtual void DispatchTargetReached()
        {
            TargetReached?.Invoke();
        }
    }
}