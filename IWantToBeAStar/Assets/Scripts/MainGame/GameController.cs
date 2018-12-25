using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IWantToBeAStar.MainGame
{
    public class GameController : MonoBehaviour
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

        public Text ScoreText;
        public Text ResultScore;
        public Text StatusHeader;
        public Text StatusBody;

        public PlayerSkins PlayerSkin;
        public GameObject Player;

        public GameObject GameOverPanel;
        public GameObject WriteInfoPanel;
        public Button OpenSendInfoButton;

        public InputField ClassField;
        public InputField NameField;

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
        public int Goals;

        #endregion 유니티 세팅 값

        // private bool paused;

        private string userClass;
        private string userName;
        private bool sentInfo = false;

        /// <summary>
        /// 배경이 바뀜을 알리는 이벤트
        /// </summary>
        public event BackgroundChange OnBackgroundChange;

        public delegate void BackgroundChange(BackgroundStatus status);

        private void Awake()
        {
            GameData.Score = 0;
            GameData.IsGameEnd = false;
            GameData.IsStarted = false;
            GameData.SpawnWait = SpawnWait;
            GameData.StartSpawnMeteo = false;
            GameData.StartSpawnUFO = false;
            GameOverPanel.SetActive(false);
        }

        // Use this for initialization
        private void Start()
        {
            ScoreText.text = "0";

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
            ScoreText.text = GameData.Score.ToString();
        }

        private IEnumerator ChangeScoreHeaderColor()
        {
            while (ScoreText.color.r <= 255)
            {
                float beforeColor = ScoreText.color.r + 0.01f;
                ScoreText.color = new Color(beforeColor, beforeColor, beforeColor);
                yield return new WaitForSeconds(0.05f);
            }
        }

        private IEnumerator CheckGameEnd()
        {
            yield return new WaitUntil(() => GameData.IsGameEnd);
            StopCoroutine("Scoring");
            Cursor.visible = true;
            Debug.Log("게임 끝");


            if (GameData.Score < Goals)
            {
                StatusHeader.text
                    = GameStrings.GetString("ScoreStatusHeader_NotAccomplish");
                StatusBody.text
                    = GameStrings.GetString("ScoreStatusBody_NotAccomplish");
            }
            else
            {
                StatusHeader.text
                    = GameStrings.GetString("ScoreStatusHeader_Accomplish");
                StatusBody.text
                    = GameStrings.GetString("ScoreStatusBody_Accomplish");
            }
            ResultScore.text = GameData.Score.ToString();

            GameOverPanel.SetActive(true);

        }

        public void Restart()
        {
            SceneManager.LoadScene("MainGame");
        }

        public void GoMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public void EndWritingClass()
        {
            userClass = ClassField.text;
            Debug.Log("입력: "+ userClass);
        }

        public void EndWritingName()
        {
            userName = NameField.text;
            Debug.Log("입력: " + userName);
        }

        public void SendButtonClick()
        {
            Debug.Log("보내기버튼 눌림");

            StartCoroutine("SendUserScore");
            sentInfo = true;
            OpenGameOverPanel();
        }

        public void SendCancelButtonClick()
        {
            Debug.Log("취소버튼 눌림");

            sentInfo = false;
            OpenGameOverPanel();
        }

        public void OpenWriteInfoPanel()
        {
            Debug.Log("정보작성패널 열림");

            GameOverPanel.SetActive(false);
            WriteInfoPanel.SetActive(true);
        }

        public void OpenGameOverPanel()
        {
            Debug.Log("게임오버패널 열림");

            if (sentInfo)
            {
                OpenSendInfoButton.interactable = false;
            }
            WriteInfoPanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }

        private IEnumerator SendUserScore()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Debug.LogError("인터넷 연결이 되어있지 않습니다!");
                yield break;
            }

            WWWForm form = new WWWForm();

            form.AddField("class", userClass, Encoding.UTF8);
            form.AddField("name", userName, Encoding.UTF8);
            form.AddField("score", GameData.Score);

            UnityWebRequest webRequest = UnityWebRequest.Post("pid011.dothome.co.kr/AddValue.php", form);
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                Debug.Log("업로드 완료");
            }
        }
    }
}