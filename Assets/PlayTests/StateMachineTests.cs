using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class StateMachineTests
    {

        private static PlayerBase CreatePlayer()
        {
            var go = new GameObject();
            var player = go.AddComponent<PlayerBase>();
            player.Settings = new PlayerSettings();
            player.stats.speed = 1f;
            return player;
        }

       
        [UnityTest]
        public IEnumerator WalkStateMovesPlayer()
        {
            PlayerBase player = CreatePlayer();

            player.MotionInput = Vector3.up;
            player.stateMachine.SwitchToWalking();
            yield return null;

            Assert.AreNotEqual(Vector3.zero, player.Transform.position);
        }

        [UnityTest]
        public IEnumerator WalkStateRotatesPlayerTowardsVelocity()
        {
            var player = CreatePlayer();

            player.MotionInput = Vector3.right;
            player.stateMachine.SwitchToWalking();
            yield return null;

            Assert.Greater(player.Rotation.eulerAngles.y, 0f);
        }
    }
}
