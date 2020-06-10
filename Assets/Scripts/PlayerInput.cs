using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
    public float MouseX => Input.GetAxis("Mouse X");
    public float MouseY => Input.GetAxis("Mouse Y");
    public Vector2 MousePosition => Input.mousePosition;

    public bool PausedPressed { get; }

    public event Action<int> HotKeyPressed;
    public event Action MoveModeTogglePressed;

    private void Update()
    {
        if (MoveModeTogglePressed != null && Input.GetKeyDown(KeyCode.Minus))
            MoveModeTogglePressed();

        if (HotKeyPressed == null)
            return;

        for (int i = 0; i < 5; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HotKeyPressed(i);
            }
        }
    }
}
