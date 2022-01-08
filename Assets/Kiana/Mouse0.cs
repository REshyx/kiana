using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
using System.IO;
using UnityEngine.UI;
using System.Threading;

public class Mouse0 : MonoBehaviour
{
    public float thiAngle = 0;
    public float det = 0;
    public float gpsx = 0;
    public float gpsy = 0;
    public int state = 0;
    public float tem = 0;
    public float rate = 0;
    public int rotateSpeed = 2;
    public bool openRotate = false;
    public bool canstop = true;
    Animator animator;
    GameObject go;
    public bool g;
    static String connetStr = "server=124.71.137.39;port=443;user=admin;password=shyx; database=trace;";
    static string sql2 = "select * from user where id='123'";
    static MySqlConnection conn = new MySqlConnection(connetStr);
    static MySqlCommand cmd = new MySqlCommand(sql2, conn);
    GameObject changeFormbtn;
    Text text;
    ThreadStart childRef;
    Thread childThread;


    // Use this for initialization
    private void Awake()
    {

    }

    void Start()
    {
        childRef = new ThreadStart(ThreadTest1);
        //childThread = new Thread(childRef);
        //Text text = new Text();
        changeFormbtn = GameObject.FindGameObjectWithTag("text1");
        text = changeFormbtn.GetComponent<Text>();
        animator = GetComponentInChildren<Animator>();
        go = GameObject.FindWithTag("route");
        g = false;
        childRef = new ThreadStart(ThreadTest1);
        childThread = new Thread(childRef);
        childThread.Start();
        /*try
        {
            conn.Open();
            print("已经建立连接");
            MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
            if (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
            {
                print("haveread");
                this.gpsx = reader.GetFloat("gpsx");
                this.gpsy = reader.GetFloat("gpsy");
                this.tem = reader.GetFloat("temperature");
                this.state = reader.GetInt32("state");
                this.rate = reader.GetInt32("heartreat");
                print("—x—" + reader.GetFloat("gpsx") + "—y—" + reader.GetFloat("gpsy") + "\n—tem—" + reader.GetFloat("temperature") + "\n—rate—" + reader.GetInt32("state") + "\n—sta—" + reader.GetInt32("heartreat"));
                //print(reader.GetFloat("gpsx") + "—y—"+reader.GetFloat("gpsy") + "—tem—" + reader.GetFloat("temperature") + "—rate—" + reader.GetUInt32("heartreat"));//"userid"是数据库对应的列名，推荐这种方式
                //text.text = reader.GetFloat("gpsx") + "—y—" + reader.GetFloat("gpsy") + "—tem—" + reader.GetFloat("temperature") + "—rate—" + reader.GetInt64("heartreat");
            }
        }
        catch (MySqlException ex)
        {
            print(ex.Message);
        }
        finally
        {
            conn.Close();
            // Thread.ResetAbort();
        }*/


    }

    private void ThreadTest1()
    {
        canstop = false;
        {
           // print("建立连接。。。");
            try
            {
                conn.Open();
                print("已经建立连接");
                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                if (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    print("haveread");
                    this.gpsx = reader.GetFloat("gpsx");
                    this.gpsy = reader.GetFloat("gpsy");
                    this.tem = reader.GetFloat("temperature");
                    this.state = reader.GetInt32("state");
                    this.rate = reader.GetInt32("heartreat");
                    print("—x—" + reader.GetFloat("gpsx") + "—y—" + reader.GetFloat("gpsy") + "\n—tem—" + reader.GetFloat("temperature") + "\n—rate—" + reader.GetInt32("state") + "\n—sta—" + reader.GetInt32("heartreat"));
                    //print(reader.GetFloat("gpsx") + "—y—"+reader.GetFloat("gpsy") + "—tem—" + reader.GetFloat("temperature") + "—rate—" + reader.GetUInt32("heartreat"));//"userid"是数据库对应的列名，推荐这种方式
                    //text.text = reader.GetFloat("gpsx") + "—y—" + reader.GetFloat("gpsy") + "—tem—" + reader.GetFloat("temperature") + "—rate—" + reader.GetInt64("heartreat");
                }
            }
            catch (MySqlException ex)
            {
                print(ex.Message);
            }
            finally
            {
                conn.Close();
                // Thread.ResetAbort();
            }
        }
        canstop = true;
    }

    // Update is called once per frame
    void Update()
    {

        //print("-"+changeFormbtn);
        if (this.state == 1) text.text = "\n状态：<color=#F28282>异常</color>\n体温：" + this.tem.ToString("f2") + "\n心率" + this.rate;
        else text.text = "\n状态：正常\n体温：" + this.tem.ToString("f2") + "\n心率" + this.rate;
        thiAngle = go.transform.eulerAngles.y;
        det = transform.eulerAngles.y - thiAngle;
        if (Input.GetMouseButtonDown(0))
        {
            if (canstop)
            {
                childThread.Abort();
                print("threadis__" + childThread.IsAlive);
                childRef = new ThreadStart(ThreadTest1);
                childThread = new Thread(childRef);
                childThread.Start();
                print("threadis__" + childThread.IsAlive);
            }
            else print("ing。。。");
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

        if (!Input.GetMouseButtonUp(0))
        {
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
        {//(90 - thiAngle)
         //transform.rotation.
            Quaternion target = Quaternion.Euler(0, det, 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed);

            if (transform.rotation == Quaternion.RotateTowards(transform.rotation, target, rotateSpeed)) openRotate = false;
            //transform.LookRotation(new Vector3(1, 1, 1))
            /*transform.rotation = Quaternion.Euler(0.0f, det, 0.0f);
            if (transform.rotation == Quaternion.Euler(0.0f, det, 0.0f)) openRotate = false;*/
        }
        else
        {
            Quaternion target = Quaternion.Euler(0, (360 - Math.Abs(det)), 0);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotateSpeed);

            if (transform.rotation == Quaternion.RotateTowards(transform.rotation, target, rotateSpeed)) openRotate = false;
            //transform.LookRotation(new Vector3(1, 1, 1))
        }
    }


}



