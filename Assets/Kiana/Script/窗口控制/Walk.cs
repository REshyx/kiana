//走路过程中控制窗体移动实现视觉上走动
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.IO;

public class Walk : MonoBehaviour
{
    public float angle=0;
    // Start is called before the first frame update
  

    #region Win函数常量
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);

    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    private const int SWP_SHOWWINDOW = 0x0040;

    #endregion
 
    public int speed;

    float ti ;
    Rect rect = new Rect();
    int timx=0,timxy;
    int tim;

   IntPtr hwnd = FindWindow(null, "Kiana");
    GameObject rb;
    Animator anm;
    private void OnEnable()
    {
        tim = 0;
        rb = GameObject.FindWithTag ("route") ;
    }

    private void Start()
    {
        anm = GetComponent<Animator >();
    }


    void FixedUpdate()
    {  GetWindowRect(hwnd, out rect);
       angle = rb.transform.eulerAngles.y ;
       // angle = rb.transform.eulerAngles.y;
        float sin = Mathf.Sin(angle* (Mathf.PI)/180);
       timx += 1;

        if (rect.Left >= -120 && rect.Left <= 1740 && rect.Top >= -200 && rect.Bottom <= 1080) {

            if (timx % 3 == 0)
            { tim++; 

                timxy = (int)(tim * sin);
                //print(angle+"---walkingandmovinga--"+ sin);
                //SetWindowPos(hwnd, -1, rect.Left - (int)(5 * sin), rect.Top, 300, 300, SWP_SHOWWINDOW);
                SetWindowPos(hwnd, -1, rect.Left - (int)(5 * sin), rect.Top, 300, 300, SWP_SHOWWINDOW);
            }

        } else if(rect.Left<300){
            SetWindowPos(hwnd, -1, -100, rect.Top, 300, 300, SWP_SHOWWINDOW);
            if ((UnityEngine.Random.Range(0, 3)) == 1)  anm.SetTrigger("Up");
            else if((UnityEngine.Random.Range(0, 3)) == 2)  anm.SetTrigger("Down");
            return;

        } 
       else if (rect.Left > 1600) {
            SetWindowPos(hwnd, -1, 1720, rect.Top, 300, 300, SWP_SHOWWINDOW);
            if ((UnityEngine.Random.Range(0, 3)) == 1) anm.SetTrigger("Up");
            else if((UnityEngine.Random.Range(0, 3)) == 2)  anm.SetTrigger("Down");
            return;
        } 

      
    }
  


}




