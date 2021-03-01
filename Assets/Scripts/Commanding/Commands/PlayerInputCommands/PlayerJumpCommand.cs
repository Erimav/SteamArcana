using MLAPI.Serialization.Pooled;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public struct PlayerJumpCommand : ICommand
{
    public CommandCode CommandCode => CommandCode.PlayerJump;

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
        var system = sender as PlayersSystem;
        var player = system.GetPlayer(playerId);
        player.RequestJump();
    }
}

