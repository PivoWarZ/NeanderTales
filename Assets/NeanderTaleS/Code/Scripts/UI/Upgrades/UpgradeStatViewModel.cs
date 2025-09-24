using System;
using NeanderTaleS.Code.Scripts.Systems.UpgradeSystem;
using R3;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class UpgradeStatViewModel: IDisposable
    {
        public event Action OnDataChanged;
        
        private Upgrade _model;
        private Sprite _logo;
        private string _title;
        private string _description;
        private int _level;
        IDisposable _disposable;

        public Sprite Logo => _logo;

        public string Title => _title;

        public string Description => _description;

        public int Level => _level;

        public void Init(Upgrade model)
        {
            _model = model;
            _logo = model.Logo;
            _title = model.Id;
            _description = model.Discription;
            _level = model.Level.CurrentValue;
            _disposable = model.Level.Subscribe(DataChanged);
        }

        private void DataChanged(int _)
        {
            Debug.Log($"DataChanged {_}");
            _level = _model.Level.CurrentValue;
            OnDataChanged?.Invoke();
        }
        
        public bool IsMaxLevel => _model.MaxLevel <= _model.Level.CurrentValue;

        public void Dispose()
        {
            _disposable.Dispose();
            _model.Level.Dispose();
        }
    }
}