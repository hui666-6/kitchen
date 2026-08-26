using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu] //右键菜单中，添加一个创建该scriptableobject实例的选项
public class KitchenObjectSO :ScriptableObject//数据容器 主要用于存储可复用的游戏数据 不附着场景中游戏对象
{
    public GameObject prefab;
    public Sprite Sprite;  //存储该物品的图标 可用与ui显示
    public string objectName;
}
