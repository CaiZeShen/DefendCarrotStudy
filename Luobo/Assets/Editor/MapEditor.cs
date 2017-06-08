using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System;

// ****************************************************************
// 功能：地图编辑器扩展 (对xml文件的操作)
// 创建：蔡泽深
// 时间：2017/06/02
// 修改内容：										修改者姓名：
// ****************************************************************

[CustomEditor(typeof(Map))]
public class MapEditor : Editor {
    public Map map;                                         // 关联的mono脚本组件
    private List<FileInfo> files = new List<FileInfo>();    // 关卡列表
    private int selectIndex = -1;                           // 当前编辑的关卡索引号

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        if (Application.isPlaying) {
            map = target as Map;

            // 第一行
            EditorGUILayout.BeginHorizontal();
            int currentIndex = EditorGUILayout.Popup(selectIndex, GetFileNames(files));
            if (currentIndex != selectIndex) {
                selectIndex = currentIndex;
                LoadLevel();
            }
            if (GUILayout.Button("读取列表")) {
                LoadLevelFiles();
            }
            EditorGUILayout.EndHorizontal();

            // 第二行
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("清除放塔点")) {
                map.ClearHolder();
            }
            if (GUILayout.Button("清除路径")) {
                map.ClearRoad();
            }
            EditorGUILayout.EndHorizontal();

            // 第三行
            if (GUILayout.Button("保存数据")) {
                SaveLevel();
            }
        }

        if (GUI.changed) {
            // 刷新
            EditorUtility.SetDirty(target);
        }
    }

    private string[] GetFileNames(List<FileInfo> files) {
        string[] fileNames = new string[files.Count];

        for (int i = 0; i < files.Count; i++) {
            fileNames[i] = files[i].Name;
        }

        return fileNames;
    }

    // 读取关卡列表
    private void LoadLevelFiles() {
        // 清除状态
        Clear();

        // 加载列表
        files = Tools.GetLevelFiles();

        // 默认加载第一个关卡
        if (files.Count>0) {
            selectIndex = 0;
            LoadLevel();
        }
    }

    // 保存关卡数据
    private void SaveLevel() {
        // 获取当前加载的关卡
        Level level = map.Level;

        List<Point> tempList;
        // 收集放塔点
        tempList = new List<Point>();
        foreach (Tile t in map.Grids) {
            if (t.canHold) {
                Point point = new Point(t.x, t.y);
                tempList.Add(point);
            }
        }
        level.holders = tempList;

        // 收集寻路点
        tempList = new List<Point>();
        foreach (Tile t in map.Road) {
            Point point = new Point(t.x, t.y);
            tempList.Add(point);
        }
        level.roadPoints = tempList;

        // 路径
        string fileFullName = files[selectIndex].FullName;

        // 保存关卡
        Tools.SaveLevle3(fileFullName, level);

        // 弹框提示
        EditorUtility.DisplayDialog("保存关卡", "保存成功", "确定");
    }

    // 加载关卡
    private void LoadLevel() {
        FileInfo fileInfo = files[selectIndex];
        Level level = new Level();
        Tools.FillLevel(fileInfo.FullName, ref level);

        map.LoadLevel(level);
    }

    // 清除状态
    private void Clear() {
        files.Clear();
        selectIndex = -1;
    }
}

