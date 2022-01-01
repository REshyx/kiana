using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radom1 : MonoBehaviour
{

    public   bool openRotate = false;
    public bool OpenRandom=true;
    public int angler;
    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("Each",1,1);
    }
    private void Each()
    {
        OpenRandom = true ;
    }
    void Update()

    {

        if (OpenRandom) {;
            if (Random.Range(0, 100) > 60)
            {
                openRotate = true;
                //Debug.Log("sdiwudiuc");
                angler = Random.Range(0, 360);
                OpenRandom = false;

            }
            else { openRotate = false;OpenRandom = false; }
          
     }


        if (openRotate)
        {   PointerRotate();
            

        }

    }

    /// <summary>
    /// 控制指针旋转
    /// </summary>
    private void PointerRotate()
    {
       
            Quaternion target = Quaternion.Euler(0,angler , 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 10);
            if( transform.rotation == Quaternion.RotateTowards(transform.rotation, target, 10)) openRotate = false;
        

    }


   
    }

