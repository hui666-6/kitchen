using UnityEngine;

[CreateAssetMenu(menuName = "Game/Level", fileName = "LevelSO")]
public class LevelSO : ScriptableObject
{
    [Header("基础信息")]
    public string levelName = "新手厨房";
    public bool isTutorial;

    [Header("关卡规则")]
    public float timeLimit = 60f;
    public float orderRate = 2f;
    public int orderMax = 5;
    public recipelistSO recipes;

    [Header("星级评价（按成功送餐数）")]
    public int star1Score = 3;
    public int star2Score = 6;
    public int star3Score = 10;
}
