using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static Vector3 MousePosition => new(Pointer.current.position.ReadValue().x, Pointer.current.position.ReadValue().y, 0f);
    public static Ray MouseRayPoint => Camera.main.ScreenPointToRay(MousePosition);
}
