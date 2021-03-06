﻿using IWantToBeAStar.MainGame.GameStage;
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

        #region Unity Settings
        public int GiftScore;

        public PlayerSkins PlayerSkin;
        private GameObject Player;
        #endregion

        #region Coroutines
        private IEnumerator RunGameCoroutine;
        #endregion

        #region Events
        public delegate void GameEnded();

        public event GameEnded GameEndEvent;

        public delegate void GameStarted();

        public event GameStarted GameStartEvent;

        public delegate void StageChanged(StageTypes changedStage);

        public event StageChanged StageChangedEvent;
        #endregion

        private HazardManager hazardManager;
        private GameObject stageManager;

        private List<Stage> stages;

        private void Awake()
        {
            GameData.GiftScore = GiftScore;
        }

        private void Start()
        {
            Player = GameObject.Find("Player");
            hazardManager = GameObject.Find("Hazard Manager").GetComponent<HazardManager>();
            stageManager = GameObject.Find("Stage Manager");

            switch (GameData.Charactor)
            {
                case Characters.Cat:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Cat;
                    break;

                case Characters.Dog:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Dog;
                    break;

                case Characters.Racoon:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Racoon;
                    break;

                case Characters.Fox:
                    Player.GetComponent<SpriteRenderer>().sprite = PlayerSkin.Fox;
                    break;
            }

            StartCoroutine(WaitAndStart());
        }

        public void PlayerHasDead()
        {
            GameEndEvent?.Invoke();
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
            GameStartEvent?.Invoke();
        }

        private IEnumerator RunTargetStage(Stage stage)
        {
            // 맵 상의 위험요소가 전부 사라질때까지 기다리기
            yield return StartCoroutine(hazardManager.WaitForAllHazardRemoved());

            GameData.BackgroundScrollSpeed = GameData.DefaultBackgroundScrollSpeed;

            GameData.CurrentStage = stage;
            StageChangedEvent?.Invoke(stage.StageType);

            yield return StartCoroutine(GameData.CurrentStage.Run());
            Debug.Log("RunTargetStage종료");
        }

        private IEnumerator RunGame()
        {
            // 처음 시작시 1번과 2번 스프라이트는 각각 Ground, Ground-LowSky임.
            // 따라서 바로 LowSky로 넘어가도 됨.
            stages = new List<Stage>
            {
                stageManager.AddComponent<LowSkyStage>(),
                stageManager.AddComponent<HighSkyStage>(),
                stageManager.AddComponent<InfiniteSpaceStage>()
            };
            // stages 리스트 순서대로 스테이지 실행
            foreach (var stage in stages)
            {
                yield return StartCoroutine(RunTargetStage(stage));
                Debug.Log($"{stage.StageType}스테이지 종료");
            }
        }
    }
}