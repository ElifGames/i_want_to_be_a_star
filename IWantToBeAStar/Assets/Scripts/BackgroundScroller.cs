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

            Sprites[0].sprite = GetBackground(BackgroundList.LowSky);
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
