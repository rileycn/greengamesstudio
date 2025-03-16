using System.Collections;
using UnityEngine;

public class player_jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public float jump;
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    // countdown
    private float countdown;
    public float slow_timer = 1;

    // audio
    public random_sound audio;
    //

    public GameObject gameObj;

    public SpriteRenderer playerSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        countdown = slow_timer + 1;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(rb.linearVelocity.x, jump));
        }

        // Countdown
        if(countdown <= slow_timer) {
            countdown -= Time.deltaTime;
        }
        // un-slow time
        if(countdown <= 0) {
            cactus.speed = 0f;
            cactus_all.speed = 10f;
            countdown = slow_timer + 1;
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        // slow time
        if (collision.transform.tag == "Cactus") {
            cactus.speed = 5f;
            cactus_all.speed = 5f;
            countdown -= 1;
            //splat sound
            audio.PlayRandom();
            //
            StartCoroutine(HitAnimation());
        }
        if (collision.transform.tag == "Finish") {
            // cactus_begone.destroy = true;
            GameManager.main.ExitWarning();
            Destroy(gameObj);
        }
    }

    private IEnumerator HitAnimation()
    {
        for (int i = 0; i < 5; i++) {
            playerSprite.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            playerSprite.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }


}
