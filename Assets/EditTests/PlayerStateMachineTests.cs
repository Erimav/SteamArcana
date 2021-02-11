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

        [Test]
        public void WalkStateSetsWalkingAnimationOnEnter()
        {
            var player = Substitute.For<IPlayer>();
            var machine = new PlayerStateMachine();
            var animatorHelper = Substitute.For<IPlayerAnimatorHelper>();
            player.AnimatorHelper.Returns(animatorHelper);
            machine.Init(player);

            machine.SwitchToWalking();

            animatorHelper.Received().SetWalking(true);
        }

        [Test]
        public void WalkStateSetsOffWalkingAnimationOnExit()
        {
            var player = Substitute.For<IPlayer>();
            var machine = new PlayerStateMachine();
            var animatorHelper = Substitute.For<IPlayerAnimatorHelper>();
            player.AnimatorHelper.Returns(animatorHelper);
            machine.Init(player);

            machine.SwitchToIdle();
            machine.SwitchToWalking();

            animatorHelper.Received().SetWalking(false);
        }

        [Test]
        public void WalkStateCallsMoveMethod()
        {
            var state = new PlayerWalkState();
            state.Init(Substitute.For<PlayerStateMachine>());
            state.player = Substitute.For<IPlayer>();
            state.player.MotionInput = Vector3.forward;

            state.OnUpdate();

            state.player.Received().Move(Arg.Any<Vector3>());
        }

        [Test]
        public void WalkStateRotatesPlayerTowardsMoveDirection()
        {
            var state = new PlayerWalkState();
            state.Init(Substitute.For<PlayerStateMachine>());
            state.player = Substitute.For<IPlayer>();
            state.player.MotionInput = Vector3.forward;
            state.player.Rotation = Quaternion.Euler(0f, 90f, 0f);

            state.OnUpdate();

            Assert.Less(state.player.Rotation.eulerAngles.y, 90f);
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
