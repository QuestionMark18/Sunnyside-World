using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MyMonoBehaviour
{
    private static InputManager _instance;
    public static InputManager Instance => _instance;

    private float _horizontal;
    public float Horizontal => _horizontal;

    private float _vertical;
    public float Vertical => _vertical;

    private bool _leftMouse;
    public bool LeftMouse => _leftMouse;

    private bool _rightMouse;
    public bool RightMouse => _rightMouse;

    private bool _key_Space;
    public bool KeySpace => _key_Space;

    private bool _key_Q;
    public bool Key_Q => _key_Q;

    private bool _key_E;
    public bool Key_E => _key_E;

    private bool _key_F;
    public bool Key_F => _key_F;

    private bool _key_R;
    public bool Key_R => _key_R;


    protected override void Awake()
    {
        if (_instance != null) return;
        _instance = this;

        base.Awake();
    }

    private void Update()
    {
        GetHorizontalAndVertical();
        GetKey();
        GetKeyDown();
        GetKeyUp();
    }

    private void GetHorizontalAndVertical()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void GetKey()
    {
        
    }

    private void GetKeyDown()
    {
        _leftMouse = Input.GetMouseButtonDown(0);
        _rightMouse = Input.GetMouseButtonDown(1);
        _key_Space = Input.GetKeyUp(KeyCode.Space);
        _key_Q = Input.GetKeyDown(KeyCode.Q);
        _key_E = Input.GetKeyDown(KeyCode.E);
        _key_F = Input.GetKeyDown(KeyCode.F);
        _key_R = Input.GetKeyDown(KeyCode.R);
    }

    private void GetKeyUp()
    {

    }
}
