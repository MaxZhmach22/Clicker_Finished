using UnityEngine;


namespace MonsterClicker
{
    internal abstract class EnemyBase : MonoBehaviour, ISelectable
    {
        #region ClassLifeCycles

        protected virtual void Start()
        {
            CurrentHealth = MaxHealth;
        }  

        #endregion


        #region Fields

        [field: SerializeField] protected float MaxHealth { get; private protected set; }
        [field: SerializeField] protected float CurrentHealth { get; private protected set; }
        [field: SerializeField] public float ScorePoints { get; private protected set; }
        public abstract EnemyTypes EnemyType { get; } 

        #endregion


        #region Methods

        protected virtual void DeacreseHealth(float value)
        {
            CurrentHealth -= value;
            if (CurrentHealth <= 0)
                ReturnToPool();
        }

        protected virtual void ReturnToPool()
        {
            CurrentHealth = MaxHealth;
            transform.position = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            gameObject.SetActive(false);
        }

        public void GetSelected(float? value = null) =>
            DeacreseHealth((float)value); 


        #endregion
    }
}
