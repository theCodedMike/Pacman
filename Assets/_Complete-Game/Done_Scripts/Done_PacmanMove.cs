using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 炸弹人的移动脚本
/// </summary>
public class Done_PacmanMove : MonoBehaviour {

    //炸弹的移动速度
    public float speed = 0.4f;

    Vector2 dest = Vector2.zero;


    void Start()
    {
        //获取当前位置
        dest = transform.position;
    }

    /// <summary>
    /// 实时获取用户输入的方向键，以改变运动方向
    /// </summary>
    void FixedUpdate()
    {

        //确定移动向量的方向和大小，将吃豆人移向目标点
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        
        //获取方向按键，并且判断吃豆人在该方向上可以移动
        if ((Vector2)transform.position == dest)
        {
            if (Input.GetKey(KeyCode.UpArrow) && valid(Vector2.up))
            {
                
                dest = (Vector2)transform.position + Vector2.up;
            }

            if (Input.GetKey(KeyCode.RightArrow) && valid(Vector2.right))
            {
               
                dest = (Vector2)transform.position + Vector2.right;
            }
            if (Input.GetKey(KeyCode.DownArrow) && valid(-Vector2.up))
            {
                dest = (Vector2)transform.position - Vector2.up;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && valid(-Vector2.right))
            {
                Debug.Log("left");
                dest = (Vector2)transform.position - Vector2.right;
            }

        }

        //获取相应的动画
        Vector2 dir = dest - (Vector2)transform.position;
        GetComponent<Animator>().SetFloat("DirX", dir.x);
        GetComponent<Animator>().SetFloat("DirY", dir.y);
    }


    /// <summary>
    /// 检测吃豆人前方是否有障碍物
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    bool valid(Vector2 dir)
    {
        //RaycastHit（Vector2 origin（起始点）, Vector2 direction（方向向量）） 投射一段光线并击打
        //这里只是简单的从距离吃豆人一单位的点(pos + dir)发射到吃豆人自身(pos)。
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        if (hit.transform.gameObject.name == "Done_Obstacle")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}