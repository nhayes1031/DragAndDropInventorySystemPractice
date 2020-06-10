using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private IMover _mover;
    private Rotator _rotator;

    private void Awake()
    {
        _mover = new Mover(this);
        _rotator = new Rotator(this);
       
        PlayerInput.Instance.MoveModeTogglePressed += MoveModeTogglePressed;
    }

    private void MoveModeTogglePressed()
    {
        if (_mover is ClickMover)
            _mover = new Mover(this);
        else
            _mover = new ClickMover(this);
    }

    private void Update()
    {
        if (Pause.Active)
            return;

        _mover.Tick();
        _rotator.Tick();
    }
}
