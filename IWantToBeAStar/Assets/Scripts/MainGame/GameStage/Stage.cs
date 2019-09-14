using System;
using System.Collections;
using UnityEngine;

namespace IWantToBeAStar.MainGame.GameStage
{
    public abstract class Stage : MonoBehaviour
    {
        public readonly StageType stageType;

        protected ScoreManager scoreManager;
        protected HazardManager hazardManager;
        protected GameManager gameManager;

        private IEnumerator StageMainCoroutine;

        public Stage(StageType type)
        {
            stageType = type;
        }

        /// <summary>
        /// 3초 카운트 다운을 한 후 스테이지를 시작합니다.
        /// 해당 함수는 스테이지의 전체 진행과정이며 스테이지가 끝난 후에 종료됩니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerator Run()
        {
            scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
            hazardManager = GameObject.Find("Hazard Manager").GetComponent<HazardManager>();
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

            yield return StartCoroutine(Countdown());
            scoreManager.ScoreAddedEvent += HandleScoreAddedEvent;
            gameManager.GameEndEvent += HandleGameEndEvent;

            StageMainCoroutine = StageMain();
            yield return StartCoroutine(StageMainCoroutine);

            scoreManager.ScoreAddedEvent -= HandleScoreAddedEvent;
            gameManager.GameEndEvent -= HandleGameEndEvent;
        }

        protected abstract IEnumerator StageMain();

        protected virtual void HandleScoreAddedEvent(int score)
        {
        }

        protected virtual void HandleGameEndEvent()
        {
            StopAllCoroutines();
        }

        private IEnumerator Countdown()
        {
            for (int i = 3; i > 0; i--)
            {
                UIManager.GameUI.ReadyText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}