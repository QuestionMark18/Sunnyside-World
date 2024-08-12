using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MyMonoBehaviour
{

    [SerializeField] private Transform _playerModel;
    public Transform PlayerModel => _playerModel;

    [SerializeField] private PlayerMovement _playerMovement;
    public PlayerMovement PlayerMovement => _playerMovement;

    [SerializeField] private PlayerAction _playerAction;
    public PlayerAction PlayerAction => _playerAction;

    [SerializeField] private PlayerAnimation _playerAnimation;
    public PlayerAnimation PlayerAnimation => _playerAnimation;

    [SerializeField] private Inventory _playerInventory;
    public Inventory PlayerInventory => _playerInventory;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerModel();
        LoadPlayerMovement();
        LoadPlayerAction();
        LoadPlayerAnimation();
        LoadPlayerInventory();
    }

    private void LoadPlayerModel()
    {
        if (_playerModel != null) return;
        _playerModel = transform.Find("PlayerModel");
        Debug.Log($"{transform.name}: LoadPlayerModel", gameObject);
    }

    private void LoadPlayerMovement()
    {
        if (_playerMovement != null) return;
        _playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        Debug.Log($"{transform.name}: LoadPlayerMovement", gameObject);
    }

    private void LoadPlayerAction()
    {
        if (_playerAction != null) return;
        _playerAction = transform.GetComponentInChildren<PlayerAction>();
        Debug.Log($"{transform.name}: LoadPlayerAction", gameObject);
    }

    private void LoadPlayerAnimation()
    {
        if (_playerAnimation != null) return;
        _playerAnimation = transform.GetComponentInChildren<PlayerAnimation>();
        Debug.Log($"{transform.name}: LoadPlayerAnimation", gameObject);
    }

    private void LoadPlayerInventory()
    {
        if (_playerInventory != null) return;
        _playerInventory = transform.GetComponentInChildren<Inventory>();
        Debug.Log($"{transform.name}: LoadPlayerInventory", gameObject);
    }
}
