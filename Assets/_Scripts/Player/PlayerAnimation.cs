using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerAnimation : MyMonoBehaviour
{
    [SerializeField] private Transform _playerModel;

    [Header("Animators")]
    [SerializeField] private Animator _bodyAnimator;
    [SerializeField] private Animator _hairAnimator;
    [SerializeField] private Animator _toolAnimator;

    [Header("Animator Parameters")]
    public bool IsWalking = false;
    public bool IsLeft = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadPlayerModel();
        LoadBodyAnimator();
        LoadHairAnimator();
        LoadToolAnimator();
    }

    private void LoadPlayerModel()
    {
        if (_playerModel != null) return;
        _playerModel = transform.parent.GetComponent<PlayerCtrl>().PlayerModel;
        Debug.Log(transform.name + ": LoadPlayerModel", gameObject);
    }

    private void LoadBodyAnimator()
    {
        if (_bodyAnimator != null) return;
        _bodyAnimator = _playerModel.Find("Body").GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadBodyAnimator", gameObject);
    }

    private void LoadHairAnimator()
    {
        if (_hairAnimator != null) return;
        //_hairAnimator = transform.Find("Hair").GetComponent<Animator>(); // to do:
        Transform hairObj = _playerModel.Find("Hair");
        foreach (Transform child in hairObj)
        {
            if (child.gameObject.activeSelf)
            {
                _hairAnimator = child.GetComponent<Animator>();
                return;
            }
        }
        Debug.Log(transform.name + ": LoadHairAnimator", gameObject);
    }

    private void LoadToolAnimator()
    {
        if (_toolAnimator != null) return;
        _toolAnimator = _playerModel.Find("Tool").GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadToolAnimator", gameObject);
    }

    private void Update()
    {
        UpdateAnimators();
        FlipPlayerModel();
    }

    private void UpdateAnimators()
    {
        UpdateParametersAnimator(_bodyAnimator);
        UpdateParametersAnimator(_hairAnimator);
        UpdateParametersAnimator(_toolAnimator);
    }

    private void UpdateParametersAnimator(Animator animator)
    {
        animator.SetBool("IsWalking", IsWalking);
    }

    private void FlipPlayerModel()
    {
        if (IsLeft) _playerModel.localScale = new Vector3(-1, 1, 1);
        else _playerModel.localScale = new Vector3(1, 1, 1);
    }
}
