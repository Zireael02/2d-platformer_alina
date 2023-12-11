using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public interface IQuestStory : IDisposable
    {
        bool IsDone { get; }

        void ResetQuests();
    }
}