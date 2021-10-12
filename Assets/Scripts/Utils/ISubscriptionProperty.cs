using System;

namespace Clicker
{
    internal interface ISubscriptionProperty<out TValue>
    {
        TValue Value { get; }
        public void SubscribeOnChange(Action<TValue> subscriptionAction);

        public void UnSubscribeOnChange(Action<TValue> unsubscriptionAction);

    }
}