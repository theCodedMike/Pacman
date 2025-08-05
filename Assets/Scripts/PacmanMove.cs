using UnityEngine;

public class PacmanMove : MonoBehaviour
{
    private static readonly int DirX = Animator.StringToHash("DirX");
    private static readonly int DirY = Animator.StringToHash("DirY");

    [Header("移动速度")]
    public float speed;
    
    
    private Vector2 dest = Vector2.zero;
    private Rigidbody2D rb;
    private Collider2D cd;
    private Animator anim;
    
    
    private void Start()
    {
        dest = transform.position;
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // 检测吃豆人前方是否有障碍物
    private bool ValidMove(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return hit.collider == cd;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = Vector2.MoveTowards(transform.position, dest, speed);
        rb.MovePosition(newPos);
        
        if (Mathf.Approximately(((Vector2)transform.position - dest).SqrMagnitude(), 0))
        {
            if (Input.GetKey(KeyCode.UpArrow) && ValidMove(Vector2.up))
            {
                dest = (Vector2)transform.position + Vector2.up;
            }
            if (Input.GetKey(KeyCode.DownArrow) && ValidMove(Vector2.down))
            {
                dest = (Vector2)transform.position + Vector2.down;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && ValidMove(Vector2.left))
            {
                dest = (Vector2)transform.position + Vector2.left;
            }
            if (Input.GetKey(KeyCode.RightArrow) && ValidMove(Vector2.right))
            {
                dest = (Vector2)transform.position + Vector2.right;
            }
        }

        Vector2 dir = dest - (Vector2)transform.position;
        anim.SetFloat(DirX, dir.x);
        anim.SetFloat(DirY, dir.y);
    }
}
