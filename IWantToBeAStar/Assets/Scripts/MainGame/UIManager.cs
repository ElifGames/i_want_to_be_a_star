using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace IWantToBeAStar.MainGame
{
    public class UIManager : MonoBehaviour
    {
        #region 패널 클래스

        public class BasePanel
        {
            public GameObject Panel { get; }

            public BasePanel(GameObject prefab, Transform transform)
            {
                Panel = Instantiate(prefab, transform);
            }

            public void Open()
            {
                Panel.SetActive(true);
            }

            public void Close()
            {
                Panel.SetActive(false);
            }
        }

        public class GameOverPanel : BasePanel
        {
            public Text ResultScore { get; }
            public Text StatusHeader { get; }
            public Text StatusBody { get; }
            public Button OpenWriteInfoButton { get; }

            public GameOverPanel(GameObject prefab, Transform transform) : base(prefab, transform)
            {
                StatusHeader = GameObject.Find("ScoreStatusHeader").GetComponent<Text>();
                StatusBody = GameObject.Find("ScoreStatusBody").GetComponent<Text>();
                ResultScore = GameObject.Find("ResultScore").GetComponent<Text>();
                OpenWriteInfoButton = GameObject.Find("Button_OpenWriteInfo").GetComponent<Button>();
            }
        }

        public class WriteInfoPanel : BasePanel
        {
            public InputField ClassField { get; }
            public InputField NameField { get; }

            public WriteInfoPanel(GameObject prefab, Transform transform) : base(prefab, transform)
            {
                ClassField = GameObject.Find("InputField_Class").GetComponent<InputField>();
                NameField = GameObject.Find("InputField_Name").GetComponent<InputField>();
            }
        }

        public class PausePanel : BasePanel
        {
            public PausePanel(GameObject prefab, Transform transform) : base(prefab, transform)
            {
            }
        }

        #endregion 패널 클래스

        public static UIManager GameUI;

        public GameObject GameOverPanelPrefab;
        public GameObject WriteInfoPanelPrefab;
        public GameObject PausePanelPrefab;

        private string userClass;
        private string userName;
        private bool sentInfo = false;

        public Text ReadyText { get; private set; }

        private GameOverPanel gameOverPanel;
        private WriteInfoPanel writeInfoPanel;
        private PausePanel pausePanel;

        private GameManager gameManager;

        private bool isPausePanelOpen = false;

        private bool playerDead = false;

        public void SetDefaultToReadyText()
        {
            ReadyText.text = string.Empty;
            ReadyText.color = Color.white;
        }

        private void Awake()
        {
            if (GameUI == null)
            {
                GameUI = this;
            }
        }

        private void Start()
        {
            ReadyText = GameObject.Find("ReadyText").GetComponent<Text>();

            ReadyText.text = string.Empty;

            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

            gameManager.GameEndEvent += HandleGameEndedEvent;
        }

        private void Update()
        {
            if (!playerDead && Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPausePanelOpen)
                {
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    pausePanel = new PausePanel(PausePanelPrefab, transform);
                    isPausePanelOpen = true;
                }
                else
                {
                    Time.timeScale = 1;
                    Resume();
                }
            }
        }

        private void HandleGameEndedEvent()
        {
            Cursor.visible = true;
            playerDead = true;
            Debug.Log("-----[Game Over]-----");

            if (gameOverPanel == null)
            {
                gameOverPanel = new GameOverPanel(GameOverPanelPrefab, transform);
            }
            if (writeInfoPanel == null)
            {
                writeInfoPanel = new WriteInfoPanel(WriteInfoPanelPrefab, transform);
            }

            OpenGameOverPanel();

            // 점수가 상품을 줄 수 있는 최저 점수를 넘겼는지 확인
            if (GameData.Score < GameData.GiftScore)
            {
                gameOverPanel.StatusHeader.text
                    = GameStrings.GetString("ScoreStatusHeader_NotAccomplish");
                gameOverPanel.StatusBody.text
                    = GameStrings.GetString("ScoreStatusBody_NotAccomplish");
            }
            else
            {
                gameOverPanel.StatusBody.text
                    = GameStrings.GetString("ScoreStatusHeader_Accomplish");
                gameOverPanel.StatusBody.text
                    = GameStrings.GetString("ScoreStatusBody_Accomplish");
            }
            gameOverPanel.ResultScore.text = GameData.Score.ToString();
        }

        #region 유니티 UGUI 이벤트

        public void Resume()
        {
            Destroy(pausePanel.Panel);
            pausePanel = null;
            Time.timeScale = 1;
            isPausePanelOpen = false;
            Cursor.visible = false;
        }

        public void Restart()
        {
            SceneManager.LoadScene("MainGame");
        }

        public void GoMainMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }

        public void EndWritingClass()
        {
            userClass = writeInfoPanel.ClassField.text;
            Debug.Log("입력: " + userClass);
        }

        public void EndWritingName()
        {
            userName = writeInfoPanel.NameField.text;
            Debug.Log("입력: " + userName);
        }

        public void SendInfo()
        {
            Debug.Log("보내기버튼 눌림");

            StartCoroutine(SendUserScore());
            sentInfo = true;
            OpenSinglePanel(gameOverPanel);
        }

        public void CancelSendInfo()
        {
            Debug.Log("취소버튼 눌림");

            sentInfo = false;
            OpenSinglePanel(gameOverPanel);
        }

        public void OpenWriteInfoPanel()
        {
            OpenSinglePanel(writeInfoPanel);
            Debug.Log("정보작성패널 열림");
        }

        public void OpenGameOverPanel()
        {
            OpenSinglePanel(gameOverPanel);
            Debug.Log("게임오버패널 열림");
        }

        #endregion 유니티 UGUI 이벤트

        /// <summary>
        /// game over panel과 write info panel중 하나만 띄웁니다.
        /// </summary>
        public void OpenSinglePanel(BasePanel target)
        {
            if (target == gameOverPanel)
            {
                gameOverPanel.Open();
                writeInfoPanel.Close();
                if (sentInfo)
                {
                    gameOverPanel.OpenWriteInfoButton.interactable = false;
                }
            }
            else
            {
                gameOverPanel.Close();
                writeInfoPanel.Open();
            }
        }

        /// <summary>
        /// 유저가 입력한 학번, 이름, 점수를 서버에 기록합니다.
        /// </summary>
        /// <returns></returns>
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