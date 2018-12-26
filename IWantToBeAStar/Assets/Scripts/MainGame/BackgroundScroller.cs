using System;
using System.Collections;
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
        public float scrollSpeed;

        #endregion 유니티 세팅 값

        private readonly float tileChangeLine = 16f;
        // private readonly float tileSizeZ = 0.5f;

        private Vector3 startPosition;
        private bool needBgChange = false;
        private bool needBgReturn = false;
        private BackgroundStatus ChangingTarget;
        private int bgRotateCount = 0;

        private void Start()
        {
            startPosition = transform.position;

            GameController controller = FindObjectOfType<GameController>();
            controller.OnBackgroundChange += WhenReceivedBgChangeEvent;

            bgRotateCount = 0;

            GameData.BgStatus = BackgroundStatus.LowSky;
            //StartCoroutine("BackgroundScroll");
        }

        private void FixedUpdate()
        {
            BackgroundScroll();
        }

        private void WhenReceivedBgChangeEvent(BackgroundStatus status)
        {
            StartCoroutine(BackGroundChange(status));
        }

        private IEnumerator BackGroundChange(BackgroundStatus status)
        {
            Debug.Log("이벤트 메소드 시작");
            ChangingTarget = status;
            needBgChange = true;

            yield return new WaitForSeconds(0.1f);
        }

        private void BackgroundScroll()
        {
            if (transform.position.y <= -tileChangeLine)
            {
                FirstSprite.sprite = SecondSprite.sprite;

                transform.position = startPosition;

                Sprite ChangeSprite = null;

                #region 배경 전환이 필요한 경우

                if (needBgChange)
                {
                    if (!needBgReturn)
                    {
                        switch (ChangingTarget)
                        {
                            case BackgroundStatus.LowSky:
                                {
                                    ChangeSprite = GetBackground(BackgroundList.LowSky);
                                    needBgReturn = false;
                                    needBgChange = false;
                                    GameData.BgStatus = BackgroundStatus.LowSky;
                                    break;
                                }
                            case BackgroundStatus.HighSky:
                                {
                                    ChangeSprite = BackgroundList.LowSkyToHighSky;
                                    needBgReturn = true;
                                    GameData.BgStatus = BackgroundStatus.HighSky;
                                    break;
                                }
                            case BackgroundStatus.Space:
                                {
                                    ChangeSprite = BackgroundList.HighSkyToSpace;
                                    needBgReturn = true;
                                    GameData.BgStatus = BackgroundStatus.Space;
                                    break;
                                }
                        }
                    }

                    #region 배경 전환 이미지 들어간 후 다음 이미지 교체작업

                    else
                    {
                        switch (ChangingTarget)
                        {
                            case BackgroundStatus.HighSky:
                                ChangeSprite = GetBackground(BackgroundList.HighSky);
                                break;

                            case BackgroundStatus.Space:
                                ChangeSprite = GetBackground(BackgroundList.Space);
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
                    switch (GameData.BgStatus)
                    {
                        case BackgroundStatus.LowSky:
                            ChangeSprite = GetBackground(BackgroundList.LowSky);
                            break;

                        case BackgroundStatus.HighSky:
                            ChangeSprite = GetBackground(BackgroundList.HighSky);
                            break;

                        case BackgroundStatus.Space:
                            ChangeSprite = GetBackground(BackgroundList.Space);
                            break;
                    }
                }

                SecondSprite.sprite = ChangeSprite;
            }

            transform.Translate(new Vector3(0, Time.deltaTime * scrollSpeed * -1, startPosition.z));
        }

        /// <summary>
        /// 서로 다른 배경들을 번갈아가면서 반환합니다.
        /// 예를 들어 <see cref="BackgroundStatus.LowSky"/>에서
        /// 배경 3개를 번갈아가며 한번 호출될때마다 서로 다른 배경을 반환합니다.
        /// </summary>
        /// <param name="sprites"></param>
        /// <returns></returns>
        private Sprite GetBackground(List<Sprite> sprites)
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