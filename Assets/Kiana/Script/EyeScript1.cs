//控制眼睛帧动画，隔几秒换一帧
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript1 : MonoBehaviour
{
 
    public Renderer rend;
    private float EyeTime=0;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        EyeTime+=Time.fixedDeltaTime;

      
        if (EyeTime >= 4) EyeTime -= 4;
        if (EyeTime <= 3.89) rend.material.mainTextureOffset = new Vector2(0, 0);
        else if (EyeTime <= 3.91) rend.material.mainTextureOffset = new Vector2(0.25f, 0);
        else if (EyeTime <= 3.94) rend.material.mainTextureOffset = new Vector2(0.5f, 0);
        else if (EyeTime <= 3.96) rend.material.mainTextureOffset = new Vector2(0.75f, 0);
        else if (EyeTime <= 3.99) rend.material.mainTextureOffset = new Vector2(0.5f, 0);
        else if (EyeTime <= 4) rend.material.mainTextureOffset = new Vector2(0.25f, 0);
        
    }

}
