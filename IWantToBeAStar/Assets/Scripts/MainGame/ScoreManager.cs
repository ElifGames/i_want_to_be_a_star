using IWantToBeAStar.MainGame.MapObjects.Hazards;
using System.Collections;
using UnityEngine;
using TMPro;

namespace IWantToBeAStar.MainGame
{
    public class ScoreManager : MonoBehaviour
    {
        /// <summary>
        /// 점수 증가 시간 폭
        /// </summary>
        public float ScoreTimeGain;

        /// <summary>
        /// 해당 값의 점수대마다 스폰 시간 감소
        /// </summary>
        public int ReduceSpawnGainScore;

        private TextMeshPro playerScore;

        public delegate void ScoreAdded(int score);
        public event ScoreAdded ScoreAddedEvent;

        private void Awake()
        {
            GameData.ScoreTimeGain = ScoreTimeGain;
            GameData.ReduceSpawnGainScore = ReduceSpawnGainScore;
        }

        private void Start()
        {
            playerScore = GameObject.Find("PlayerScore").GetComponent<TextMeshPro>();
            GameData.Score = 0;
            playerScore.text = "0";
        }

        public void AddScore(int score)
        {
            GameData.Score += score;
            var currentScore = GameData.Score;
            ScoreAddedEvent(currentScore);
            playerScore.text = currentScore.ToString();
        }
    }
}