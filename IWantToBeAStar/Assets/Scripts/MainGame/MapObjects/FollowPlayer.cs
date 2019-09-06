using UnityEngine;
using System.Collections;

namespace IWantToBeAStar.MainGame.MapObjects
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
        // Update is called once per frame
        void Update()
        {
            transform.position = player.transform.position + new Vector3(borderX, borderY);
        }
    }
}