using System;
using UnityEngine;

public class Events : MonoBehaviour
{
    public static event Action Sprint;

    public static void OnSprint()
    {
        Sprint?.Invoke();
    }
}
