using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action<int> HotKeyPressed;
    event Action MoveModeTogglePressed;
    float Vertical { get; }
    float Horizontal { get; }
    float MouseX { get; }
    bool PausedPressed { get; }
    Vector2 MousePosition { get; }
}