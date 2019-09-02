using System;
using IWantToBeAStar;
using IWantToBeAStar.MainGame;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreText;

    /// <summary>
    /// 점수 증가 시간 폭
    /// </summary>
    public float ScoreTimeGain;

    /// <summary>
    /// 맨처음 시작 대기 시간
    /// </summary>
    public float StartWait;

    public int HighSkyStartScore;
    public int SpaceStartScore;

    GameController controller;

    private void Awake()
    {
        GameData.Score = 0;
        GameData.HighSkyStartScore = HighSkyStartScore;
        GameData.SpaceStartScore = SpaceStartScore;

        controller = FindObjectOfType<GameController>();
        if (controller != null)
        {
            controller.GameEndedEvent += HandleGameEndedEvent;
        }
        else
        {
            Debug.LogError(GameStrings.GetString("Error_DoesNotFindGameController"));
        }
    }

    private void HandleGameEndedEvent(object sender, EventArgs e)
    {
        StopCoroutine("Scoring");
    }

    void Start()
    {
        ScoreText.text = "0";
        StartCoroutine("Scoring");
    }

    private IEnumerator Scoring()
    {
        // 맨 처음 시작 대기
        yield return new WaitForSeconds(StartWait);

        while (true)
        {
            yield return new WaitForSeconds(ScoreTimeGain);
            AddScore(1);
            /*
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
            */
        }
    }

    private void AddScore(int score)
    {
        GameData.Score += score;
        ScoreText.text = GameData.Score.ToString();
    }

    public IEnumerator ChangeScoreHeaderColorBlackToWhite()
    {
        while (ScoreText.color.r <= 255)
        {
            float beforeColor = ScoreText.color.r + 0.01f;
            ScoreText.color = new Color(beforeColor, beforeColor, beforeColor);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
