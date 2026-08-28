using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [Header("关卡列表（可选，优先使用）")]
    [SerializeField] private LevelSO[] levels;

    [Header("无关卡配置时的默认新手关参数")]
    [SerializeField] private float defaultTimeLimit = 120f;
    [SerializeField] private float defaultOrderRate = 6f;
    [SerializeField] private int defaultOrderMax = 2;

    private LevelSO currentLevel;
    private LevelSO[] loadedLevels;

    public int CurrentIndex { get; private set; }

    public bool IsTutorialLevel
    {
        get { return CurrentLevel != null && CurrentLevel.isTutorial; }
    }

    public LevelSO CurrentLevel
    {
        get
        {
            if (currentLevel == null) ResolveCurrentLevel();
            return currentLevel;
        }
    }

    private const string UNLOCK_KEY = "UnlockedLevel";
    private const string STAR_PREFIX = "LevelStars_";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        CurrentIndex = Loader.LevelIndex;
    }

    private void ResolveCurrentLevel()
    {
        LevelSO level = null;

        if (levels != null && CurrentIndex >= 0 && CurrentIndex < levels.Length)
        {
            level = levels[CurrentIndex];
        }

        if (level == null)
        {
            loadedLevels = Resources.LoadAll<LevelSO>("Levels");
            if (loadedLevels != null && loadedLevels.Length > 0)
            {
                Array.Sort(loadedLevels, (a, b) => string.Compare(a.name, b.name, StringComparison.Ordinal));
                if (CurrentIndex >= 0 && CurrentIndex < loadedLevels.Length)
                {
                    level = loadedLevels[CurrentIndex];
                }
            }
        }

        if (level == null)
        {
            level = CreateDefaultTutorialLevel();
        }

        currentLevel = level;
    }

    private LevelSO CreateDefaultTutorialLevel()
    {
        LevelSO level = ScriptableObject.CreateInstance<LevelSO>();
        level.levelName = "1-1 新手厨房";
        level.isTutorial = true;
        level.timeLimit = defaultTimeLimit;
        level.orderRate = defaultOrderRate;
        level.orderMax = defaultOrderMax;
        level.star1Score = 1;
        level.star2Score = 3;
        level.star3Score = 5;
        return level;
    }

    public float GetTimeLimit() { return CurrentLevel.timeLimit; }
    public float GetOrderRate() { return CurrentLevel.orderRate; }
    public int GetOrderMax() { return CurrentLevel.orderMax; }

    public recipelistSO GetRecipeList()
    {
        recipelistSO list = CurrentLevel.recipes;
        if (list == null && OrderManager.Instance != null)
        {
            list = OrderManager.Instance.GetRecipeList();
        }
        return list;
    }

    public int GetLevelCount()
    {
        if (levels != null && levels.Length > 0) return levels.Length;
        if (loadedLevels != null && loadedLevels.Length > 0) return loadedLevels.Length;
        return 1;
    }

    public bool IsLastLevel()
    {
        return CurrentIndex >= GetLevelCount() - 1;
    }

    public int CalculateStar(int deliveryCount)
    {
        if (deliveryCount >= CurrentLevel.star3Score) return 3;
        if (deliveryCount >= CurrentLevel.star2Score) return 2;
        if (deliveryCount >= CurrentLevel.star1Score) return 1;
        return 0;
    }

    public int CompleteLevel(int deliveryCount)
    {
        int star = CalculateStar(deliveryCount);

        string key = STAR_PREFIX + CurrentIndex;
        if (PlayerPrefs.GetInt(key, 0) < star)
        {
            PlayerPrefs.SetInt(key, star);
        }

        int unlocked = PlayerPrefs.GetInt(UNLOCK_KEY, 1);
        if (!IsLastLevel() && CurrentIndex + 1 > unlocked)
        {
            PlayerPrefs.SetInt(UNLOCK_KEY, CurrentIndex + 1);
        }

        PlayerPrefs.Save();
        return star;
    }

    public static int GetStars(int index)
    {
        return PlayerPrefs.GetInt(STAR_PREFIX + index, 0);
    }

    public static int GetUnlockedCount()
    {
        return PlayerPrefs.GetInt(UNLOCK_KEY, 1);
    }
}
