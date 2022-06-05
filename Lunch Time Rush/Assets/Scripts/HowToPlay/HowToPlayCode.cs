using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayCode : MonoBehaviour
{

    public GameObject[] HowToPlayInfo;
    public GameObject next;
    public GameObject back;

    private int num = 0;

    void Start()
    {
        next.SetActive(true);
        back.SetActive(false);
        
        for(int i = 0; i < 4; i++)
        {
            if(i == 0)
            {
                HowToPlayInfo[i].SetActive(true);
            }
            else
            {
                HowToPlayInfo[i].SetActive(false);
            }
        }
    }

    public void Next()
    {
        if (HowToPlayInfo[2].activeSelf)
        {
            next.SetActive(false);
        }
        HowToPlayInfo[num].SetActive(false);
        num++;
        HowToPlayInfo[num].SetActive(true);
        back.SetActive(true);
    }

    public void Back()
    {
        if (HowToPlayInfo[1].activeSelf)
        {
            back.SetActive(false);
        }
        HowToPlayInfo[num].SetActive(false);
        num--;
        HowToPlayInfo[num].SetActive(true);
        next.SetActive(true);
    }
}
