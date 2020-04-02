using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration Parameters
    [SerializeField] float screenWidthInUnits = 16f;
    [SerializeField] float minX = 2f;
    [SerializeField] float maxX = 14.35f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mousePosXInUnits = (Input.mousePosition.x / Screen.width * screenWidthInUnits);

        //compact way of storing X and Y co-ordinates
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        paddlePosition.x = Mathf.Clamp(mousePosXInUnits, minX, maxX);
        transform.position = paddlePosition;
    }
}
