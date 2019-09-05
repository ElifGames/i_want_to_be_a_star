using System;
using System.Collections;
using UnityEngine;

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
            UIManager.GameUI.ReadyText.text = "Hello!";
            yield return new WaitForSeconds(2f);
            UIManager.GameUI.ReadyText.text = "Ready...";
            yield return new WaitForSeconds(1f);
            UIManager.GameUI.ReadyText.color = Color.yellow;
            UIManager.GameUI.ReadyText.text = "Steady...";
            yield return new WaitForSeconds(1f);
            UIManager.GameUI.ReadyText.color = Color.white;
            UIManager.GameUI.ReadyText.text = "GO!";
            yield return new WaitForSeconds(1f);
            UIManager.GameUI.ReadyText.text = string.Empty;
            GameStartEvent();
        }

        public void PlayerHasDead()
        {
            GameEndEvent();
        }
    }
}