using IWantToBeAStar.MainGame.GameStage;
using IWantToBeAStar.MainGame.MapObjects.Hazards;
using System;
using System.Collections;
using System.Collections.Generic;
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

        public int GiftScore;
        public int StartWait;

        public PlayerSkins PlayerSkin;
        private GameObject Player;

        #endregion 유니티 세팅 값

        #region Coroutine
        private IEnumerator RunGameCoroutine;
        private IEnumerator ScoringCoroutine;
        #endregion

        public delegate void GameEnded();
        public event GameEnded GameEndEvent;

        public delegate void GameStarted();
        public event GameStarted GameStartEvent;

        public delegate void StageChanged(StageType changedStage);
        public event StageChanged StageChangedEvent;

        private ScoreManager scoreManager;
        private HazardManager hazardManager;

        private List<Stage> stages;


        public void PlayerHasDead()
        {
            StopCoroutine(ScoringCoroutine);
            GameEndEvent();
        }

        private void Awake()
        {
            GameData.GiftScore = GiftScore;

        }

        private void Start()
        {
            Player = GameObject.Find("Player");
            scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
            hazardManager = GameObject.Find("Hazard Manager").GetComponent<HazardManager>();

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
            StartGame();
        }

        private void StartGame()
        {
            RunGameCoroutine = RunGame();
            StartCoroutine(RunGameCoroutine);
            GameStartEvent();
        }

        private IEnumerator RunTargetStage(Stage stage)
        {
            // 맵 상의 위험요소가 전부 사라질때까지 기다리기
            yield return new WaitUntil(() => hazardManager.SpawnedHazardsCount == 0);

            GameData.BackgroundScrollSpeed = GameData.DefaultBackgroundScrollSpeed;

            switch (stage.stageType)
            {
                case StageType.LowSky:
                    GameData.CurrentStage = stage;
                    StageChangedEvent(StageType.LowSky);
                    yield return StartCoroutine(GameData.CurrentStage.Run());
                    break;
                case StageType.HighSky:
                    break;
                case StageType.Space:
                    break;
            }
        }

        private IEnumerator RunGame()
        {
            // 처음 시작시 1번과 2번 스프라이트는 각각 Ground, Ground-LowSky임.
            // 따라서 바로 LowSky로 넘어가도 됨.
            stages = new List<Stage>
            {
                new LowSkyStage()
            };

            ScoringCoroutine = Scoring();
            StartCoroutine(ScoringCoroutine);

            // stages 리스트 순서대로 스테이지 실행
            foreach (var stage in stages)
            {
                yield return StartCoroutine(RunTargetStage(stage));
            }
        }

        private IEnumerator Scoring()
        {
            scoreManager.AddScore(1);
            yield return new WaitForSeconds(0.1f);
        }
    }
}