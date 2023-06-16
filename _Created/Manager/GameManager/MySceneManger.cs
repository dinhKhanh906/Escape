using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Threading.Tasks;
using System.Collections;

public class MySceneManger : MonoBehaviour
{
    public static MySceneManger instance;
    public GameObject loadingPrefab;

    private void Awake()
    {
        if(instance)
        {
            Debug.LogWarning("Has more than one MySceneManager instance");
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void LoadScene(int sceneIndex)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        this.LoadSceneAsync(sceneIndex, canvas);
    }
    public void LoadScene(int sceneIndex, Canvas canvas)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex, canvas));
    }
    IEnumerator LoadSceneAsync(int sceneIndex, Canvas canvas)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        GameObject loadingBar = Instantiate(loadingPrefab, canvas.transform);
        LoadingController loadingController = loadingBar.GetComponent<LoadingController>();

        while(operation.progress <= 0.95f)
        {
            loadingController.Progress = operation.progress;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

    }
}
