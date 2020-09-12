using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameSession gameStatus;

    //  state variables
    int timesHit;

    // consts
    float volume = 0.1f;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameSession>();
        if (tag == "Breakable")
        {
            level.countBreakableBlocks();
        }
            
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(tag == "Breakable")
        {
            HandleHit();
        }

    }

    private void HandleHit()
    {
        timesHit++;
        if (timesHit >= maxHits)
        {
            gameStatus.addPointsToScore();
            destroyBlock();
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
}
