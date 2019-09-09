using UnityEngine;
using System.Collections;

namespace IWantToBeAStar.MainGame.MapObjects.Player
{
    public class FollowPlayer : MonoBehaviour
    {
        public float borderX;
        public float borderY;

        private GameObject player;

        private void Awake()
        {
            player = GameObject.Find("Player");
        }

        private void FixedUpdate()
        {
            if (player != null)
            {
                transform.position = player.transform.position + new Vector3(borderX, borderY);
            }
        }
    }
}