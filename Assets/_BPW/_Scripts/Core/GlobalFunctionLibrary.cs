using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalFunctionLibrary
{

    private static Playermovement[] s_PlayerControllers;

    public static Playermovement GetPlayerController(int playerIndex = 0)
    {
        if (s_PlayerControllers.Length == 0)
        {
            s_PlayerControllers = MonoBehaviour.FindObjectsOfType<Playermovement>();
        }

        return s_PlayerControllers[playerIndex];
    }
}
