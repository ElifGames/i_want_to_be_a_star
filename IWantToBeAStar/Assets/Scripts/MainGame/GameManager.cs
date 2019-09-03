using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IWantToBeAStar.MainGame
{
    public class GameManager : MonoBehaviour
    {
        [Serializable]
        public class PlayerSkins
        {
            public Sprite Dog;
            public Sprite Cat;
            public Sprite Racoon;
            public Sprite Fox;
        }

        #region 유니티 세팅 값


        public int Goal;
        public int StartWait;

        public PlayerSkins PlayerSkin;
        private GameObject Player;


        #endregion 유니티 세팅 값

        // private bool paused;
        public delegate void GameEnd();
        public event GameEnd GameEndEvent;

        public delegate void GameStart();
        public event GameStart GameStartEvent;

        private void Awake()
        {
            Player = GameObject.Find("Player");

            GameData.Goal = Goal;

            switch (GameData.Charactor)
            {
                case Charactors.Cat:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Cat;
                    break;
                case Charactors.Dog:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Dog;
                    break;
                case Charactors.Racoon:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Racoon;
                    break;
                case Charactors.Fox:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Fox;
                    break;
            }

            Cursor.visible = false;
        }

        private void Start()
        {
            StartCoroutine(WaitAndStart());
        }

        private IEnumerator WaitAndStart()
        {
            yield return new WaitForSeconds(StartWait);
            GameStartEvent();
        }

        public void PlayerHasDead()
        {
            GameEndEvent();
        }
    }
}