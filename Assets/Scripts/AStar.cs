using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar : MonoBehaviour
{
    public static AStar instance;

    [Header("参考物体")]
    public GameObject referencePrefab;
    public Grid[,] grids; // 格子数组
    public GameObject[,] objs; // 格子数组对应的参考物对象
    public List<Grid> openList; // 开启列表
    public List<Grid> closeList; // 关闭列表
    public int targetX; // 目标点坐标
    public int targetY;
    public int startX; // 起始点坐标
    public int startY;
    private int row; // 行
    private int col; // 列
    private Stack<string> parentList; // 结果栈
    private Transform plane;
    private Transform start;
    private Transform end;
    private Transform obstacle;
    private float alpha = 0;
    private float incremnetPer = 0;
    private float cdTime = 1;
    private float cdsTime;
    private GhostMove ghost;

    private void Awake()
    {
        instance = this;
        parentList = new();
        openList = new();
        closeList = new();
    }

    private void Start()
    {
        Init();
        ghost = FindFirstObjectByType<GhostMove>();
    }

    private void Update()
    {
        cdsTime += Time.deltaTime;
        if (cdsTime > cdTime)
        {
            GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");
            foreach (var cube in cubes)
            {
                Destroy(cube);
            }
            parentList.Clear();
            openList.Clear();
            closeList.Clear();
            Init();
            StartCoroutine(Count());
            StartCoroutine(ShowResult());
            cdsTime = 0;
        }
    }

    private void Init()
    {
        int x = 28;
        int y = 31;
        row = x;
        col = y;
        grids = new Grid[x, y];
        objs = new GameObject[x, y];
        Vector3 startPos = new Vector3(-11, -14, 0); // 起始坐标
        // 生成参考物体(Cube)
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                grids[i, j] = new Grid(i, j);
                GameObject item = Instantiate(referencePrefab, new Vector3(i + 12f, j + 15f, -10) + startPos, Quaternion.identity);
                Reference firstRef = item.transform.GetChild(0).GetComponent<Reference>();
                firstRef.x = i;
                firstRef.y = j;
                objs[i, j] = item;
            }
        }
    }

    // A*计算
    private IEnumerator Count()
    {
        yield return new WaitForSeconds(0.1f);
        openList.Add(grids[startX, startY]);
        Grid currGrid = openList[0];
        // 循环遍历路径最小F的点
        while (openList.Count > 0 && currGrid.type != GridType.End)
        {
            // 获取此时最小F点
            currGrid = openList[0];
            // 如果当前点就是目标
            if(currGrid.type == GridType.End)
                GenerateResult(currGrid);

            // 上下左右、左上左下、右上右下，遍历
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if(i == 0 && j == 0)
                        continue;
                    
                    int x = currGrid.x + i;
                    int y = currGrid.y + j;
                    if (x >= 0 && y >= 0 && x < row && y < col && grids[x, y].type != GridType.Obstacle &&
                        !closeList.Contains(grids[x, y]))
                    {
                        // 计算g值
                        int g = currGrid.g + (int)(Mathf.Sqrt(Mathf.Abs(i) + Mathf.Abs(j)) * 10);
                        // 与原g值对照
                        if (grids[x, y].g == 0 || grids[x, y].g > g)
                        {
                            grids[x, y].g = g;
                            grids[x, y].parent = currGrid;
                        }
                        // 计算h值
                        grids[x, y].h = Manhattan(x, y);
                        // 计算f值
                        grids[x, y].f = grids[x, y].g + grids[x, y].h;
                        // 如果未添加则添加
                        if(!openList.Contains(grids[x, y]))
                            openList.Add(grids[x, y]);
                        
                        // 重新排序
                        openList.Sort();
                    }
                }
            }
            
            // 遍历完后添加到关闭列表
            closeList.Add(currGrid);
            openList.Remove(currGrid);
            if(openList.Count == 0)
                print("Can not find path...");
        }
    }

    private void GenerateResult(Grid currGrid)
    {
        // 如果当前格子有父格子
        if (currGrid.parent != null)
        {
            // 添加到父对象栈（即结果栈）
            parentList.Push($"{currGrid.x}|{currGrid.y}");
            // 递归获取
            GenerateResult(currGrid.parent);
        }
    }

    private IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.3f);
        // 计算每帧颜色值增量
        incremnetPer = 1 / (float)parentList.Count;
        while (parentList.Count != 0)
        {
            string str = parentList.Pop();
            yield return new WaitForSeconds(0.3f);
            string[] xy = str.Split('|');
            int x = int.Parse(xy[0]);
            int y = int.Parse(xy[1]);

            alpha += incremnetPer;
            ghost.Move(x, y);
            // 以颜色方式绘制路径
            objs[x, y].transform.GetChild(0).GetComponent<MeshRenderer>().material.color =
                new Color(1 - alpha, alpha, 0, 1);
        }
    }

    // 曼哈顿方式计算H值
    private int Manhattan(int x, int y)
        => (Mathf.Abs(targetX - x) + Mathf.Abs(targetY - y)) * 10;
}
