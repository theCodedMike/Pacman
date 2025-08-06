using System;


public enum GridType
{
    Normal, //正常类型
    Obstacle, //障碍物类型
    Start,  //起点类型
    End  //终点类型
}

public class Grid : IComparable<Grid>
{
    //格子坐标x-y
    public int x;
    public int y;
    //格子A*三属性f-g-h
    public int f;
    public int g;
    public int h;
    //格子类型
    public GridType type;
    //格子的归属（父格子）
    public Grid parent;


    public Grid(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    
    
    public int CompareTo(Grid other)
    {
        if (f < other.f)
            return -1;
        if (f > other.f)
            return 1;
        return 0;
    }
}
