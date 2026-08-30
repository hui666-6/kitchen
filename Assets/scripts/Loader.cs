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
    public static void load(scene target)
    { 
        Time.timeScale = 1;
       targetscene = target;
       SceneManager.LoadScene((int)scene.Loading);
    
    }
    public static void LoadBack()
    {
        SceneManager.LoadScene((int)targetscene);
    }
}