using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public MapSize MapSize;
        public float CursorSpeed;
        public float KeyboardSpeed;

        private Rigidbody2D rigidbody2d;

        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            switch (GameData.Controller)
            {
                case Controllers.Keyboard:
                    KeyboardControl();
                    break;

                case Controllers.Mouse:
                    MouseControl();
                    break;
            }
        }

        private void KeyboardControl()
        {
            float inputX = Input.GetAxis("Horizontal");
            float inputY = Input.GetAxis("Vertical");
            Vector2 velocity = new Vector2(inputX, inputY);
            velocity *= KeyboardSpeed;
            rigidbody2d.velocity = velocity;
            rigidbody2d.position = new Vector2(
                Mathf.Clamp(rigidbody2d.position.x, MapSize.xMin, MapSize.xMax),
                Mathf.Clamp(rigidbody2d.position.y, MapSize.yMin, MapSize.yMax));
        }

        private void MouseControl()
        {
            // 그냥 마우스 따라 움직이는 코드
            //transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //부드럽게 움직이는 코드
            transform.position =
                Vector2.Lerp(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), CursorSpeed);

            // 플레이어가 맵 밖을 나가지 않도록 하는 코드
            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, MapSize.xMin, MapSize.xMax),
                Mathf.Clamp(transform.position.y, MapSize.yMin, MapSize.yMax));
        }
    }
}