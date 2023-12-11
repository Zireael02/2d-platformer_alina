using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class QuestConfiiguratorController
    {
        private QuestObjectView _singleQuestView;
        private QuestController _singleQuestController;
        private QuestStoryConfig[] _questStoryConfigs;
        private QuestObjectView[] _storyQuestViews;
        private QuestCoinModel _questCoinModel;

        private List<IQuestStory> _questStoriesList;
        private InteractiveObjectView _player;
        private Dictionary<QuestType, Func<IQuestModel>> _questFactory = new Dictionary<QuestType, Func<IQuestModel>>(10);
        private Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>> _questStoryFactory = new Dictionary<QuestStoryType, Func<List<IQuest>, IQuestStory>>(10);

        public QuestConfiiguratorController(QuestView questView, InteractiveObjectView player)
        {
            _singleQuestView = questView._singleQuest;
            _questStoryConfigs = questView._storyConfig;
            _storyQuestViews = questView._questObject;
            _questCoinModel = new QuestCoinModel();
            _player = player;
        }

        public void Start()
        {
            _singleQuestController = new QuestController(_player, _questCoinModel, _singleQuestView);
            _singleQuestController.Reset();

            _questFactory.Add(QuestType.Coins, () => new QuestCoinModel());
            _questStoryFactory.Add(QuestStoryType.Common, questCollection => new QuestStoryController(questCollection));

            _questStoriesList = new List<IQuestStory>();
            foreach (QuestStoryConfig cfg in _questStoryConfigs)
            {
                _questStoriesList.Add(CreateQuestStory(cfg));
            }
        }

        private IQuest CreateQuest(QuestConfig cfg)
        {
            int questID = cfg.id;
            QuestObjectView qView = _storyQuestViews.FirstOrDefault(value => value._id == cfg.id);

            if (qView == null)
            {
                Debug.Log("No View");
                return null;
            }
            if (_questFactory.TryGetValue(cfg.type, out var factory))
            {
                IQuestModel qModel = factory.Invoke();
                return new QuestController(_player, qModel, qView);
            }
            Debug.Log("No Model");
            return null;
        }

        private IQuestStory CreateQuestStory(QuestStoryConfig cfg)
        {
            List<IQuest> quests = new List<IQuest>();

            foreach (QuestConfig item in cfg.quests)
            {
                IQuest quest = CreateQuest(item);

                if (quest == null) continue;

                quests.Add(quest);
                Debug.Log("Add Quest");
            }

            return _questStoryFactory[cfg.questStoryType].Invoke(quests);

        }


    }
}