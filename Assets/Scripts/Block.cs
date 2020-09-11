using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakSound;

    // cached reference
    Level level;
    GameStatus gameStatus;

    private void Start()
    {
        level = FindObjectOfType<Level>();
        gameStatus = FindObjectOfType<GameStatus>();
        level.countBreakableBlocks();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameStatus.addPointsToScore();
        destroyBlock();
    }

    private void destroyBlock()
    {
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
        Destroy(gameObject);
        level.blockDestroyed();
    }
}
