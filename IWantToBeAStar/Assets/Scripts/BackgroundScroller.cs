﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace IWantToBeAStar
{
    public class BackgroundScroller : MonoBehaviour
    {
        #region Setting values
        public BackgroundList BackgroundList;
        public SpriteRenderer FirstSprite;
        public SpriteRenderer SecondSprite;
        public float scrollSpeed;
        #endregion

        private readonly float tileSizeY = 10.8f;

        private Vector3 startPosition;
        private bool needBgChange = false;
        private bool needBgReturn = false;
        private BackgroundStatus ChangingTarget;
        private int bgRotateCount = 0;

        void Start()
        {

            startPosition = transform.position;

            GameController controller = FindObjectOfType<GameController>();
            controller.WaveStarted += OnWaveStarted;

            bgRotateCount = 0;

            GameData.BgStatus = BackgroundStatus.LowSky;
        }

        void Update()
        {
            BackgroundScroll();
        }

        private void BackgroundScroll()
        {
            if (transform.position.y <= -tileSizeY)
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
                    #endregion
                }
                #endregion
                else
                {
                    switch (GameData.BgStatus)
                    {
                        // case BackgroundStatus.Ground:

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

        private void OnWaveStarted(object sender, EventArgs e)
        {
            StartCoroutine(WhenWaveStarted((e as WaveStartedEventArgs).WaveCount));
        }

        IEnumerator WhenWaveStarted(int waveCount)
        {
            Debug.Log("이벤트 메소드 실행");
            switch (waveCount)
            {
                case 1:
                    ChangingTarget = BackgroundStatus.LowSky;
                    needBgChange = true;
                    break;
                case 5:
                    ChangingTarget = BackgroundStatus.HighSky;
                    needBgChange = true;
                    break;
                case 10:
                    ChangingTarget = BackgroundStatus.Space;
                    needBgChange = true;
                    break;
            }

            yield return new WaitForSeconds(0.1f);
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