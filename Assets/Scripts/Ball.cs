using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xLaunch = 2f;
    [SerializeField] float yLaunch = 15f;
    [SerializeField] float randomFactor = 0.2f;

    [SerializeField] AudioClip[] ballSounds;

    //State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidbody2D;
    CircleCollider2D myCollider;
    Level level;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<CircleCollider2D>();
        level = FindObjectOfType<Level>();

        if (gameObject.tag != "Temp Ball")
        {
            //Calculate the initial distance between the paddle and the ball
            paddleToBallVector = transform.position - paddle1.transform.position;
        }

        level.countBalls();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted && gameObject.tag != "Temp Ball")
        {
            LockBallToPaddle(hasStarted);
            LaunchOnClick();
        }
        
    }

    private void LockBallToPaddle(bool startState)
    {
            Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
            transform.position = paddlePos + paddleToBallVector;
    }

    private void LaunchOnClick()
    {
        if(Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            myRigidbody2D.velocity = new Vector2(xLaunch, yLaunch);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak = new Vector2(
            Random.Range(0f, randomFactor),
            Random.Range(0f, randomFactor)
            );

        if(hasStarted)
        {
            if(collision.otherRigidbody.name.Contains("Ball"))
            {
                Physics2D.IgnoreCollision(collision.otherCollider, myCollider);
                return;
            }
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidbody2D.velocity += velocityTweak;
        }
    }
}
