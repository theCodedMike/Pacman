using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class Done_GhostMove : MonoBehaviour {
    public float speed = 0.3f;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        

    }

    public void ghostMove(int x,int y)
    {
        //改幽灵位置
        transform.position = new Vector3(x+1.5f,y+1f,1);
        //Vector3 a = new Vector3(x, y, 1);
        //Vector3 p = Vector3.MoveTowards(transform.position,a,speed);
        //GetComponent<Rigidbody2D>().MovePosition(p);
    }
    private void OnTriggerEnter2D(Collider2D co)
    {
        if (co.name == "Done_Pacman")
        {
            EditorSceneManager.LoadScene("Done_game");
        }
    }
}
