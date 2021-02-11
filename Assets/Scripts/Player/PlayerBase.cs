using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour, IPlayer, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private CharacterController controller;
    private bool isGrounded = true;
    private Transform _transform;

    public PlayerStateMachine stateMachine;
    public PlayerStats stats;

    private Vector2 lookAngles;
    public Vector2 MotionInput { get; set; }
    public Vector2 LookAngles
    {
        get => lookAngles;
        set
        {
            value.x = Mathf.Clamp(value.x, -90f, 90f);
            value.y %= 360f;
            lookAngles = value;
        }
    }

    //public ref PlayerStats GetOriginalStatsRef() => ref stats;

    PlayerStats IPlayer.OriginalStats
    {
        get => stats;
        set => stats = value;
    }
    public IPlayerAnimatorHelper AnimatorHelper { get; private set; }

    public event UnityAction MoveStopped;
    public event UnityAction MoveStarted;
    public event UnityAction MoveProceed;
    public event UnityAction GotOffTheLand;
    public event UnityAction Landed;
    public event UnityAction JumpRequested;
    public event UnityAction Jumped;

    public PlayerStats FinalStats => stats;//TODO: Add stats modification

    public Vector3 Position { get => _transform.position; set => _transform.position = value; }
    public Quaternion Rotation { get => _transform.rotation; set => _transform.rotation = value; }

    [SerializeField]
    private PlayerSettings settings;
    public PlayerSettings Settings { get => settings; set => settings = value; }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        AnimatorHelper = GetComponent<IPlayerAnimatorHelper>();
        stateMachine = new PlayerStateMachine();
        stateMachine.Init(this);
    }

    private void Update()
    {

        if (isGrounded && !controller.isGrounded)
        {
            isGrounded = false;
            GotOffTheLand?.Invoke();
        }
        else if (!isGrounded && controller.isGrounded)
        {
            isGrounded = true;
            Landed?.Invoke();
        }

        //(controller.collisionFlags & CollisionFlags.Above) != 0
    }

    public void Move(Vector3 motion) => controller.Move(_transform.TransformDirection(motion));

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        stateMachine.player = this;
    }

    public void StartMove(Vector2 motion)
    {
        MotionInput = motion;
        MoveStarted?.Invoke();
    }
    public void StopMove()
    {
        MotionInput = Vector2.zero;
        MoveStopped?.Invoke();
    }

    public void ProceedMove(Vector2 motion)
    {
        MotionInput = motion;
        MoveProceed?.Invoke();
    }

    public void RequestJump() => JumpRequested?.Invoke();
    public void Jump()
    {
        Jumped?.Invoke();
    }
}
