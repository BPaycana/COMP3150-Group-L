using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraSize : MonoBehaviour
{

    public SpriteRenderer ScreenSizeHelper;

    // Start is called before the first frame update
    void Start()
    {

        float orthoSize = ((ScreenSizeHelper.bounds.size.x * Screen.height) / Screen.width) * 0.5f;
        Camera.main.orthographicSize = Mathf.Abs(orthoSize);

    }

}
