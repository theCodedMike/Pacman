using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Reference : MonoBehaviour
{
    public Material startMat;
    public Material endMat;
    public Material obstacleMat;

    public int x; // 当前格子坐标
    public int y;
    
    private Text info;
    private MeshRenderer mr;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (mr == null)
            mr = GetComponent<MeshRenderer>();
        
        if (other.name == "Blinky")
        {
            mr.material = startMat;
            Grid currGrid = AStar.instance.grids[x, y];
            currGrid.type = GridType.Start;
            AStar.instance.openList.Add(currGrid);
            AStar.instance.startX = x;
            AStar.instance.startY = y;
        } else if (other.name == "Pacman")
        {
            mr.material = endMat;
            AStar.instance.grids[x, y].type = GridType.End;
            AStar.instance.targetX = x;
            AStar.instance.targetY = y;
        } else if (other.name == "Obstacle")
        {
            mr.material = obstacleMat;
            AStar.instance.grids[x, y].type = GridType.Obstacle;
        }
    }

    // 鼠标点击显示当前格子基础信息
    private void OnMouseDown()
    {
        Grid grid = AStar.instance.grids[x, y];
        info.text = $"XY({x},{y})\nFGH({grid.f},{grid.g},{grid.h})";
        info.color = mr.material.color;
    }
}
