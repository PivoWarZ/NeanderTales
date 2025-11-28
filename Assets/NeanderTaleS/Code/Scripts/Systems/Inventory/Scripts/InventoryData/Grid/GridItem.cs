using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryBase;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Grid
{
    public sealed class GridItem: MonoBehaviour, IPointerClickHandler
    {
        public event Action<GridItem> OnGridLeftClicked;
        public event Action<GridItem> OnGridRightClicked;
        public event Action<GridItem> OnGridActivated;
        public event Action<GridItem> OnDoubleClick;
        public event Action<GridItem> OnGridDestroyed;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private float _doubleClickTime;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _activeFrame;
        private InventoryItem _item;
        private bool _isAction = false;
        private CancellationTokenSource _cancell = new ();
        private IDisposable _disposable;
        
        public Button Button => _button;

        public InventoryItem InventoryItem => _item;

        private void Awake()
        {
            _button.onClick.AddListener(Clicked);
        }

        public void Initialize(InventoryItem item)
        {
            _item = item;
            _icon.sprite = _item.Meta.Icon;
            gameObject.name = _item.Meta.Name;
        }

        public void InitCountText(ReactiveProperty<int> count)
        {
            _disposable = count.Subscribe(onNext => _countText.text = count.CurrentValue.ToString());
            _countText.gameObject.SetActive(true);
        }

        private void Clicked()
        {
            OnGridLeftClicked?.Invoke(this);

            if (_isAction)
            {
                OnDoubleClick?.Invoke(this);
                _isAction = false;
                _cancell.Cancel();
            }
            else
            {
                _isAction = true;
                CanDoubleClick();
            }
        }

        private void CanDoubleClick()
        {
            DoubleClickTime(_cancell).Forget();
        }

        private async UniTaskVoid DoubleClickTime(CancellationTokenSource cancell)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_doubleClickTime));
            _isAction = false;
        }

        public void ActivateGrid()
        {
            _activeFrame.gameObject.SetActive(true);
            OnGridActivated?.Invoke(this);
        }

        public void DeactivateGrid()
        {
            _activeFrame.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                OnGridRightClicked?.Invoke(this);
            }
        }

        private void OnDestroy()
        {
            OnGridDestroyed?.Invoke(this);
            _cancell.Cancel();
            _disposable?.Dispose();
        }
    }
}