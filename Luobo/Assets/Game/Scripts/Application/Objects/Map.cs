using UnityEngine;
using System.Collections.Generic;
using System;

// ****************************************************************
// 功能：用于描述一个关卡地图的状态
// 创建：蔡泽深
// 时间：2017/06/01
// 修改内容：										修改者姓名：
// ****************************************************************

public class Map : MonoBehaviour {
    public const int RowCount = 8;                      // 行数
    public const int ColumnCount = 12;                  // 列数

    public event EventHandler<TileClickEvnetArgs> tileClickEvent;
    public bool drawGizmos = true;                      // 是否绘制网格
    private float mapWidth;                             // 地图宽(米)
    private float mapHeight;                            // 地图高(米)
    private float tileWidth;                            // 格子宽(米)
    private float tileHeight;                           // 格子高(米)
    private Vector3 origin;                             // 地图原点坐标
    private List<Tile> grids = new List<Tile>();        // 格子集合
    private List<Tile> road = new List<Tile>();         // 路径集合
    private Level level;                                // 关卡数据

    #region 属性
    public string BackgroundImage {
        set {
            SpriteRenderer renderer = transform.Find("Background").GetComponent<SpriteRenderer>();
            renderer.sprite = ResourcesMgr.Instance.Load<Sprite>(Consts.MapResDir + value);
        }
    }

    public string RoadImage {
        set {
            SpriteRenderer renderer = transform.Find("Road").GetComponent<SpriteRenderer>();
            renderer.sprite = ResourcesMgr.Instance.Load<Sprite>(Consts.MapResDir + value);
        }
    }

    public List<Tile> Grids {
        get { return grids; }
    }

    public List<Tile> Road {
        get { return road; }
    }

    public Level Level {
        get { return level; }
    }

    // 怪物寻路路径
    public Vector3[] Path {
        get {
            Vector3[] path = new Vector3[road.Count];

            for (int i = 0; i < road.Count; i++) {
                path[i] = GetPosition(road[i]);
            }

            return path;
        }
    }

    public Rect MapRect {
        get { return new Rect(-mapWidth / 2, -mapHeight / 2, mapWidth, mapHeight); }
    }

    #endregion

    #region Unity 回调
    private void Awake() {
        CalculateSize();

        // 创建所有的格子
        for (int row = 0; row < RowCount; row++) {
            for (int col = 0; col < ColumnCount; col++) {
                Tile tile = new Tile(col, row);
                grids.Add(tile);
            }
        }
    }

    private void OnEnable() {
        tileClickEvent += OnTileClick;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Tile t = GetTileUnderMouse();
            if (tileClickEvent != null) {
                tileClickEvent(this, new TileClickEvnetArgs(0, t));
            }
        }

