using MLAPI.Serialization;
using MLAPI.Serialization.Pooled;
using NSubstitute;
using NUnit.Framework;
using System.IO;
using System.Threading;

namespace Tests
{
    public class MessagingTests
    {
        public struct TestStruct : ICommand
        {
            public CommandCode CommandCode => default;

            public void Execute(object sender)
            {
            }

            public void Read(Stream stream)
            {
            }

            public void Write(Stream stream)
            {
            }
        }

        [Test]
        public void ManagerSendsMessageToRegisteredReceiverDirectlyIfOffline()
        {
            var manager = new CommandsManager();
            var message = new TestStruct();
            var receiver = Substitute.For<ICommandReceiver>();
            manager.RegisterMessageReceiver(default, receiver);
            manager.SendMessage(message);

            receiver.Received().ReceiveMessage(message);
        }

        [Test]
        public void ManagerSendsMessageToNetworkedManagerIfOnline()
        {
            var manager = new CommandsManager();
            var message = new TestStruct();
            manager.networkedManager = Substitute.For<ICommandsManager>();
            manager.isMultiplayer = true;
            manager.SendMessage(message);

            manager.networkedManager.Received().SendMessage(message);
        }

        [Test]
        public void NetworkedManagerSendsMessageToMessagingManagerOnReceive()
        {
            var networkedManager = new NetworkedCommandsManager();
            networkedManager.messagingManager = Substitute.For<CommandsManager>();
            using (var stream = PooledBitStream.Get())
            {
                using (var writer = PooledBitWriter.Get(stream))
                    writer.WriteInt32(default);
                stream.Position = 0;
                networkedManager.OnMessage(default, stream);

                networkedManager.messagingManager.Received().OnNetworkMessage(default, stream);
            }
        }
    }
}
