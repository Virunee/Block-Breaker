using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xLaunch = 2f;
    [SerializeField] float yLaunch = 15f;

    //State
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        //Calculate the initial distance between the paddle and the ball
        paddleToBallVector = transform.position - paddle1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!hasStarted)
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
            GetComponent<Rigidbody2D>().velocity = new Vector2(xLaunch, yLaunch);
        }
    }
}
