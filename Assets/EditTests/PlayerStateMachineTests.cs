using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace Tests
{
    public class PlayerStateMachineTests
    {
        public class Transitions
        {

            // A Test behaves as an ordinary method
            [Test]
            public void StateSwitchesFromIdleToWalkingOnInput()
            {

                var player = Substitute.For<IPlayer>();
                var machine = new PlayerStateMachine();
                machine.Init(player);

                Assert.IsAssignableFrom<PlayerIdleState>(machine.State);
                player.MoveStarted += Raise.Event<UnityAction>();
                Assert.IsAssignableFrom<PlayerWalkState>(machine.State);
            }

            [Test]
            public void StateSwitchesFromWalkingToIdleOnStopMoving()
            {
                var player = Substitute.For<IPlayer>();
                var machine = new PlayerStateMachine();
                machine.Init(player);
                machine.SwitchToWalking();

                Assert.IsAssignableFrom<PlayerWalkState>(machine.State);
                player.MoveStopped += Raise.Event<UnityAction>();
                Assert.IsAssignableFrom<PlayerIdleState>(machine.State);
            }

            [Test]
            public void IdleSwitchesToInAirWhenOffTheGround()
            {
                var player = Substitute.For<IPlayer>();
                var machine = new PlayerStateMachine();
                machine.Init(player);

                player.GotOffTheLand += Raise.Event<UnityAction>();

                Assert.IsAssignableFrom<PlayerInAirDescendingState>(machine.State);
            }
        }



        //// A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        //// `yield return null;` to skip a frame.
        //[UnityTest]
        //public IEnumerator PlayerStateMachineTestsWithEnumeratorPasses()
        //{
        //    // Use the Assert class to test conditions.
        //    // Use yield to skip a frame.
        //    yield return null;
        //}
    }
}
