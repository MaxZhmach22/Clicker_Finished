using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        public float CurrentHp { get; set; } = 100;

        public float MaxHp => 200;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

    }
}