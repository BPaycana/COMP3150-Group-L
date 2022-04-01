using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool towerHeld;

    // Start is called before the first frame update
    void Start()
    {
        towerHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void towerHoldBool()
    {
        if(towerHeld == true)
        {
            towerHeld = false;
        }
        else if(towerHeld == false)
        {
            towerHeld = true;
        }
    }

    public bool getTowerHeld()
    {
        return towerHeld;
    }

}
