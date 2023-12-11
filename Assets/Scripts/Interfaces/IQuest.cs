using System;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public interface IQuest : IDisposable
    {
        event EventHandler<IQuest> Completed;

        bool IsCompleted { get; }

        void Reset();
    }
}