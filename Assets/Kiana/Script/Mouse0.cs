using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

using System.IO;
public class Mouse0 : MonoBehaviour
{
    public int thiAngle = 0;
    public int rotateSpeed = 2;
    public bool openRotate = false;
    Animator animator;
    GameObject go;
    public bool g;
  


    // Use this for initialization
    
    void Start()
    {
       
        animator =GetComponentInChildren <Animator>();
        go = GameObject.FindWithTag("ALL");
        g = false ;
        

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
           animator.SetTrigger("Jump");
            openRotate = true;
            StartCoroutine(Delay());


        }
 

        if (openRotate)
        {
            PointerRotate();
                }
        
    }
    IEnumerator Delay()

    {

        yield return new WaitForSeconds(2);
        
        if (!Input.GetMouseButtonUp(0)) {
            print("ture");
            g = true;
        }
           
            

    }


    /// <summary>
    /// 控制指针旋转
    /// </summary>
    private void PointerRotate()
    {
        if (thiAngle > -0.001f && thiAngle <= 180)
        {
            Quaternion target = Quaternion.Euler(0, (90 - thiAngle), 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed);
            if (transform.rotation == Quaternion.RotateTowards(transform.rotation, target, rotateSpeed)) openRotate = false;
        }
    }


    }



