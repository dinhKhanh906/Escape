using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiteController : MonoBehaviour
{
    public GameObject[] objectsInSite;

    private void Start()
    {
        DeactiveObjectsInSite();
    }
    public void ActiveObjectsInSite()
    {
        if (objectsInSite.Length <= 0) return;

        foreach(GameObject obj in objectsInSite)
        {
            obj.SetActive(true);
        }
    }
    public void DeactiveObjectsInSite()
    {
        if (objectsInSite.Length <= 0) return;

        foreach (GameObject obj in objectsInSite)
        {
            obj.SetActive(false);
        }
    }
}
