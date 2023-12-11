using Platformer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SwitchQuestModel : IQuestModel
{
    private const string TargetTag = "QuestCoin";
    #region Methods
    public bool TryComplete(GameObject activator)
    {
        return activator.CompareTag(TargetTag);
    }
    #endregion
}

