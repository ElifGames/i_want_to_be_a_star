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

        public int Goals;

        #endregion 유니티 세팅 값

        // private bool paused;

        private string userClass;
        private string userName;
        private bool sentInfo = false;
        private bool openedGameOverPanel = false;

        public event EventHandler GameEndedEvent;

        private void Awake()
        {
            GameData.IsGameRunning = true;
            GameData.SpawnWait = SpawnWait;
            GameData.StartSpawnMeteo = false;
            GameData.StartSpawnUFO = false;
            GameOverPanel.SetActive(false);
            WriteInfoPanel.SetActive(false);
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
            if (!GameData.IsGameRunning && !openedGameOverPanel)
            {
                OnGameEndedEvent();
                openedGameOverPanel = true;
                StopCoroutine("Scoring");
                Cursor.visible = true;
                Debug.Log("게임 끝");

                // 점수가 상품을 줈 있는 최저 점수를 넘겼는지 확인
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
        }

        protected virtual void OnGameEndedEvent()
        {
            GameEndedEvent?.Invoke(this, null);
        }


        private void ReduceSpawnWait()
        {
            GameData.SpawnWait -= SpawnGain;
            Debug.Log("스폰 시간 감소");
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
            Debug.Log("입력: " + userClass);
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