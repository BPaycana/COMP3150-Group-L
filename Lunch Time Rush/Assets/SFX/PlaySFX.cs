using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySFX : MonoBehaviour
{

    public AudioSource Pew;
    public AudioSource Splat;


    public void PlayPew()
    {
        Pew.Play();

    }

    public void PlaySplat()
    {
        Splat.Play();

    }


}
