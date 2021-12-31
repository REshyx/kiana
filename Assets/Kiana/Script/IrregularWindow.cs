//extern alias spl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing;
//using col=spl.System.Drawing.Color;
//using rec=spl.System.Drawing.Rectangle;
//using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.IO;

/// <summary>
/// /// use the screen shot image to cut the window.
/// </summary>



public class IrregularWindow : MonoBehaviour
{
    public Bitmap winTex2d = new Bitmap(@"E:\创意\148542515062415458.PNG");
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw );
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, IntPtr hRgn, bool bRedraw );
    [DllImport("Gdi32.dll")]
    static extern IntPtr CreateRectRgn(int a, int b, int c, int d);
    [DllImport("Gdi32.dll")]
    static extern int CombineRgn(
        IntPtr hrgnDest,      // handle to destination region
        IntPtr hrgnSrc1,      // handle to source region
        IntPtr hrgnSrc2,      // handle to source region
        int fnCombineMode   // region combining mode
    );
    IntPtr hwnd;
    void Start()
    {
         hwnd = FindWindow(null, "Kiana");
         updateWindow();
    }
    private void updateWindow()
    {
        
        IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);

      Debug.Log(" width: " + winTex2d.Width + " height: " + winTex2d.Height);
        Debug.Log(winTex2d.GetPixel(0, 0)) ; 
        Debug.Log(System.Drawing.Color.Transparent.ToArgb());
        for (int h = 1; h < winTex2d.Height ; ++h){
            int w = 0;
            
            do{
                // Debug.Log(w +"and"+ (winTex2d.Height - h));
                // if (winTex2d.GetPixel(0, 2882).ToArgb() == 16777215) Debug.Log("....................");
                while (w < winTex2d.Width && 
                    (winTex2d.GetPixel(w, winTex2d.Height - h).A == 0 )){
                   
                    ++w;
                   
                }
                int iLeftX = w;
                while (w < winTex2d.Width && (winTex2d.GetPixel(w, winTex2d.Height - h).A != 0))//|| tex2d.GetPixel(w, h) != Color.black
                {
                    ++w;
                    //Debug.Log(tex2d.GetPixel(w, h));
                
                }
                Debug.Log("ileftX: " + iLeftX + "w: " + w +"h:" +h);

                CombineRgn(hRgn, hRgn, CreateRectRgn(iLeftX, h, w, h + 1), 2);
            }//end do{}
            while (w < winTex2d.Width);
        }//end for(h);
        //SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        Debug.Log("setWindowRgn: " + SetWindowRgn(hwnd, hRgn, true));
    }//end updateWindow();
}