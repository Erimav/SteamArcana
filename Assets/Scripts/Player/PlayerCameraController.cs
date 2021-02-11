using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerCameraController : MonoBehaviour
    {
        private PlayerBase player;

        public Transform followTransform;
        public float rotationLerpSpeed = 1f;

        private void Update()
        {
            followTransform.rotation = Quaternion.Lerp(followTransform.localRotation, Quaternion.Euler(player.LookAngles.y, player.LookAngles.x, 0), rotationLerpSpeed * Time.deltaTime);
        }
    }
}
