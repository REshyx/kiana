using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO;

public class Drag1 : MonoBehaviour
{
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    const int WM_SYSCOMMAND = 0x0112;
    const int SC_MOVE = 0xF010;
    const int HTCAPTION = 0x0002;
    IntPtr hwnd;
    bool  isdrag=false ;

    void Start()
    {
        hwnd = FindWindow (null,"Kiana");

    }

    void Update()
    {
       isdrag = GameObject.FindWithTag("Avatar").GetComponent<Mouse0>().g;
       if (isdrag) {
        ReleaseCapture();
        SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
            print("dragging");
            GameObject.FindWithTag("Avatar").GetComponent<Mouse0>().g = false;
        }
    }
  
    IEnumerator Delay()

    {

        yield return new WaitForSeconds(2);


        if (Input.GetMouseButtonDown(0)) isdrag = true;

 }
 
   

}