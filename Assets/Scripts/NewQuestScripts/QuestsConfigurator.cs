using Platformer;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsConfigurator : MonoBehaviour
{
    [SerializeField] private QuestObjectView _singleQuestView;
    [SerializeField] private InteractiveObjectView _playerView;
    [SerializeField] private QuestStoryConfig[] _questStoryConfigs;
    [SerializeField] private QuestObjectView[] _questObjects;


    public List<IQuestStory> _questStories;
    public List<bool> _questStoriesDone;
    private Quest _singleQuest;

    private readonly Dictionary<QuestType, Func<IQuestModel>> _questFactories = new Dictionary<QuestType, Func<IQuestModel>>
        {
            { QuestType.Switch, () => new SwitchQuestModel() },
        };
    private readonly Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactories = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>
        {
            { QuestStoryType.Common, questCollection => new QuestStory(questCollection) },
            //
            { QuestStoryType.Resettable, questCollection => new ResettableQuestStory(questCollection) },
            //
        };
        


    private void Start()

    {
        _singleQuest = new Quest(_playerView, _singleQuestView, new SwitchQuestModel());
        _singleQuest.Reset();
        _questStories = new List<IQuestStory>();
        foreach (var questStoryConfig in _questStoryConfigs)
        {
            _questStories.Add(CreateQuestStory(questStoryConfig));
            _questStoriesDone.Add(false);
        }
        
    }

    private void FixedUpdate()
    {
        _questStoriesDone[0] = _questStories[0].IsDone;
        _questStoriesDone[1] = _questStories[1].IsDone;
    }

    private void OnDestroy()
    {
        foreach (var questStory in _questStories)
        {
            questStory.Dispose();
        }
        _questStories.Clear();

        //_singleQuest.Dispose();
    }


    private IQuestStory CreateQuestStory(QuestStoryConfig config)
    {
        var quests = new List<IQuest>();
        foreach (var questConfig in config.quests)
        {
            // создаём квест на основе данных из ScriptableObject
            var quest = CreateQuest(questConfig);
            if (quest == null) continue;
            quests.Add(quest);
        }
        // какая логика будет у цепочки определяем по типу QuestStoryType
        return _questStoryFactories[config.questStoryType].Invoke(quests);
    }

    private IQuest CreateQuest(QuestConfig config)
    {
        var questId = config.id;
        var questView = _questObjects.FirstOrDefault(value => value.Id == config.id);
        if (questView == null)
        {
            // пытаемся найти представление для квеста
            Debug.LogWarning($"QuestsConfigurator :: Start : Can't find view of quest { questId.ToString()}");
            return null;
        }
        if (_questFactories.TryGetValue(config.type, out var factory))
        {
            // пытаемся создать модель для квеста по типу QuestType
            var questModel = factory.Invoke();
            return new Quest(_playerView, questView, questModel);
        }
        Debug.LogWarning($"QuestsConfigurator :: Start : Can't create model for quest{ questId.ToString()}");
        return null;
    }
        




}