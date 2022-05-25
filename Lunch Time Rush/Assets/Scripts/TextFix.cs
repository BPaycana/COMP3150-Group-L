using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFix : MonoBehaviour
{

    private Renderer textRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        textRenderer = gameObject.GetComponent<Renderer>();
        textRenderer.sortingOrder = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
