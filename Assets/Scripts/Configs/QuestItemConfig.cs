using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//конфигурация, описывающую, например,
//диалоги или список необходимых предметов


namespace Platformer
{
    [CreateAssetMenu(fileName = "QuestItemCfg", menuName = "Configs / QuestSystem / QuestItemCfg", order = 1)]
    public class QuestItemConfig : ScriptableObject
    {
        public int questId;
        public List<int> questItemIdCollection;

    }
}