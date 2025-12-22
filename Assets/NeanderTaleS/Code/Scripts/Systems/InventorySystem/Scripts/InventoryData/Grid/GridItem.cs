using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.InventoryBase;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NeanderTaleS.Code.Scripts.Systems.InventorySystem.Scripts.InventoryData.Grid
{
    public sealed class GridItem: MonoBehaviour, IPointerClickHandler, IDisposable
    {
        public event Action<GridItem> OnGridLeftClicked;
        public event Action<GridItem> OnGridRightClicked;
        public event Action<GridItem> OnGridActivated;
        public event Action<GridItem> OnDoubleClick;
        public event Action<GridItem> OnGridDestroyed;
        public event Action<GridItem> OnGridReset;
        
        [SerializeField] private Image _icon;
        [SerializeField] private Button _button;
        [SerializeField] private float _doubleClickTime;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Image _activeFrame;
        private InventoryItem _item;
        private bool _isInitialising;
        private bool _isAction = false;
        private CancellationTokenSource _cancell = new ();
        private IDisposable _disposable;
        
        public Button Button => _button;

        public InventoryItem InventoryItem => _item;

        public Image Icon => _icon;

        public bool IsInitialising => _isInitialising;

        private void Awake()
        {
            _button.onClick.AddListener(Clicked);
        }

        public void Initialize(InventoryItem item)
        {
            if (item.Id == String.Empty)
            {
                return;
            }

            _isInitialising = true;
            _item = item;
            _icon.enabled = true;
            _icon.sprite = _item.Meta.Icon;
            gameObject.name = _item.Meta.Name;
        }

        public void InitCountText(int count)
        {
            EnableCountText();
            SetCountText(count);
        }

        private void EnableCountText()
        {
            _countText.gameObject.SetActive(true);
        }

        private void SetCountText(int count)
        {
            _countText.text = count.ToString();
        }

        private void Clicked()
        {
            OnGridLeftClicked?.Invoke(this);

            if (_isAction && _isInitialising)
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

        public void Activate()
        {
            _activeFrame.gameObject.SetActive(true);
            OnGridActivated?.Invoke(this);
        }

        public void Deactivate()
        {
            _activeFrame.gameObject.SetActive(false);
        }

        public void Reset()
        {
            _isInitialising = false;
            _countText.gameObject.SetActive(false);
            OnGridReset?.Invoke(this);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right && _isInitialising)
            {
                OnGridRightClicked?.Invoke(this);
            }
        }

        public bool IsActive()
        {
            return _activeFrame.gameObject.activeSelf;
        }

        private void OnDestroy()
        { 
            OnGridDestroyed?.Invoke(this);
            Dispose();
        }
        
        public void Dispose()
        {
            _cancell.Cancel();
            _disposable?.Dispose();
        }
    }
}