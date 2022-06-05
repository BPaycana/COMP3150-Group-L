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

    
    public int[] bestTime1 = new int[3];
    public int[] bestTime2 = new int[3];
    public int[] bestTime3 = new int[3];
    
    /*
    public string bestTime1;
    public string bestTime2;
    public string bestTime3;
    */
    
    public GameData(bool classicEndless, bool moveControls, bool interactControls, int[] level1Time, int[] level2Time, int[] level3Time)
    {
        gameMode = classicEndless;
        movement = moveControls;
        interact = interactControls;

        bestTime1 = level1Time;
        bestTime2 = level2Time;
        bestTime3 = level3Time;
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
