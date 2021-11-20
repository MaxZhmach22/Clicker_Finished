using UnityEngine;


namespace MonsterClicker
{
    internal abstract class BasePresenter : MonoBehaviour
    {
        public virtual void Start() =>
            gameObject.SetActive(true);

        public virtual void Dispose() =>
            gameObject.SetActive(false);
    }
}
