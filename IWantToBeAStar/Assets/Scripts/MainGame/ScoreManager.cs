using System.Collections;
using TMPro;
using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// 점수 증가 시간 폭
        /// </summary>
        public float ScoreTimeGain;

        /// <summary>
        /// 해당 값의 점수대마다 생성 시간 감소
        /// </summary>
        public int ReduceSpawnGainScore;

        #region Events
        public delegate void ScoreAdded(int score);

        /// <summary>
        /// 점수가 변경될 때마다 이벤트가 발생합니다.
        /// </summary>
        /// <param name="score"></param>
        public event ScoreAdded ScoreAddedEvent;
        #endregion

        private GameManager gameManager;

        private IEnumerator ScoringCoroutine;

        private TextMeshPro playerScore;

        private void Awake()
        {
            GameData.ScoreTimeGain = ScoreTimeGain;
            GameData.ReduceSpawnGainScore = ReduceSpawnGainScore;
        }

        private void Start()
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.GameStartEvent += HandleGameStartEvent;
            gameManager.GameEndEvent += HandleGameEndEvent;

            playerScore = GameObject.Find("PlayerScore").GetComponent<TextMeshPro>();
            GameData.Score = 0;
            playerScore.text = "0";
        }

        private void HandleGameStartEvent()
        {
            ScoringCoroutine = Scoring();
            StartCoroutine(ScoringCoroutine);
        }

        private void HandleGameEndEvent()
        {
            StopCoroutine(ScoringCoroutine);
        }

        private IEnumerator Scoring()
        {
            while (true)
            {
                AddScore(1);
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void AddScore(int score)
        {
            GameData.Score += score;
            var currentScore = GameData.Score;
            ScoreAddedEvent?.Invoke(currentScore);
            playerScore.text = currentScore.ToString();
        }
    }
}