using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.UI.Upgrades
{
    public class UpgradeStatView: MonoBehaviour
    {
        public event Action<string> OnUpgradeRequest;
        public event Action<UpgradeStatView> OnAllUpgradeComplete;
        
        [SerializeField] private Image _background;
        [SerializeField] private Image _logo;
        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private Button _upgradeButton;
        private UpgradeStatViewModel _viewModel;

        public void Construct(UpgradeStatViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnDataChanged += Refresh;
            _upgradeButton.onClick.AddListener(UpgradeRequest);
            
            Init(viewModel);
        }

        private void Refresh()
        {
            Init(_viewModel);
        }

        public void Init(UpgradeStatViewModel viewModel)
        {
            _logo.sprite = viewModel.Logo;
            _title.text = viewModel.Title;
            _description.text = viewModel.Description;
            _level.text = $"Level {_viewModel.Level}";

            if (_viewModel.IsMaxLevel)
            {
                Debug.Log("Max level upgrade");
                OnAllUpgradeComplete?.Invoke(this);
                _upgradeButton.gameObject.SetActive(false);
                Destroy(gameObject, 0.5f);
            }
        }
        
        private void UpgradeRequest()
        {
            OnUpgradeRequest?.Invoke(_title.text);
        }

        private void OnDestroy()
        {
            _upgradeButton.onClick.RemoveListener(UpgradeRequest);
            _viewModel.Dispose();
        }
    }
}