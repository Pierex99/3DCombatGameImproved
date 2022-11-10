using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 6f;
    public float dampTime = 0.1f;
    public float rotationDampTime = 0.1f;
    public GameObject hitBox;

    public Camera MainCamera;

    private Rigidbody mRb;
    private Animator mAnimator;
    private FiniteStateMachine<PlayerController> mFsm;
    public bool OnAttackingAnimationEnd { get; set; } = false;

    // States
    public State<PlayerController> PlayerIdleState;
    public State<PlayerController> PlayerRunningState;
    public State<PlayerController> PlayerAttackingState;

    private void Awake()
    {
        mRb = GetComponent<Rigidbody>();
        mAnimator = GetComponent<Animator>();

        mFsm = new FiniteStateMachine<PlayerController>();
        PlayerIdleState = new PlayerIdleState(this, mFsm);
        PlayerRunningState = new PlayerRunningState(this, mFsm);
        PlayerAttackingState = new PlayerAttackingState(this, mFsm);
    }

    private void Start()
    {
        //MainCamera = Camera.main.transform;
        mFsm.Start(PlayerIdleState);
    }

    private void FixedUpdate()
    {
        mFsm.CurrentState.OnPhysicsUpdate();
    }

    private void Update()
    {
        mFsm.CurrentState.HandleInput();
        mFsm.CurrentState.OnLogicUpdate();
    }

    public void ActivateHitbox()
    {
        hitBox.SetActive(true);
    }   

    public void OnAttackAnimationEnd()
    {
        OnAttackingAnimationEnd = true;
        hitBox.SetActive(false);
    }
}
