using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private GameObject _explosionPrefab;
        public float CurrentHp { get; set; } = 100;

        public float MaxHp => 200;
        bool IsDead = false;
        
        void Update()
        {
            if (CurrentHp <= 0 && !IsDead)
            {
                Instantiate(_explosionPrefab, gameObject.transform.position, Quaternion.identity);
                IsDead = true;
                gameObject.SetActive(false);
            }

                
        }

    }
}