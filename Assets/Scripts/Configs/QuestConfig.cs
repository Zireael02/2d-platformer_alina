using UnityEngine;

//Для построения базы данных квестов необходимо выделить ключевые данные, которыми будут
//обладать все квесты и на основе которых можно построить систему

namespace Platformer
{
    public enum QuestType
    {
        Switch,
        Coins,
        Buttons
    }  
    
    [CreateAssetMenu(fileName = "QuestCfg", menuName = "Configs / QuestSystem / QuestCfg", order = 1)]
    public class QuestConfig : ScriptableObject
    {
        public int id;
        public QuestType type;
    }
}