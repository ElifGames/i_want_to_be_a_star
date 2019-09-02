using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IWantToBeAStar.MainGame
{
    public class BackgroundScroller : GameManager
    {
        #region 유니티 세팅 값

        public BackgroundList BackgroundList;
        public SpriteRenderer FirstSprite;
        public SpriteRenderer SecondSprite;
        public float scrollSpeed;

        #endregion 유니티 세팅 값

        private readonly float tileChangeLine = 16f;
        // private readonly float tileSizeZ = 0.5f;

        private Vector3 startPosition;
        private bool needBgChange = false;
        private bool needBgReturn = false;
        private Stage ChangingTarget;

        // 백그라운드 순환
        private int bgRotateCount = 0;

        private void Start()
        {
            startPosition = transform.position;

            bgRotateCount = 0;

            GameData.CurrentStage = Stage.Ground;
            // 처음 시작시 1번과 2번 스프라이트는 각각 Ground, Ground-LowSky임.
            // 따라서 바로 LowSky로 넘어가도 됨.
            needBgChange = true;
        }

        private void FixedUpdate()
        {
            BackgroundScroll();
        }

        private void WhenReceivedBgChangeEvent(Stage status)
        {
            StartCoroutine(BackGroundChange(status));
        }

        private IEnumerator CheckScore()
        {
            if (GameData.IsGameRunning)
            {

            }
        }

        private IEnumerator BackGroundChange(Stage status)
        {
            Debug.Log("배경 변경");
            ChangingTarget = status;
            needBgChange = true;

            yield return new WaitForSeconds(0.1f);
        }

        private void BackgroundScroll()
        {
            // 만약 스프라이트의 상단이 화면 상단에 도달했을 때
            if (transform.position.y <= -tileChangeLine)
            {
                // 1번 스프라이트를 2번 스프라이트로 옮기기
                FirstSprite.sprite = SecondSprite.sprite;

                // 다시 처음 위치로 이동
                transform.position = startPosition;

                
                Sprite ChangeSprite = null;

                #region 배경 전환이 필요한 경우

                if (needBgChange)
                {
                    if (!needBgReturn)
                    {
                        switch (ChangingTarget)
                        {
                            case Stage.LowSky:
                                {
                                    ChangeSprite = GetBackgroundRotate(BackgroundList.LowSky);
                                    needBgReturn = false;
                                    needBgChange = false;
                                    GameData.CurrentStage = Stage.LowSky;
                                    break;
                                }
                            case Stage.HighSky:
                                {
                                    ChangeSprite = BackgroundList.LowSkyToHighSky;
                                    needBgReturn = true;
                                    GameData.CurrentStage = Stage.HighSky;
                                    break;
                                }
                            case Stage.Space:
                                {
                                    ChangeSprite = BackgroundList.HighSkyToSpace;
                                    needBgReturn = true;
                                    GameData.CurrentStage = Stage.Space;
                                    break;
                                }
                        }
                    }

                    #region 배경 전환 이미지 들어간 후 다음 이미지 교체작업

                    else
                    {
                        switch (ChangingTarget)
                        {
                            case Stage.HighSky:
                                ChangeSprite = GetBackgroundRotate(BackgroundList.HighSky);
                                break;

                            case Stage.Space:
                                ChangeSprite = GetBackgroundRotate(BackgroundList.Space);
                                break;
                        }

                        needBgChange = false;
                        needBgReturn = false;
                    }

                    #endregion 배경 전환 이미지 들어간 후 다음 이미지 교체작업
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
            // 스프라이트 내리기
            transform.Translate(new Vector3(0, Time.deltaTime * scrollSpeed * -1, startPosition.z));
        }

        /// <summary>
        /// 서로 다른 배경들을 번갈아가면서 반환합니다.
        /// 예를 들어 <see cref="Stage.LowSky"/>에서
        /// 배경 3개를 번갈아가며 한번 호출될때마다 서로 다른 배경을 반환합니다.
        /// </summary>
        /// <param name="sprites"></param>
        /// <returns></returns>
        private Sprite GetBackgroundRotate(List<Sprite> sprites)
        {
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