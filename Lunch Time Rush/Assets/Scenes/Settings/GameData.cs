using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // true == classic, false == endless
    public bool gameMode;
    // true == joystick, false == hold finger
    public bool movement;
    // true == tap anywhere; false == tap object
    public bool interact;

    /*
    public int[] bestTime1 = new int[3];
    public int[] bestTime2 = new int[3];
    public int[] bestTime3 = new int[3];
    */

    public int bestTime1Min;
    public int bestTime1Sec;
    public int bestTime1Ms;
    public int bestTime2Min;
    public int bestTime2Sec;
    public int bestTime2Ms;
    public int bestTime3Min;
    public int bestTime3Sec;
    public int bestTime3Ms;

    /*
    public string bestTime1;
    public string bestTime2;
    public string bestTime3;
    */
    
    public GameData(bool classicEndless, bool moveControls, bool interactControls, int level1TimeMin, int level1TimeSec, int level1TimeMs, int level2TimeMin,
        int level2TimeSec, int level2TimeMs, int level3TimeMin, int level3TimeSec, int level3TimeMs)
    {
        gameMode = classicEndless;
        movement = moveControls;
        interact = interactControls;

        bestTime1Min = level1TimeMin;
        bestTime1Sec = level1TimeSec;
        bestTime1Ms = level1TimeMs;
        bestTime2Min = level2TimeMin;
        bestTime2Sec = level2TimeSec;
        bestTime2Ms = level2TimeMs;
        bestTime3Min = level3TimeMin;
        bestTime3Sec = level3TimeSec;
        bestTime3Ms = level3TimeMs;

    }
    

    /*
    public GameData(bool classicEndless, bool moveControls, bool interactControls, string level1Time, string level2Time, string level3Time)
    {
        gameMode = classicEndless;
        movement = moveControls;
        interact = interactControls;

        bestTime1 = level1Time;
        bestTime2 = level2Time;
        bestTime3 = level3Time;
    }
    */
}
