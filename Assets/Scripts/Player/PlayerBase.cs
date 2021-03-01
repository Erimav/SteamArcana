using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerBase : MonoBehaviour, IPlayer, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private CharacterController controller;
    private bool isGrounded = true;
    private Transform _transform;

    public PlayerStateMachine stateMachine;
    public PlayerStats stats;

    public Vector2 MotionInput { get; set; }
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }

    private Vector2 lookAngles;
    public Vector2 LookAngles
    {
        get => lookAngles;
        set
        {
            value.y = Mathf.Clamp(value.y, -90f, 90f);
            value.x %= 360f;
            lookAngles = value;
        }
    }

    //public ref PlayerStats GetOriginalStatsRef() => ref stats;

    PlayerStats IPlayer.OriginalStats
    {
        get => stats;
        set => stats = value;
    }

    public event UnityAction MoveStopped;
    public event UnityAction MoveStarted;
    public event UnityAction MoveProceed;
    public event UnityAction GotOffTheLand;
    public event UnityAction Landed;
    public event UnityAction JumpRequested;
    public event UnityAction Jumped;

    public PlayerStats FinalStats => stats;//TODO: Add stats modification

    public Transform Transform => _transform;
    public Vector3 Position { get => _transform.position; set => _transform.position = value; }
    public Quaternion Rotation { get => _transform.rotation; set => _transform.rotation = value; }

    [SerializeField]
    private PlayerSettings settings;
    private Quaternion targetRotation;

    public PlayerSettings Settings { get => settings; set => settings = value; }
    public Vector3 Velocity { get; set; }

    public int PlayerId { get; set; }

    protected virtual void Awake()
    {
        _transform = transform;
        controller = GetComponent<CharacterController>();
        stateMachine = new PlayerStateMachine();
        stateMachine.Init(this);
    }

    protected virtual void Update()
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

        stateMachine.OnUpdate();

        Move(Velocity * Time.deltaTime);
        _transform.rotation = Quaternion.Lerp(_transform.rotation, targetRotation, settings.rotationLerpSpeed * Time.deltaTime);
        //(controller.collisionFlags & CollisionFlags.Above) != 0
    }

    public void Move(Vector3 motion) => controller.Move(motion);
    public void SetTargetRotationToCameraRotation() => TargetRotation = Quaternion.Euler(0f, lookAngles.x, 0f);
    public void SetTargetRotationTowardsVelocity() => TargetRotation = Quaternion.LookRotation(Velocity.WithY(0f));

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
