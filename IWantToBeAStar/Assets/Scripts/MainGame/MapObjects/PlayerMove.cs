using UnityEngine;

namespace IWantToBeAStar.MainGame.MapObjects
{
    public class PlayerMove : MonoBehaviour
    {
        public MapSize MapSize;
        public float CursorSpeed;
        public float KeyboardSpeed;

        private Rigidbody2D rigidbody2d;
        private GameManager gameManager;
        private bool gameRunning;

        private void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            gameManager.GameStartEvent += HandleGameStartEvent;
            gameRunning = false;
        }

        private void HandleGameStartEvent()
        {
            // GameStartEvent를 받고 나서 움직일 수 있도록 구현함
            gameRunning = true;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (gameRunning)
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
            transform.position = Vector2.Lerp
                                (transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), CursorSpeed);

            // 플레이어가 맵 밖을 나가지 않도록 하는 코드
            transform.position = new Vector2(
                Mathf.Clamp(transform.position.x, MapSize.xMin, MapSize.xMax),
                Mathf.Clamp(transform.position.y, MapSize.yMin, MapSize.yMax));
        }
    }
}