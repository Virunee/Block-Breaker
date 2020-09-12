using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;
    [SerializeField] GameObject ball;
    [SerializeField] float ballBlockXLaunch = 2f;
    [SerializeField] float ballBlockYLaunch = 15f;

    // cached reference
    Level level;
    GameSession gameStatus;

    //  state variables
    int timesHit;

    // consts
    float volume = 0.1f;

    private void Start()
    {
        gameStatus = FindObjectOfType<GameSession>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable" || tag == "Ball Block")
        {
            level.countBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable" || tag == "Ball Block")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        int maxHits = hitSprites.Length + 1;
        timesHit++;
        if (timesHit >= maxHits)
        {
            if(tag == "Ball Block")
            {
                createNewBall();
            }
            gameStatus.addPointsToScore();
            destroyBlock();
        } else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        } else
        {
            Debug.LogError("Block sprite is missing from array: " + gameObject.name);
        }
        
    }

    private void destroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, volume);
        triggerSparklesVFX();
        Destroy(gameObject);
        
        level.blockDestroyed();
    }

    private void triggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
    }

    private void createNewBall()
    {
        GameObject newBall = Instantiate(ball, transform.position, transform.rotation);
        newBall.GetComponent<Rigidbody2D>().velocity = new Vector2(ballBlockXLaunch, ballBlockYLaunch);
        newBall.tag = "Temp Ball";
    }
}
