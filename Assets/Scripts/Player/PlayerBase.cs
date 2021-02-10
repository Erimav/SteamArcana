using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerBase : MonoBehaviour, IPlayer, ISerializationCallbackReceiver
{
    [SerializeField, HideInInspector]
    private CharacterController controller;

    public PlayerStateMachine stateMachine;
    public PlayerStats stats;

    public event UnityAction MoveStopped;
    public event UnityAction MoveStarted;
    public event UnityAction MoveProceed;
    public event UnityAction GotOffTheLand;
    public event UnityAction Landed;

    public IPlayerAnimatorHelper AnimatorHelper => throw new System.NotImplementedException();

    public Vector3 MotionInput { get; set; }
    PlayerStats IPlayer.OriginalStats
    {
        get => stats;
        set => stats = value;
    }

    public PlayerStats FinalStats => stats;//TODO: Add stats modification

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        stateMachine = new PlayerStateMachine();
        stateMachine.Init(this);
    }

    private void Update()
    {

    }

    public void Move(Vector3 motion) => controller.Move(motion);

    void ISerializationCallbackReceiver.OnBeforeSerialize()
    {
    }

    void ISerializationCallbackReceiver.OnAfterDeserialize()
    {
        stateMachine.player = this;
    }
}
