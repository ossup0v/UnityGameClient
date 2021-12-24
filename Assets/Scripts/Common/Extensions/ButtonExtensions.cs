using System;
using UnityEngine.UI;

public static class ButtonExtension
{
    public static void AddListener<T>(this Button btn, T param, Action<T> callback)
    {
        btn.onClick.AddListener(delegate () { callback(param); });
    }
}
