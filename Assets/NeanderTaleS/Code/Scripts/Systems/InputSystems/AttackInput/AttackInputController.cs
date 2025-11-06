using System;
using NeanderTaleS.Code.Scripts.Interfaces.Components;
using NeanderTaleS.Code.Scripts.Systems.InputSystems.Interfaces;
using UnityEngine;
using Zenject;

namespace NeanderTaleS.Code.Scripts.Systems.InputSystems.AttackInput
{
    public class AttackInputController: IInitializable, IDisposable, IPlayerInput
    {
        private readonly AttackInputListener _listener;
        private IAttackComponent _attackComponent;

        public AttackInputController(AttackInputListener listener)
        {
            _listener = listener;
        }

        public void Initialize()
        {
            _listener.OnAttackInput += Attack;
        }

        private void Attack()
        {
            _attackComponent.Attack();
        }

        public void Init(GameObject player)
        {
            _attackComponent = player.GetComponent<IAttackComponent>();
            
            if (_attackComponent == null)
            {
                Debug.Log($"{player.GetType()} : {player.name} <color=red> IAttackComponent NOT FOUND </color>");
            }
        }
        
        public void Dispose()
        {
            _listener.OnAttackInput -= Attack;
        }
    }
}