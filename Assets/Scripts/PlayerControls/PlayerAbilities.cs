using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] Ability _ability_1;


    #region Ability Inputs

    public void OnQ(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("Q!");
    }

    public void OnW(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("W!");
    }

    public void OnE(InputAction.CallbackContext ctx)
    {
        if (!ctx.performed) return;
        Debug.Log("E!");
    }

    #endregion
}
