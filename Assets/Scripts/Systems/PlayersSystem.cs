using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersSystem", menuName = "Systems/PlayersSystem")]
[RecievesCommand(CommandCode.PlayerStartMove)]
[RecievesCommand(CommandCode.PlayerProceedMove)]
[RecievesCommand(CommandCode.PlayerStoppedMove)]
[RecievesCommand(CommandCode.PlayerJump)]
[RecievesCommand(CommandCode.PlayerAttack)]
[RecievesCommand(CommandCode.PlayerSecondaryAttack)]
[RecievesCommand(CommandCode.PlayerInteract)]
public class PlayersSystem : SystemBase
{
    public int localPlayerId;
    public List<PlayerBase> players = new List<PlayerBase>();
    public PlayerBase LocalPlayer => GetPlayer(localPlayerId);

    public PlayerBase GetPlayer(int playerId) => players[playerId];
    public override void ReceiveMessage(CommandCode commandCode, Stream messageStream)
    {
        switch (commandCode)
        {
            case CommandCode.PlayerStartMove:
                var command = new PlayerStartMoveCommand();
                command.Read(messageStream);
                command.Execute(this);
                break;
        }
    }

    public void StartMoveLocalPlayer(Vector2 motion) => commands.SendCommand(new PlayerStartMoveCommand
    {
        playerId = localPlayerId,
        motion = motion
    });
    public void ProceedMoveLocalPlayer(Vector2 motion) => commands.SendCommand(new PlayerProceedMoveCommand
    {
        playerId = localPlayerId,
        motion = motion
    });
    public void StopMoveLocalPlayer() => commands.SendCommand(new PlayerStoppedMoveCommand { playerId = localPlayerId });
    public void JumpLocalPlayer() => commands.SendCommand(new PlayerJumpCommand { playerId = localPlayerId });

    public void SetLocalPlayer(LocalPlayer localPlayer)
    {
        localPlayerId = localPlayer.PlayerId;
        players[localPlayerId] = localPlayer;
    }
}

