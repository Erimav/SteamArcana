using MLAPI.Serialization.Pooled;
using System.IO;


public struct PlayerStoppedMoveCommand : ICommand
{
    public CommandCode CommandCode => CommandCode.PlayerStoppedMove;

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

