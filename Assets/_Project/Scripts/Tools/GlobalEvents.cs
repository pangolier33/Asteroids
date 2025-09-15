using UnityEngine;
using System;

public static class GlobalEvents
{
    public static event Action OnPlayerDestroyed;
    public static void PlayerDestroyed()
    {
        OnPlayerDestroyed?.Invoke();
    }
}
