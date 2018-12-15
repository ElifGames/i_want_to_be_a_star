using System.Collections.Generic;
using UnityEngine;
namespace IWantToBeAStar
{
    public partial class BackgroundScroller : MonoBehaviour
    {
        public float Speed = 1;
        public SpriteRenderer[] Sprites = new SpriteRenderer[2];
        public BackgroundList BackgroundList;

        private float heightCamera;

        private Vector3 PositionCam;
        private Camera cam;
        private int bgShuffleCount;

        private void Start()
        {
            cam = Camera.main;
            heightCamera = 2f * cam.orthographicSize;

            Sprites[0].sprite = BackgroundList.GroundToLowSky;
            Sprites[1].sprite = GetBackground(BackgroundList.LowSky);
            bgShuffleCount = 1;
            GameData.BgStatus = BackgroundStatus.LowSky;
        }

        private void Update()
        {
            foreach (var item in Sprites)
            {
                if (item.transform.position.y + item.bounds.size.y / 2 < cam.transform.position.y - heightCamera / 2)
                {
                    SpriteRenderer sprite = Sprites[0];
                    foreach (var i in Sprites)
                    {
                        if (i.transform.position.y > sprite.transform.position.y)
                            sprite = i;
                    }

                    item.transform.position = new Vector2(sprite.transform.position.x, (sprite.transform.position.y + (sprite.bounds.size.y / 2) + (item.bounds.size.y / 2)));
                }

                item.transform.Translate(new Vector2(0, Time.deltaTime * Speed * -1));
            }
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
            var returnValue = sprites[bgShuffleCount];
            if (bgShuffleCount <= 3)
            {
                bgShuffleCount = 1;
            }
            else
            {
                bgShuffleCount++;
            }
            return returnValue;
        }
    }

    [System.Serializable]
    public class BackgroundList
    {
        public Sprite GroundToLowSky;
        public List<Sprite> LowSky = new List<Sprite>();
        public Sprite LowSkyToHighSky;
        public List<Sprite> HighSky = new List<Sprite>();
        public Sprite HighSkyToSpace;
        public List<Sprite> Space = new List<Sprite>();
    }
}
