using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collider_Test : MonoBehaviour
{

    // Start is called before the first frame update
    private void OnMouseDown()       //鼠标按下
    {
        Destroy(this.gameObject);
    }
    private void OnMouseEnter()       //鼠标移入物体
    {
        this.transform.localScale = new Vector3(40f, 40f, 40f);
    }
    private void OnMouseOver()     //鼠标悬停时每帧调用
    {
        this.transform.Rotate(new Vector3(10, 10, 10) * Time.deltaTime);
       
    }
    private void OnMouseExit()         //鼠标移出
    {
        this.transform.localScale = new Vector3(50, 50, 50);
    }
}

