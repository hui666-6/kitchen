using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader

{

    public enum scene
    { 
      GameMenu,
      Loading,
      GameScene
    }
    private static scene targetscene;
    public static int LevelIndex { get; private set; }
    public static void load(scene target)
    { 
        Time.timeScale = 1;
       targetscene = target;
       ClearGameStaticData();
       SceneManager.LoadScene((int)scene.Loading);
    
    }
    public static void LoadLevel(int levelIndex)
    {
        LevelIndex = levelIndex;
        load(scene.GameScene);
    }
    public static void LoadBack()
    {
        SceneManager.LoadScene((int)targetscene);
    }
    private static void ClearGameStaticData()
    {
        TrashCounter.ClearStaticData();
        KitchenObjectHolder.ClearStaticData();
        CuttingCounter.clearStaticData();
        StoveCounter.clearStaticData();
    }
}
