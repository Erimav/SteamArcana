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
        public struct TestStruct : IMessageData
        {
            public MessageCode MessageCode => default;

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
            var manager = new MessagingManager();
            var message = new TestStruct();
            var receiver = Substitute.For<IMessageReceiver>();
            manager.RegisterMessageReceiver(default, receiver);
            manager.SendMessage(message);

            receiver.Received().ReceiveMessage(message);
        }

        [Test]
        public void ManagerSendsMessageToNetworkedManagerIfOnline()
        {
            var manager = new MessagingManager();
            var message = new TestStruct();
            manager.networkedManager = Substitute.For<IMessagingManager>();
            manager.isMultiplayer = true;
            manager.SendMessage(message);

            manager.networkedManager.Received().SendMessage(message);
        }

        [Test]
        public void NetworkedManagerSendsMessageToMessagingManagerOnReceive()
        {
            var networkedManager = new NetworkedMessagingManager();
            networkedManager.messagingManager = Substitute.For<MessagingManager>();
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
