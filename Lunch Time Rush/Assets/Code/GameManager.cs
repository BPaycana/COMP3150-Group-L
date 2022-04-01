using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private bool towerHeld;
    private bool canRestock;
    private bool getRestock;

    // Start is called before the first frame update
    void Start()
    {
        towerHeld = false;
        canRestock = false;
        getRestock = true;
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

    public void restock()
    {
        if (canRestock == true)
        {
            canRestock = false;
        }
        else if (canRestock == false)
        {
            canRestock = true;
        }
        Debug.Log(canRestock);
    }

    public bool restockState()
    {
        Debug.Log(canRestock);
        return canRestock;
    }
}
