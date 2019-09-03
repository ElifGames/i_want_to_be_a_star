using System;
using IWantToBeAStar.MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IWantToBeAStar;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;

namespace IWantToBeAStar.MainGame
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager GameUI;

        private string userClass;
        private string userName;
        private bool sentInfo = false;
        // private bool openedGameOverPanel = false;

        public Text ScoreText { get; set; }
        public Text ResultScore { get; private set; }
        public Text StatusHeader { get; private set; }
        public Text StatusBody { get; private set; }
        public GameObject GameOverPanel { get; private set; }
        public GameObject WriteInfoPanel { get; private set; }
        public Button OpenSendInfoButton { get; private set; }

        public InputField ClassField;
        public InputField NameField;

        private GameManager gameManager;

        private void Awake()
        {
            if (GameUI == null)
            {
                GameUI = this;
            }
            ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            ResultScore = GameObject.Find("ResultScore").GetComponent<Text>();
            ResultScore = GameObject.FindWithTag("ResultScore").GetComponent<Text>();
            StatusHeader = GameObject.Find("ScoreStatusHeader").GetComponent<Text>();
            StatusBody = GameObject.Find("ScoreStatusBody").GetComponent<Text>();
            GameOverPanel = GameObject.Find("GameOverPanel");
            WriteInfoPanel = GameObject.Find("WriteInfoPanel");
            OpenSendInfoButton = GameObject.Find("Button_OpenSendInfo").GetComponent<Button>();
            ClassField = GameObject.Find("InputField_Class").GetComponent<InputField>();
            NameField = GameObject.Find("InputField_Name").GetComponent<InputField>();

            GameOverPanel.SetActive(false);
            WriteInfoPanel.SetActive(false);

            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

            gameManager.GameEndEvent += HandleGameEndedEvent;
        }

        private void Start()
        {
        }

        private void HandleGameEndedEvent()
        {
            // openedGameOverPanel = true;
            Cursor.visible = true;
            Debug.Log("-----[Game Over]-----");

            // 점수가 상품을 줄 수 있는 최저 점수를 넘겼는지 확인
            if (GameData.Score < GameData.Goal)
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

            //GameOverPanel.SetActive(true);
            OpenGameOverPanel();
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
