using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public interface IQuestModel
    {
        bool TryComplete(GameObject activator);
    }
}