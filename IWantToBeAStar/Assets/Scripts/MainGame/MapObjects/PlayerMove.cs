using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects
{
    public class PlayerMove : MonoBehaviour
    {
        public MapSize MapSize;
        public float speed = 10f;

        private Rigidbody2D rigidbody2d;

        private void Start()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (GameData.MouseControl)
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
            else
            {
                #region 키보드 조작
                float inputX = Input.GetAxis("Horizontal");
                float inputY = Input.GetAxis("Vertical");
                Vector2 velocity = new Vector2(inputX, inputY);
                velocity *= speed;
                rigidbody2d.velocity = velocity;
                rigidbody2d.position = new Vector2(
                    Mathf.Clamp(rigidbody2d.position.x, MapSize.xMin, MapSize.xMax),
                    Mathf.Clamp(rigidbody2d.position.y, MapSize.yMin, MapSize.yMax));
                #endregion 키보드 조작
            }
        }
    }
}