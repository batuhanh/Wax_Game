using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This is my EventManager Class, It holds all of the event that this 
 * project has. So it is became to more easy to understand Events of project.
 */
public class EventManager : MonoBehaviour
{
    public delegate void LevelStarted();
    public static event LevelStarted myLevelStarted;

    public delegate void LevelCompleted();
    public static event LevelCompleted myLevelCompleted;

    public delegate void LevelFailed();
    public static event LevelFailed myLevelFailed;

    public delegate void FirstStageComplated();
    public static event FirstStageComplated firstStageComplated;

    public void CallLevelStartedEvent()
    {
        if (myLevelStarted != null)
            myLevelStarted();
        Debug.Log("LevelStartedEvent");
    }
    public void CallLevelCompletedEvent()
    {
        if (myLevelCompleted != null)
            myLevelCompleted();
    }
    public void CallLevelFailedEvent()
    {
        if (myLevelFailed != null)
            myLevelFailed();
    }
    public void CallFirstStageComplatedEvent()
    {
        if (firstStageComplated != null)
            firstStageComplated();
    }

}



