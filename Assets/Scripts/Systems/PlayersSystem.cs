using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersSystem", menuName = "Systems/PlayersSystem")]
public class PlayersSystem : ScriptableObject, IMessageReceiver
{
    public MessagingManager messaging;

    public int localPlayerId;
    public List<PlayerBase> players = new List<PlayerBase>();
    public PlayerBase LocalPlayer => GetPlayer(localPlayerId);

    public PlayerBase GetPlayer(int playerId) => players[playerId];
    public void ReceiveMessage(MessageCode messageType, Stream messageStream)
    {
        switch (messageType)
        {
            case MessageCode.PlayerStartMove:
                var message = new PlayerStartMoveMessage();
                message.Read(messageStream);
                OnPlayerStartedMove(message);
                break;
        }
    }

    public void ReceiveMessage<T>(T message)
    {
        if (!messaging.isMultiplayer)
            return;

        switch (message)
        {
            case PlayerStartMoveMessage msg:
                OnPlayerStartedMove(msg);
                break;
        }
    }

    public void OnPlayerStartedMove(PlayerStartMoveMessage msg)
    {
        var player = GetPlayer(msg.playerId);
        player.StartMove(msg.motion);
    }
}

