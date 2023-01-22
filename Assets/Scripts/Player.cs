using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public float runSpeed;
    public float jumpForce;
    public string NextLevelName;
    SpriteRenderer sr;
    Rigidbody2D rb;
    //float horizontalMove = 0f;

    private Animator animator;
    public static bool isDead;

    PlayerControl control;
    float direction = 0f;
    void Awake()
    {
        control = new PlayerControl();
        control.Enable();
        control.Navigation.Move.performed += ctx =>
        {
            direction = ctx.ReadValue<float>(); // direction = 1 for right arrow, -1 for left arrow, and 0 when released
        };

        // Jump Player
        control.Navigation.Jump.performed += ctx => JumpPlayer();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //horizontalMove = Input.GetAxis("Horizontal") * Time.deltaTime * runSpeed;

        if (!isDead)
        {
            rb.velocity = new Vector2(direction * runSpeed * Time.fixedDeltaTime, rb.velocity.y);

        }
        else
        {
            StartCoroutine(ReloadLostGame());
        }

        // Setting the player speed to change animations accordingly
        animator.SetFloat("Speed", Mathf.Abs(direction));
        Debug.Log(direction);

        if(direction > 0 && !isDead)
        {
            sr.flipX = false;
            animator.SetBool("isRunning", true);
        }
        else if(direction < 0 && !isDead)
        {
            sr.flipX=true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


        // Jumping
        if (!isDead && Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            animator.SetBool("isJump", true);

            // Play jump sound
            AudioManager.instance.PlayMySound("Jump");
        }
    }
    private IEnumerator ReloadLostGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }


    // for canceling jumping animation..
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            animator.SetBool("isJump", false);

            // Play grounded sound
            AudioManager.instance.PlayMySound("FootStep");
        }
        else if (collision.gameObject.CompareTag("water"))
        {
            isDead = true;
            animator.SetBool("isDead", true);

            // Play splashing into water sound
            AudioManager.instance.PlayMySound("WaterSplash");
        }

        // Coins Collection
        else if (collision.gameObject.CompareTag("coin"))
        {
            Destroy(collision.gameObject);

            // Play coin collected sound
            AudioManager.instance.PlayMySound("Coin");

            // Adding coin scores
            ScoreManager.GetInstance().AddScores(10);
            ScoreManager.GetInstance().UpdateScoreUI();

            // storing to the max store in player prefs
            if(ScoreManager.GetInstance().GetScores() > PlayerPrefs.GetInt("MaxScore"))
            {
                PlayerPrefs.SetInt("MaxScore", PlayerPrefs.GetInt("MaxScore") + 10);
            }
        }
        else if (collision.gameObject.CompareTag("spike"))
        {
            isDead = true;
            animator.SetBool("isDead", true);

            // Play spike collision sound
            AudioManager.instance.PlayMySound("Spike");
        }
        else if (collision.gameObject.CompareTag("saw"))
        {
            isDead = true;
            animator.SetBool("isDead", true);

            // Play spike collision sound
            AudioManager.instance.PlayMySound("SawSawing");
        }
        else if (collision.gameObject.CompareTag("cup"))
        {
            // Play victory sound
            AudioManager.instance.PlayMySound("Win");
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(NextLevelName);

    }

    void JumpPlayer()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
