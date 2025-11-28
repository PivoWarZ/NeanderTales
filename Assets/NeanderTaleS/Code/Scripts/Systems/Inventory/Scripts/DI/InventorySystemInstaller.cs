using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.EquipPopup;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Installers;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.InventoryItemInfo;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Listeners;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Manager;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Observers.GridObservers;
using NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.InventoryData.Servises;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.Inventory.Scripts.DI
{
    public class InventorySystemInstaller: MonoInstaller
    {
        [SerializeField] private GameObject _gridItemPrefab;
        [SerializeField] private InventoryPopupProvider _popupProvider;
        
        public override void InstallBindings()
        {
            Container.Bind<IContext>().To<ZenjectContext>().AsSingle().NonLazy();
            Container.Bind<InventorySystemInitializer>().AsSingle().NonLazy();
            
            var equipView = _popupProvider.EquipPopup.GetComponent<EquipPopupView>();
            var statsView = _popupProvider.EquipPopup.GetComponent<StatsView>();
            var infoView = _popupProvider.ItemInfo.GetComponent<InventoryItemInfoView>();
            
            BindStatsViewPresenter(statsView);
            BindInventoryEffectObservers();
            BindInfoAdapter(infoView);
            BindActiveGridService();
            BindGridClickObservers();
            BindEquipManager(equipView);
            BindEquipItemAdapter(equipView);
            
            Container.BindInterfacesAndSelfTo<InventoryRemoveItemListener>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ItemInfoController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InventoryGridInstaller>().AsSingle().WithArguments(_gridItemPrefab).NonLazy();
            Container.BindInstance(_popupProvider).AsSingle();
            
        }

        private void BindInventoryEffectObservers()
        {
            Container.BindInterfacesAndSelfTo<InventoryEffectObserver>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<MedicalKitObserver>().AsSingle().NonLazy();
        }

        private void BindEquipItemAdapter(EquipPopupView equipView)
        {
            EquipItemAdapter equipAdapter = new EquipItemAdapter(equipView);
            Container.BindInstance(equipAdapter).AsSingle().NonLazy();
        }

        private void BindStatsViewPresenter(StatsView statsView)
        {
            Container.BindInterfacesAndSelfTo<StatsViewPresenter>().AsSingle().WithArguments(statsView).NonLazy();
        }

        private void BindEquipManager(EquipPopupView equipView)
        {
            Container.BindInterfacesAndSelfTo<EquippedItemManager>().AsSingle().WithArguments(equipView).NonLazy();
        }

        private void BindGridClickObservers()
        {
            Container.BindInterfacesAndSelfTo<GridObserver_DoubleClick>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GridObserver_LeftClick>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GridObserver_RightClick>().AsSingle().NonLazy();
        }

        private void BindActiveGridService()
        {
            var activeGridService = new ActiveGridService();
            
            Container.BindInstance(activeGridService);
        }

        private void BindInfoAdapter(InventoryItemInfoView infoView)
        {
            Container.Bind<ItemInfoPopupAdapter>().AsSingle().WithArguments(infoView).NonLazy();
        }
    }
}