using UnityEngine;

/// <summary>
/// 关卡管理器：每个关卡场景放一个，通过 levelType 声明当前关卡类型。
/// 目前只有一个场景，作为新手教程关（Tutorial）。
/// 后续新增关卡 = 新建场景 + 一个 LevelManager，设置不同的 levelType 和菜单列表即可。
/// </summary>
public class LevelManager : MonoBehaviour
{
    public enum LevelType
    {
        Tutorial,   // 新手教程关
        Normal      // 普通关卡
    }

    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelType levelType = LevelType.Tutorial;
    [Tooltip("本关卡可生成的菜单列表；每个关卡可配置不同的菜单池")]
    [SerializeField] private recipelistSO recipeList;

    private void Awake()
    {
        Instance = this;
    }

    public LevelType CurrentLevel => levelType;

    public bool IsTutorialLevel()
    {
        return levelType == LevelType.Tutorial;
    }

    /// <summary>本关卡的菜单列表（可能为空，调用方需自行兜底）。</summary>
    public recipelistSO RecipeList => recipeList;
}
