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

    public string bestTime1;
    public string bestTime2;
    public string bestTime3;

    public GameData(bool classicEndless, bool moveControls, bool interactControls)
    {
        gameMode = classicEndless;
        movement = moveControls;
        interact = interactControls;
    }

}
