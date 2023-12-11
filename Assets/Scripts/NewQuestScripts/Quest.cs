using Platformer;
using System;

public sealed class Quest : IQuest
{
    #region Fields
    private readonly InteractiveObjectView _playerView;
    private readonly QuestObjectView _view;
    private readonly IQuestModel _model;
    private bool _active;
    #endregion
    #region Life Cycle
    public Quest(InteractiveObjectView playerView, QuestObjectView view, IQuestModel model)
    {
        _playerView = playerView;
        _view = view != null ? view : throw new ArgumentNullException(nameof(view));
        _model = model != null ? model : throw new ArgumentNullException(nameof(model));
        _active = false;
    }
    #endregion
    #region Methods
    private void OnContact(QuestObjectView QuestItem)
    {

        if (QuestItem != null)
        {
            if (_model.TryComplete(QuestItem.gameObject))
            {
                if (QuestItem == _view)
                {
                    Complete();
                }
            }
        }

        //var completed = _model.TryComplete(QuestItem.gameObject);
        //Debug.Log("Contact");
        //if (completed) Complete();
    }
    private void Complete()
    {
        if (!_active) return;
        _active = false;
        IsCompleted = true;
        _playerView.OnComplete -= OnContact;
        _view.ProcessComplete();
        _view._isComplete = true;
        OnCompleted();
        
    }
    private void OnCompleted()
    {
        Completed?.Invoke(this, this);
    }
    #endregion
    #region IQuest
    public event EventHandler<IQuest> Completed;
    public bool IsCompleted { get; private set; }
    public void Reset()
    {
        if (_active) return;
        _active = true;
        IsCompleted = false;
        _playerView.OnComplete += OnContact;
        _view.ProcessActivate();
        //Debug.Log("Reset");
    }
    public void Dispose()
    {
        _playerView.OnComplete -= OnContact;
    }
    #endregion
}