        if (Input.GetMouseButtonDown(1)) {
            Tile t = GetTileUnderMouse();
            if (tileClickEvent != null) {
                tileClickEvent(this, new TileClickEvnetArgs(1, t));
            }
        }
    }

    private void OnDrawGizmos() {
        if (!Application.isPlaying) {
            return;
        }

        if (!drawGizmos) {
            return;
        }

        // 绘制格子
        Gizmos.color = Color.green;

        // 绘制行
        for (int row = 0; row <= RowCount; row++) {
            Vector2 from = new Vector2(origin.x, origin.y + tileHeight * row);
            Vector2 to = new Vector2(origin.x + mapWidth, origin.y + tileHeight * row);
            Gizmos.DrawLine(from, to);
        }

        // 绘制列
        for (int col = 0; col <= ColumnCount; col++) {
            Vector2 from = new Vector2(origin.x + tileWidth * col, origin.y);
            Vector2 to = new Vector2(origin.x + tileWidth * col, origin.y + mapHeight);
            Gizmos.DrawLine(from, to);
        }

        // 绘制放塔点
        foreach (Tile t in grids) {
            if (t.canHold) {
                Vector3 pos = GetPosition(t);
                Gizmos.DrawIcon(pos, "holder.png", false);
            }
        }

        // 绘制路径
        Gizmos.color = Color.red;
        for (int i = 0; i < road.Count; i++) {
            Vector3 pos = GetPosition(road[i]);

            // 绘制起点
            if (i == 0) {
                Gizmos.DrawIcon(pos, "start.png", false);
            }

            // 绘制终点
            if (road.Count > 1 && i == road.Count - 1) {
                Gizmos.DrawIcon(pos, "end.png", false);
            }

            // 绘制路径
            if (i > 0) {
                Vector2 from = GetPosition(road[i - 1]);
                Vector2 to = pos;
                Gizmos.DrawLine(from, to);
            }
        }
    }
    #endregion

    #region 事件回调
    private void OnTileClick(object sender, TileClickEvnetArgs e) {
        if (gameObject.scene.name !=Consts.LevelBuilder) {
            return;
        }

        if (!drawGizmos) {
            return;
        }

        if (level == null) {
            return;
        }

        if (e.mouseButton == 0 && !road.Contains(e.tile)) {
            // 处理放塔点
            e.tile.canHold = !e.tile.canHold;
        } else if (e.mouseButton == 1 && !e.tile.canHold) {
            // 处理路径点
            if (road.Contains(e.tile)) {
                road.Remove(e.tile);
            } else {
                road.Add(e.tile);
            }
        }
    }
    #endregion

    #region 方法
    // 加载关卡信息,填充当前类
    public void LoadLevel(Level level) {
        // 清空当前状态
        Clear();

        // 保存关卡信息
        this.level = level;

        // 加载图片
        BackgroundImage = level.background;
        RoadImage = level.road;

        // 寻路路径
        foreach (Point point in level.roadPoints) {
            Tile tile = GetTile(point.x, point.y);
            road.Add(tile);
        }

        // 炮塔空地
        foreach (Point point in level.holders) {
            Tile tile = GetTile(point.x, point.y);
            tile.canHold = true;
        }
    }

    // 清除塔位信息
    public void ClearHolder() {
        foreach (Tile t in grids) {
            if (t.canHold) {
                t.canHold = false;
            }
        }
    }

    // 清除寻路信息
    public void ClearRoad() {
        road.Clear();
    }

    // 清除所有信息
    public void Clear() {
        level = null;
        ClearHolder();
        ClearRoad();
    }

    // 获取位置通过Tile
    public Vector3 GetPosition(Tile tile) {
        return new Vector3(
            origin.x + (tile.x + 0.5f) * tileWidth,
            origin.y + (tile.y + 0.5f) * tileHeight,
            0
        );
    }

    public Tile GetTile(Vector3 worldPos) {
        // 获取相对原地的左边，也就是转换没有负数的坐标
        Vector3 relativePos = worldPos - origin;

        int tileX = (int)(relativePos.x / tileWidth);
        int tileY = (int)(relativePos.y / tileHeight);

        return GetTile(tileX, tileY);
    }
    #endregion

    #region 私有方法
    private bool IsClickOnMap() {
        // 射线检测
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo, 100f)) {
            Debug.Log(hitinfo.transform.tag);
            if (hitinfo.transform.tag.Equals("Map")) {
                return true;
            }
        }
        
        return false;
    }

    // 计算地图大小，格子大小
    private void CalculateSize() {
        Vector3 leftDown = new Vector3(0, 0);
        Vector3 rightUp = new Vector3(1, 1);

        Vector3 p1 = Camera.main.ViewportToWorldPoint(leftDown);
        Vector3 p2 = Camera.main.ViewportToWorldPoint(rightUp);

        mapWidth = p2.x - p1.x;
        mapHeight = p2.y - p1.y;

        tileWidth = mapWidth / ColumnCount;
        tileHeight = mapHeight / RowCount;

        // 获取原点
        origin = new Vector3(-mapWidth / 2, -mapHeight / 2);
    }

    // 获取鼠标下面的格子
    private Tile GetTileUnderMouse() {
        Vector3 worldPos = GetWorldPosOnMouse();
        return GetTile(worldPos);
    }

    // 根据格子索引号获得格子
    private Tile GetTile(int tileX, int tileY) {
        int index = tileX + tileY * ColumnCount;

        if (index < 0 || index >= grids.Count) {
            Debug.LogWarning("获取格子越界");
            return null;
        }

        return grids[index];
    }

    // 获取鼠标所在位置的世界坐标
    private Vector3 GetWorldPosOnMouse() {
        Vector3 viewPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        //return Camera.main.ScreenToWorldPoint(Input.mousePosition); // 听讲课老师说有问题，未试
        return worldPos;
    }
    #endregion
}

