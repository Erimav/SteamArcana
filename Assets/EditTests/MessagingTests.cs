using MLAPI.Serialization;
using MLAPI.Serialization.Pooled;
using NSubstitute;
using NUnit.Framework;
using System.IO;

namespace Tests
{
    public class MessagingTests
    {
        public struct TestStruct : IBitWritable
        {
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
            manager.RegisterMessageReceiver<TestStruct>(receiver);
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

        //[Test]
        //public void NetworkedManagerSendsMessageToMessagingManagerOnReceive()
        //{
        //    var networkedManager = new NetworkedMessagingManager();
        //    networkedManager.messagingManager = Substitute.For<IMessagingManager>();
        //    using var stream = new MemoryStream();
        //    using var writer = PooledBitWriter.Get(stream);
        //    writer.WriteString()
        //    networkedManager.OnMessage(default, stream);
            
        //    networkedManager.messagingManager.Received().SendMessage()
        //}
    }
}
