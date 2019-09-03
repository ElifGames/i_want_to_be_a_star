using System;
using IWantToBeAStar;
using IWantToBeAStar.MainGame;
using System.Collections;
using UnityEngine;
using IWantToBeAStar.MainGame.MapObjects.Hazards;
using UnityEngine.UI;

public class ScoreManager : GameManager
{

    /// <summary>
    /// 점수 증가 시간 폭
    /// </summary>
    public float ScoreTimeGain;

    public int HighSkyStartScore;
    public int SpaceStartScore;

    private void Awake()
    {
        GameData.Score = 0;
        GameData.HighSkyStartScore = HighSkyStartScore;
        GameData.SpaceStartScore = SpaceStartScore;

        GameEndedEvent += HandleGameEndedEvent;
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
        while (true)
        {
            yield return new WaitForSeconds(ScoreTimeGain);
            AddScore(1);
            var score = GameData.Score;

            // 100점마다 스폰 주기 감소
            // 0.2초보다 낮아지거나 같으면 더이상 감소 안함
            if (GameData.SpawnWait > 0.2f)
            {
                if (score % 100 == 0)
                {
                    ReduceSpawnWait();
                }
            }

            if (score == HighSkyStartScore)
            {
                OnStageChangedEvent(new StageChangedEventArgs(Stage.HighSky));
            }
            else if (score == SpaceStartScore)
            {
                GameData.SpawnSpaceHazard = SpaceHazards.Meteo;
                OnStageChangedEvent(new StageChangedEventArgs(Stage.Space));
                StartCoroutine(ChangeScoreHeaderColorBlackToWhite());
            }

            // SpaceStartScore보다 높은 점수 일때
            // 200점마다 장애물 변경
            if ((score >= SpaceStartScore + 1) && (score % 200 == 0))
            {
                switch (GameData.SpawnSpaceHazard)
                {
                    case SpaceHazards.Meteo:
                        GameData.SpawnSpaceHazard = SpaceHazards.UFO;
                        break;
                    case SpaceHazards.UFO:
                        GameData.SpawnSpaceHazard = SpaceHazards.Meteo;
                        break;
                    default:
                        break;
                }
            }
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

    private void ReduceSpawnWait()
    {
        GameData.SpawnWait -= SpawnGain;
        Debug.Log("스폰 시간 감소");
    }

}
