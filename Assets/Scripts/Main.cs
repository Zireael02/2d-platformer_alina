using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private InteractiveObjectView _playerView;
        [SerializeField] private CannonView _cannonView;
        [SerializeField] private LevelObjectView _BirdView;
        [SerializeField] private LevelObjectView _enemyBirdView;
        [SerializeField] private LevelObjectView _patrolBirdView;
        [SerializeField] private LevelObjectView _healthView;
        [SerializeField] private List<LevelObjectView> _waterList;
        [SerializeField] private List<LevelObjectView> _waterfallList;
        [SerializeField] private List<LevelObjectView> _coinList;
        [SerializeField] private List<PortalView> _portalList;
        [SerializeField] private List<LevelObjectView> _moss1List;
        [SerializeField] private List<LevelObjectView> _moss2List;
        [SerializeField] private List<LevelObjectView> _moss3List;
        [SerializeField] private List<LevelObjectView> _lianaList;
        [SerializeField] private List<LevelObjectView> _predatoryFlowerList;
        [SerializeField] private Sprite[] _healthSprites;
        [SerializeField] private AIConfig _aiConfig;

        [Header("Protector AI")]
        [SerializeField] private AIDestinationSetter _protectorAIDestinationSetter;
        [SerializeField] private AIPatrolPath _protectorAIPatrolPath;
        [SerializeField] private LevelObjectTrigger _protectedZoneTrigger;
        [SerializeField] private Transform[] _protectorWaypoints;

      
        //[SerializeField] private QuestObjectView _singleQuestItem;
        
        //[SerializeField] private QuestView _questView;


        private AnimationsConfig _config;
        private SpriteAnimController _animator;

        private PlayerController _playerController;
        private CannonController _cannonController;
        private EmiterController _emiterController;
        private BirdController _birdController;
        private HealthController _healthController;
        private EnemyBirdController _enemybirdController;
        private SimplePatrolAIController _simplePatrolAIController;

        //private QuestConfiiguratorController _questConfiigurator;
        
        //private QuestController _questController;



        private ProtectorAI _protectorAI;
        private ProtectedZone _protectedZone;


        private void Awake()
        {
            _playerController = new PlayerController(_playerView);
            _cannonController = new CannonController(_cannonView._muzzleT, _playerView._transform);
            _birdController = new BirdController(_BirdView, _playerView);
            _emiterController = new EmiterController(_cannonView._bullets, _cannonView._emitterT);
            _healthController = new HealthController(_healthView, _healthSprites);
            _enemybirdController = new EnemyBirdController(_enemyBirdView);

            //_questConfiigurator = new QuestConfiiguratorController(_questView, _playerView);
            //_questConfiigurator.Start();

            //
            //_questController = new QuestController(_playerView, new QuestCoinModel(), _singleQuestItem);
            //_questController.Reset();


            _simplePatrolAIController = new SimplePatrolAIController(_patrolBirdView, new SimplePatrolAIModel(_aiConfig));
            _config = Resources.Load<AnimationsConfig>("SpriteAnimCfg");
            _animator = new SpriteAnimController(_config);

            _protectorAI = new ProtectorAI(_playerView, new PatrolAIModel(_protectorWaypoints), _protectorAIDestinationSetter, _protectorAIPatrolPath);
            _protectorAI.Init();

            _protectedZone = new ProtectedZone(_protectedZoneTrigger, new List<IProtector> { _protectorAI });
            _protectedZone.Init();



            for (int i = 0; i < _waterList.Count; i++)
            {
                _animator.StartAnimation(_waterList[i]._spriteRenderer, AnimState.Water, true, 10f);
            }
            for (int i = 0; i < _waterfallList.Count; i++)
            {
                _animator.StartAnimation(_waterfallList[i]._spriteRenderer, AnimState.Waterfall, true, 10f);
            }
            for (int i = 0; i < _coinList.Count; i++)
            {
                _animator.StartAnimation(_coinList[i]._spriteRenderer, AnimState.Coin, true, 10f);
            }
            for (int i = 0; i < _portalList.Count; i++)
            {
                _animator.StartAnimation(_portalList[i]._spriteRenderer, AnimState.Portal, true, 10f);
            }
            for (int i = 0; i < _moss1List.Count; i++)
            {
                _animator.StartAnimation(_moss1List[i]._spriteRenderer, AnimState.Moss1, true, 10f);
            }
            for (int i = 0; i < _moss2List.Count; i++)
            {
                _animator.StartAnimation(_moss2List[i]._spriteRenderer, AnimState.Moss2, true, 10f);
            }
            for (int i = 0; i < _moss3List.Count; i++)
            {
                _animator.StartAnimation(_moss3List[i]._spriteRenderer, AnimState.Moss3, true, 10f);
            }
            for (int i = 0; i < _lianaList.Count; i++)
            {
                _animator.StartAnimation(_lianaList[i]._spriteRenderer, AnimState.LianaIdle, true, 3f);
            }
            for (int i = 0; i < _predatoryFlowerList.Count; i++)
            {
                _animator.StartAnimation(_predatoryFlowerList[i]._spriteRenderer, AnimState.PredatoryFlowerIdle, true, 4f);
            }

        }


        void Update()
        {
            _playerController.Update();
            _cannonController.Update();
            _emiterController.Update(_playerView.gameObject);
            _birdController.Update();
            _animator.Update();
            _healthController.Update(_playerController._health);
            _simplePatrolAIController.Update();
            _enemybirdController.Update();
        }

        private void FixedUpdate()
        {
            _simplePatrolAIController.FixedUpdate();
            _enemybirdController.FixedUpdate();
        }


        private void OnDestroy()
        {
            _protectorAI.DeInit();
            _protectedZone.DeInit();

        }


    }
}