using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//экран загрузки
public class LoadingBar : MonoBehaviour
{
    public int SceneID;
    public Scrollbar sbar;
    public Text progressText;
    private void Start()
    {
        StartCoroutine(AsyncLoad());
    }
    //загрузка сцены
    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(SceneID);
        while (!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            sbar.size = progress;
            progressText.text = string.Format("{0:0}%", progress * 100);
            yield return null;
        }
    }
}
