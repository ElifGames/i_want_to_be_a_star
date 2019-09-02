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

        /// <summary>
        /// 스폰 시간 간격
        /// </summary>
        public float SpawnWait;

        /// <summary>
        /// 스폰 시간 감소 폭
        /// </summary>
        public float SpawnGain;

        public int Goals;

        public PlayerSkins PlayerSkin;

        #endregion 유니티 세팅 값

        // private bool paused;
        protected GameObject Player;
        protected Text ScoreText;
        protected Text ResultScore;
        protected Text StatusHeader;
        protected Text StatusBody;

        protected event EventHandler GameEndedEvent;
        protected event EventHandler<StageChangedEventArgs> StageChangedEvent;


        private void Awake()
        {
            Player = GameObject.Find("Player");

            GameData.IsGameRunning = true;
            GameData.SpawnWait = SpawnWait;
            GameData.StartSpawnMeteo = false;
            GameData.StartSpawnUFO = false;
            //Application.targetFrameRate = 60;
        }

        // Use this for initialization
        private void Start()
        {
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

        private void Update()
        {
        }

        protected virtual void OnGameEndedEvent()
        {
            GameEndedEvent?.Invoke(this, null);
        }

        protected virtual void OnStageChangedEvent(StageChangedEventArgs e)
        {
            StageChangedEvent?.Invoke(this, e);
        }


        private void ReduceSpawnWait()
        {
            GameData.SpawnWait -= SpawnGain;
            Debug.Log("스폰 시간 감소");
        }

    }
}