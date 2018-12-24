using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace IWantToBeAStar
{
    public class GameController : MonoBehaviour
    {
        #region 유니티 세팅 값

        public GameObject Hazard;
        public Vector2 SpawnValues;
        public Text scoreHeaderText;
        public Text scoreText;

        /// <summary>
        /// 스폰 시간 간격
        /// </summary>
        public float SpawnWait;

        /// <summary>
        /// 스폰 시간 감소 폭
        /// </summary>
        public float SpawnGain;

        /// <summary>
        /// 맨처음 시작 대기 시간
        /// </summary>
        public float StartWait;

        /// <summary>
        /// 점수 증가 시간 폭
        /// </summary>
        public float ScoreTimeGain;

        public int HighSkyChangeScore;
        public int SpaceChangeScore;

        #endregion 유니티 세팅 값

        /// <summary>
        /// 배경이 바뀜을 알리는 이벤트
        /// </summary>
        public event BackgroundChange OnBackgroundChange;

        public delegate void BackgroundChange(BackgroundStatus status);

        // Use this for initialization
        private void Start()
        {
            GameData.Score = 0;
            scoreText.text = "0";
            GameData.IsGameEnd = false;
            GameData.IsStarted = false;
            GameData.SpawnWait = SpawnWait;
            GameData.StartSpawnMeteo = false;
            GameData.StartSpawnUFO = false;

            StartCoroutine("StartSpawning");
            StartCoroutine("CheckGameEnd");
        }

        private IEnumerator StartSpawning()
        {
            Cursor.visible = false;
            // 다른 루틴보다 늦게 시작해서 오류가 안나게 함
            yield return new WaitForEndOfFrame();

            OnBackgroundChange(BackgroundStatus.LowSky);

            // 맨 처음 시작 대기
            yield return new WaitForSeconds(StartWait);
            GameData.IsStarted = true;
            StartCoroutine("Scoring");
        }

        private IEnumerator Scoring()
        {
            while (true)
            {
                yield return new WaitForSeconds(ScoreTimeGain);
                AddScore(1);

                var score = GameData.Score;

                if (GameData.SpawnWait > 0.2f)
                {
                    if (score % 100 == 0)
                    {
                        ReduceSpawnWait();
                    }
                }

                if (score == HighSkyChangeScore)
                {
                    OnBackgroundChange(BackgroundStatus.HighSky);
                }
                else if (score == SpaceChangeScore)
                {
                    OnBackgroundChange(BackgroundStatus.Space);
                    GameData.StartSpawnMeteo = true;
                    StartCoroutine(ChangeScoreHeaderColor());
                }

                if (score >= SpaceChangeScore + 1 && score % 200 == 0)
                {
                    if (!GameData.StartSpawnMeteo)
                    {
                        GameData.StartSpawnMeteo = true;
                        GameData.StartSpawnUFO = false;
                    }
                    else if (!GameData.StartSpawnUFO)
                    {
                        GameData.StartSpawnMeteo = false;
                        GameData.StartSpawnUFO = true;
                    }
                }
                GameData.CheckSpaceSpawn = true;
            }
        }

        private void ReduceSpawnWait()
        {
            GameData.SpawnWait -= SpawnGain;
            Debug.Log("스폰 시간 감소");
        }

        private void AddScore(int score)
        {
            GameData.Score += score;
            scoreText.text = GameData.Score.ToString();
        }

        private IEnumerator ChangeScoreHeaderColor()
        {
            while (scoreHeaderText.color.r <= 255)
            {
                float beforeColor = scoreHeaderText.color.r + 0.01f;
                scoreHeaderText.color = new Color(beforeColor, beforeColor, beforeColor);
                yield return new WaitForSeconds(0.01f);
            }
        }

        private IEnumerator CheckGameEnd()
        {
            yield return new WaitUntil(() => GameData.IsGameEnd);
            //StopCoroutine("Scoring");
            Cursor.visible = true;
            Debug.Log("게임 끝");
        }
    }
}