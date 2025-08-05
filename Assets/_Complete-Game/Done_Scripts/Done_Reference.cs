using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Done_Reference : MonoBehaviour
{
	//颜色材质区分
	public Material startMat;
	public Material endMat;
	public Material obstacleMat;
	//显示信息Text
	private Text text;
	//当前格子坐标
	public int x;
	public int y;

	void Awake ()
	{
		//text = GameObject.Find ("Text").GetComponent<Text> ();
	}
	//判断当前格子的类型
	void OnTriggerEnter2D (Collider2D other)
	{
        if (other.name == "Done_Blinky") {
			GetComponent<MeshRenderer> ().material = startMat;
			Done_MyAStar.instance.grids [x, y].type = GridType.Start;
			Done_MyAStar.instance.openList.Add (Done_MyAStar.instance.grids [x, y]);
			Done_MyAStar.instance.startX = x;
			Done_MyAStar.instance.startY = y;
        } else if (other.name == "Done_Pacman") {
			GetComponent<MeshRenderer> ().material = endMat;
			Done_MyAStar.instance.grids [x, y].type = GridType.End;
			Done_MyAStar.instance.targetX = x;
			Done_MyAStar.instance.targetY = y;
        } else if (other.name == "Done_Obstacle") {
			GetComponent<MeshRenderer> ().material = obstacleMat;
			Done_MyAStar.instance.grids [x, y].type = GridType.Obstacle;
        }

    }

	/// <summary>
	/// 鼠标点击显示当前格子基础信息
	/// </summary>
	void OnMouseDown ()
	{
		text.text = "XY(" + x + "," + y + ")" + "\n" +
		"FGH(" + Done_MyAStar.instance.grids [x, y].f + "," +
		Done_MyAStar.instance.grids [x, y].g + "," +
		Done_MyAStar.instance.grids [x, y].h + ")";
		text.color = GetComponent<MeshRenderer> ().material.color;
	}
}