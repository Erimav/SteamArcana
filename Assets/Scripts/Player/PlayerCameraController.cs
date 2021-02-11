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
        private void Update()
        {
            followTransform.rotation = Quaternion.Lerp(followTransform.localRotation, Quaternion.Euler(player.LookAngles.y, player.LookAngles.x, 0), player.Settings.cameraLerpSpeed * Time.deltaTime);
        }
    }
}
