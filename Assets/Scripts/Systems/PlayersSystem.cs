using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersSystem", menuName = "Systems/PlayersSystem")]
public class PlayersSystem : ScriptableObject, ICommandReceiver
{
    public CommandsManager messaging;

    public int localPlayerId;
    public List<PlayerBase> players = new List<PlayerBase>();
    public PlayerBase LocalPlayer => GetPlayer(localPlayerId);

    public PlayerBase GetPlayer(int playerId) => players[playerId];
    public void ReceiveMessage(CommandCode commandCode, Stream messageStream)
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

    public void ReceiveMessage<T>(T message) where T : ICommand
    {
        message.Execute(this);
    }
}

