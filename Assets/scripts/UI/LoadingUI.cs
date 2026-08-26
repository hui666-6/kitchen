using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingUI : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI dot;
    private float dotRate = 0.3f;
    private void Start()
    {
        StartCoroutine(DotAnimation());
      
    }
    IEnumerator DotAnimation()
    {
        while (true)
        {
            dot.text = ".";
            yield return new WaitForSeconds(dotRate);
            dot.text = "..";
            yield return new WaitForSeconds(dotRate);
            dot.text = "...";
            yield return new WaitForSeconds(dotRate);
            dot.text = "....";
            yield return new WaitForSeconds(dotRate);
            dot.text = ".....";
            yield return new WaitForSeconds(dotRate);
            dot.text = "......";
            yield return new WaitForSeconds(dotRate);

        }

    }

}
