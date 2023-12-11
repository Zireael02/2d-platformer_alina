using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//объект, который будет хранить в себе данные о цепочке квестов

namespace Platformer
{
    public enum QuestStoryType
    {
        Common,
        Resettable
    }

    [CreateAssetMenu(fileName = "QuestStoryCfg", menuName = "Configs / QuestSystem / QuestStoryCfg", order = 1)]
    public class QuestStoryConfig : ScriptableObject
    {
        public QuestConfig[] quests;
        public QuestStoryType questStoryType;

    }
}