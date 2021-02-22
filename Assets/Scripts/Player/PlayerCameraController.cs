using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(PlayerBase))]
    public class PlayerCameraController : MonoBehaviour
    {
        private PlayerBase player;
        public Transform followTransform;
        private float currentAngle;

        private void Awake()
        {
            player = GetComponent<PlayerBase>();
        }

        private void Update()
        {
            currentAngle = Mathf.Lerp(currentAngle, player.LookAngles.y, player.Settings.rotationLerpSpeed * Time.deltaTime);
            followTransform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
            player.TargetRotation = Quaternion.Euler(0, player.LookAngles.x, 0);
        }
    }
}
