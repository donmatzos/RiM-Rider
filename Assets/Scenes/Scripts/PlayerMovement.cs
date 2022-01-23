using System;
using System.Collections;
using UnityEngine;

namespace Scenes.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        private static float laneWidth = 2;
        public SwipeManager swipeControls;
        [Range(-2,2)] public float value;
        public float Speed;
        private Rigidbody rigid;
        public float transitionDuration=0.2F;
        private bool isInTransition = false;
        private Direction nextMovement = Direction.NONE;
        private float actualSpeed;
        public float speedMultiplikator=0;
        private Vector3 startPos;
        private FloatingText startFloatingText = null;
        private string startText = "TAP TO START!";
        private Color startTextColor=Color.white;
        
        public bool gameIsStarted = false;
        public bool gameIsStopped = false;
        // Start is called before the first frame update
        void Start()
        {
            swipeControls = gameObject.AddComponent<SwipeManager>();
            
            startPos = transform.position;
            setActualSpeed();
            rigid = GetComponent<Rigidbody>();
            GameManager.player = gameObject;
            GameManager.playerMovement = this;
            startFloatingText = GameManager.ShowFloatingText(startText,startTextColor);
        }

        public void setSpeedMultiplikator(float m)
        {
            speedMultiplikator = m;
            setActualSpeed();
        }
    
        private void setActualSpeed()
        {
            actualSpeed = Speed * speedMultiplikator;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
            rigid.velocity = (Vector3.forward * Time.deltaTime * actualSpeed);
        }

        public void Reset()
        {
            gameIsStopped = false;
            startFloatingText = GameManager.ShowFloatingText(startText,startTextColor);
            transform.position = startPos;
            speedMultiplikator = 0;
            value = 0;
            setActualSpeed();
            gameIsStarted = false;
        }

        private void LateUpdate()
        {
            if (gameIsStopped)
            {
                return;
            }
        
            if ((Input.anyKey || swipeControls.Tap)&& !gameIsStarted)
            {
                gameIsStarted = true;
                speedMultiplikator = 1F;
                setActualSpeed();
                startFloatingText.HideText();
            }
        
            if (!gameIsStarted)
            {
                return;
            }
            
            if (Application.platform == RuntimePlatform.Android) {
        
                // Check if Back was pressed this frame
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    GameObject canvas = GameObject.Find("Canvas");
                    PauseMenu pauseMenu = canvas.GetComponent<PauseMenu>();
                    if (PauseMenu.GameIsPaused)
                    {
                     
                       pauseMenu.Resume();
                    }
                    else
                    {
                        pauseMenu.Pause();
                    }
                    // Quit the application
                    
                }
            }

            if (PauseMenu.GameIsPaused)
            {
                return;
            }
    
            if (Input.GetButtonDown("Right") || swipeControls.SwipeRight)
            {
                if (value == laneWidth)
                {
                    return;
                }
                //value += laneWidth;
                if (!isInTransition)
                {
                    nextMovement = Direction.NONE;
                    StartCoroutine(transformValue(Direction.RIGHT));
                }
                else
                {
                    nextMovement = Direction.RIGHT;
                }
            }
        
            if (Input.GetButtonDown("Left") || swipeControls.SwipeLeft)
            {
                if (value == -laneWidth)
                {
                    return;
                }
                //value -= laneWidth;
                if (!isInTransition)
                {
                    nextMovement = Direction.NONE;
                    StartCoroutine(transformValue(Direction.LEFT));
                }
                else
                {
                    nextMovement = Direction.LEFT;
                }
            }

            if (!isInTransition && nextMovement != Direction.NONE)
            {
                StartCoroutine(transformValue(nextMovement));
                nextMovement = Direction.NONE;
            }
        
            if (Input.GetKeyUp(KeyCode.R))
            {
                Reset();
            }
        
        }

        private IEnumerator transformValue(Direction direction)
        {
            isInTransition = true;
            float stepSize = 0.45F;
            float totalSteps = laneWidth / stepSize;
            float waitTime =  transitionDuration/totalSteps;
            for (int i = 0; i < totalSteps-1; i++)
            {
                switch (direction)
                {
                    case Direction.RIGHT:
                        if (value < laneWidth)
                        {
                            value += stepSize;
                        }
                        break;
                    case Direction.LEFT:
                        if (value > -laneWidth)
                        {
                            value -= stepSize;
                        }
                        break;
                    default:
                        i =  Convert.ToInt32(totalSteps);
                        break;
                }
                yield return new WaitForSeconds(waitTime);
            }
            value = (float)Math.Round(value, MidpointRounding.ToEven);

            isInTransition = false;
        }

        private enum Direction
        {
            NONE,
            LEFT,
            RIGHT
        }
    }
}
