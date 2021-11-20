using UniRx;
using UnityEngine;
using TMPro;
using System;

internal static class UniRxUiExtension
{
    public static IDisposable SubscribeToText<T>(this IObservable<T> source, TMP_Text text)
    {
        return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
    }
}
