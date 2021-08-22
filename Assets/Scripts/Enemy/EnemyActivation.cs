using System;
using UnityEngine;

namespace MonsterClicker
{
    internal sealed class EnemyActivation : IEnemyActivation
    {

        public Action<EnemyBase> OnEnemyAction;


        public void Activation(EnemyBase enemy)
        {
            enemy.gameObject.SetActive(true);
            OnEnemyAction?.Invoke(enemy); 
        }

        
    }
}