using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MyMonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    [Header("Move")]
    [SerializeField] private float _horizontal;
    [SerializeField] private float _vertical;
    [SerializeField] private float _walkSpeed = 2f;
    //[SerializeField] private float _runSpeed = 5f;
    [SerializeField] private Vector2 _moveDirection;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadRigidbody();
    }

    private void LoadRigidbody()
    {
        if (_rb != null) return;
        _rb = transform.parent.GetComponent<Rigidbody2D>();
        Debug.Log(transform.name + ":LoadRigidbody", gameObject);
    }

    private void Update()
    {
        GetMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + Time.fixedDeltaTime * _walkSpeed * _moveDirection);
    }

    private void GetMoveDirection()
    {
        _horizontal = InputManager.Instance.Horizontal;
        _vertical = InputManager.Instance.Vertical;
        _moveDirection = new Vector2(_horizontal, _vertical).normalized;

        GetMovementState();
    }

    private void GetMovementState()
    {
        PlayerCtrl playerCtrl = transform.parent.GetComponent<PlayerCtrl>();

        if (_moveDirection.magnitude > 0) playerCtrl.PlayerAnimation.IsWalking = true;
        else playerCtrl.PlayerAnimation.IsWalking = false;

        if (_horizontal > 0) playerCtrl.PlayerAnimation.IsLeft = false;
        if (_horizontal < 0) playerCtrl.PlayerAnimation.IsLeft = true;
    }
}
