using UnityEngine;

namespace IWantToBeAStar.MapObjects
{
    public class PlayerMove : MonoBehaviour
    {
        public MapSize MapSize;

        public float speed = 10f;

        // Update is called once per frame
        private void FixedUpdate()
        {
            #region 마우스 커서 따라가기

            transform.position = Vector2.Lerp
                (transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), speed);

            // 플레이어가 맵 밖을 나가지 않도록 하는 코드
            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, MapSize.xMin, MapSize.xMax),
                Mathf.Clamp(transform.position.y, MapSize.yMin, MapSize.yMax));

            #endregion 마우스 커서 따라가기

        }
    }
}