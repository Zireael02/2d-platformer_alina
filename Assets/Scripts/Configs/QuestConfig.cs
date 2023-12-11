using UnityEngine;

//��� ���������� ���� ������ ������� ���������� �������� �������� ������, �������� �����
//�������� ��� ������ � �� ������ ������� ����� ��������� �������

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