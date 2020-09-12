using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    // parameters
    [SerializeField] int breakableBlocks; // serialized for debugging purposes
    [SerializeField] int numberOfBalls; //TODO Serialized for debugging

    // cached references
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void countBlocks()
    {
        breakableBlocks++;
    }

    public void countBalls()
    {
        numberOfBalls++;
    }

    public void ballLost()
    {
        numberOfBalls--;
    }

    public int NumberOfBalls()
    {
        return numberOfBalls;
    }

    public void blockDestroyed()
    {
        breakableBlocks--;
        if(breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
