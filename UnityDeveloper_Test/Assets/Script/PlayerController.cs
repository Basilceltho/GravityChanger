using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    int speed = 3;
    int sp = 50;
    int jumpspeed = 5;
    Rigidbody rb;
    public LayerMask mask, mask1, mask2, mask3, mask4, mask5;
    int forw, backw, leftw, rightw, score, start;
   public bool jump = true;
    bool fall = true;
    public Animator animator;
    public GameObject body;
    public GameObject hololeft, holoright, holoforward, holoback;
    public TextMeshProUGUI scoreTxt, timerTxt, gameOverTxt;
    float timer;
    float startingTime = 120;
    public GameObject panel,buttons;
    public GameObject[] parts;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = startingTime;
    
    }

   
    void Update()
    {
        //This allows to stat the game by clicking on the play button
        if (start == 1)
        {
            //GameControl function controls the game by implementing a countdown timer and also specific win and lose conditions
            GameControlCondition();

            //Playercontroller function allows to control the movements of the player 
            PlayerControl();

            //Gravitycontroller function allows the player to manipulate gravity using designated keys
            GravityController();
        }
    }



    //Player Controller function
    //This function allows to control the movements of the player by using designated keys  
    void PlayerControl()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            animator.Play("Running");

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            animator.Play("Running");
        }


        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.down * sp * Time.deltaTime);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * sp * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space) && jump == true)
        {

            rb.AddForce(transform.up * jumpspeed, ForceMode.Impulse);
            jump = false;
            animator.Play("Falling");

        }
          if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                holoforward.SetActive(true);
                holoback.SetActive(false);
                hololeft.SetActive(false);
                holoright.SetActive(false);
                forw = 1;


            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {

                holoback.SetActive(true);
                holoforward.SetActive(false);
                hololeft.SetActive(false);
                holoright.SetActive(false);
                backw = 1;

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {

                hololeft.SetActive(true);
                holoright.SetActive(false);
                holoforward.SetActive(false);
                holoback.SetActive(false);
                leftw = 1;

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {

                holoright.SetActive(true);
                hololeft.SetActive(false);
                holoforward.SetActive(false);
                holoback.SetActive(false);
                rightw = 1;

            }


            if (Input.GetKeyDown(KeyCode.Return))
            {

                if (forw == 1)
                {
                    fall = false;
                    holoforward.SetActive(false);
                    transform.position = holoforward.transform.position;
                    transform.rotation = holoforward.transform.rotation;
                    forw = 0;
                }
                if (backw == 1)
                {
                    fall = false;
                    holoback.SetActive(false);
                    transform.position = holoback.transform.position;
                    transform.rotation = holoback.transform.rotation;
                    backw = 0;
                }
                if (leftw == 1)
                {
                    fall = false;
                    hololeft.SetActive(false);
                    transform.position = hololeft.transform.position;
                    transform.rotation = hololeft.transform.rotation;
                    leftw = 0;
                }
                if (rightw == 1)
                {
                    fall = false;
                    holoright.SetActive(false);
                    transform.position = holoright.transform.position;
                    transform.rotation = holoright.transform.rotation;
                    rightw = 0;
                }
            }
        
    }



    //Game Controller function
    //This function is created to control the overall game by implementing certain winning and losing conditions

    void GameControlCondition()
    {
        //this allows players to see the score 
        scoreTxt.text = "Score = " + score + " /5";


        //Function to implement the countdown timer
        timer -= 1 * Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerTxt.text = "Timer =" + string.Format("{0:00}:{1:00}", minutes, seconds);
        //Gameover condition case-1 || * Game will end if all the cubes are not collected and the time runs out 
        if (timer <= 0)
        {
            start = 0;
            gameOverTxt.text = "Game Over";
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].gameObject.SetActive(false);
            }
        }



        //GameWin condition || * Player will win the game if all the  cubees are collected within the countdown time
        if (score == 5)
        {
            start = 0;
            gameOverTxt.text = "You Win";
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].gameObject.SetActive(false);
            }
        }
    }


    //Gravity controller function 
    //This function uses raycast to identify the ground or platform below the player and controls the gravity of the player
    void GravityController()
    {
        


            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit, 20f, mask))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit.distance, Color.green);
                Physics.gravity = new Vector3(0, -9.8f, 0);
                if (hit.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }
            }
            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit1, 20f, mask1))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit1.distance, Color.green);
                Physics.gravity = new Vector3(0, 0, -9.8f);
                if (hit1.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit1.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }

            }
            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit2, 20f, mask2))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit2.distance, Color.green);
                Physics.gravity = new Vector3(0, 9.8f, 0);
                if (hit2.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit2.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }

            }
            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit3, 20f, mask3))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit3.distance, Color.green);
                Physics.gravity = new Vector3(0, 0, 9.8f);
                if (hit3.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit3.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }
            }
            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit4, 20f, mask4))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit4.distance, Color.green);
                Physics.gravity = new Vector3(9.8f, 0, 0);
                if (hit4.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit4.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }
            }
            if (Physics.Raycast(body.transform.position, -transform.up, out RaycastHit hit5, 20f, mask5))
            {
                Debug.DrawRay(body.transform.position, -transform.up * hit5.distance, Color.green);
                Physics.gravity = new Vector3(-9.8f, 0, 0);
                if (hit5.collider.gameObject.CompareTag("endwall"))
                {
                    animator.Play("Falling");
                }
                if (hit5.collider.gameObject.CompareTag("Ground") && fall == false)
                {
                    animator.Play("Falling");
                }

            }
            else
            {
                Debug.DrawRay(body.transform.position, -transform.up * 20f, Color.red);
            }
        
    }



    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //To check if the  player  is on the ground so as to implement an animation for the fall
            fall = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //This conditions allows to check the colloision between the player and other specific objects

        if (collision.gameObject.CompareTag("Ground"))
        {
            //To allow only a single jump at a time
            jump = true;
            
        }

        if (collision.gameObject.CompareTag("Box"))
        {
            //This is to increase the score if cube is collected
            score++;

            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("endwall"))
        {
            //GameOver condition case - 2 || * Checks if the player is free falling 
            start = 0;
            gameOverTxt.text = "Game Over";
            buttons.SetActive(true);
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].gameObject.SetActive(false);
            }
        }
    }

    //Exit button
    //This function allows player to quit the game
    public void ExitButton()
    {
        Application.Quit();
    }
    



    //Play Button 
    //This public function allows the player to start the game by clicking on the play button in the game window
    public void PlayBtn()
    {
        start = 1;
        
        panel.SetActive(false);
    }
}
