using System;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class BackgroundScroller : MonoBehaviour
    {
        #region 유니티 세팅 값

        public BackgroundList BackgroundList;
        public SpriteRenderer FirstSprite;
        public SpriteRenderer SecondSprite;

        /// <summary>
        /// 배경 스크롤 속도
        /// </summary>
        public float scrollSpeed;

        #endregion 유니티 세팅 값

        /// <summary>
        /// 스프라이트가 화면의 최상단에 위치되는 지점
        /// </summary>
        private readonly float tileChangeLine = 16f;

        /// <summary>
        /// 스프라이트가 다시 최초 위치로 이동하는데 사용됩니다.
        /// </summary>
        private Vector3 startPosition;

        /// <summary>
        /// 스테이지가 바뀌었는지 여부를 알려줍니다.
        /// </summary>
        private bool hasStageChanged = false;

        /// <summary>
        /// 변경되는 Stage
        /// </summary>
        private Stage ChangingTarget;

        /// <summary>
        /// 같은 스테이지에서 여러 배경을 순서대로 보여줄때 사용됩니다.
        /// </summary>
        private int bgRotateCount = 0;

        private ScoreManager scoreManager;
        private GameManager gameManager;

        private bool gameRunning;

        private void HandleStageChangedEvent(Stage changedStage)
        {
            Debug.Log("배경 변경");
            ChangingTarget = changedStage;
            hasStageChanged = true;
        }

        private void Awake()
        {
            GameData.BackgroundScrollSpeed = scrollSpeed;
            scoreManager = GameObject.Find("Score Manager").GetComponent<ScoreManager>();
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            scoreManager.StageChangedEvent += HandleStageChangedEvent;
            gameManager.GameStartEvent += HandleGameStartEvent;
            gameRunning = false;
        }

        private void Start()
        {
            startPosition = transform.position;

            bgRotateCount = 0;

            GameData.CurrentStage = Stage.Ground;
            // 처음 시작시 1번과 2번 스프라이트는 각각 Ground, Ground-LowSky임.
            // 따라서 바로 LowSky로 넘어가도 됨.
            hasStageChanged = true;
            ChangingTarget = Stage.LowSky;
        }

        private void HandleGameStartEvent()
        {
            // 배경이 GameStartEvent를 받고 나서 작동할 수 있도록 함
            gameRunning = true;
        }

        private void FixedUpdate()
        {
            if (gameRunning)
            {
                BackgroundScroll();
            }
        }

        private void BackgroundScroll()
        {
            // 만약 스프라이트의 상단이 화면 상단에 도달했을 때
            if (transform.position.y <= -tileChangeLine)
            {
                // 2번 스프라이트를 1번 스프라이트로 옮기기
                FirstSprite.sprite = SecondSprite.sprite;

                // 다시 처음 위치로 이동
                transform.position = startPosition;

                Sprite ChangeSprite = null;

                #region 배경 전환이 필요한 경우

                if (hasStageChanged)
                {
                    switch (ChangingTarget)
                    {
                        case Stage.LowSky:
                            ChangeSprite = GetBackgroundRotate(BackgroundList.LowSky);
                            hasStageChanged = false;
                            GameData.CurrentStage = Stage.LowSky;
                            break;

                        case Stage.HighSky:
                            ChangeSprite = BackgroundList.LowSkyToHighSky;
                            hasStageChanged = false;
                            GameData.CurrentStage = Stage.HighSky;
                            break;

                        case Stage.Space:
                            ChangeSprite = BackgroundList.HighSkyToSpace;
                            hasStageChanged = false;
                            GameData.CurrentStage = Stage.Space;
                            break;
                    }
                }

                #endregion 배경 전환이 필요한 경우

                else
                {
                    switch (GameData.CurrentStage)
                    {
                        case Stage.LowSky:
                            ChangeSprite = GetBackgroundRotate(BackgroundList.LowSky);
                            break;

                        case Stage.HighSky:
                            ChangeSprite = GetBackgroundRotate(BackgroundList.HighSky);
                            break;

                        case Stage.Space:
                            ChangeSprite = GetBackgroundRotate(BackgroundList.Space);
                            break;
                    }
                }

                SecondSprite.sprite = ChangeSprite;
            }
            // 스프라이트를 아래로 스크롤
            transform.Translate(new Vector3(0, Time.deltaTime * GameData.BackgroundScrollSpeed * -1, startPosition.z));
        }

        /// <summary>
        /// 서로 다른 3개의 배경들을 번갈아가면서 반환합니다.
        /// 예를 들어 <see cref="Stage.LowSky"/>에서
        /// 배경 3개를 번갈아가며 한번 호출될때마다 서로 다른 배경을 반환합니다.
        /// </summary>
        /// <param name="sprites"></param>
        /// <returns></returns>
        private Sprite GetBackgroundRotate(List<Sprite> sprites)
        {
            // BUG: iws2
            var returnValue = sprites[bgRotateCount];
            if (bgRotateCount >= 2)
            {
                bgRotateCount = 0;
            }
            else
            {
                bgRotateCount++;
            }

            return returnValue;
        }
    }

    [Serializable]
    public class BackgroundList
    {
        public Sprite Ground;
        public Sprite GroundToLowSky;
        public List<Sprite> LowSky = new List<Sprite>();
        public Sprite LowSkyToHighSky;
        public List<Sprite> HighSky = new List<Sprite>();
        public Sprite HighSkyToSpace;
        public List<Sprite> Space = new List<Sprite>();
    }
}