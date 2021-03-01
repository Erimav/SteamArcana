using MLAPI.Serialization.Pooled;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public struct PlayerStoppedMoveCoomand : ICommand
{
    public CommandCode MessageCode => CommandCode.PlayerStoppedMove;

    public int playerId;

    public void Read(Stream stream)
    {
        using (var reader = PooledBitReader.Get(stream))
            playerId = reader.ReadInt32Packed();
    }

    public void Write(Stream stream)
    {
        using (var writer = PooledBitWriter.Get(stream))
            writer.WriteInt32Packed(playerId);
    }

    public void Execute(object sender)
    {
        var player = (sender as PlayersSystem).GetPlayer(playerId);
        player.StopMove();
    }
}

