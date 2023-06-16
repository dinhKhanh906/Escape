
using UnityEngine;
using UnityEngine.UI;

public class HDRTest: MonoBehaviour
{
    [ColorUsage(true, true)] public Color colorTest;
    [SerializeField] KeyCode increaseIntensity;
    [SerializeField] KeyCode reduceIntensity;
    [SerializeField] KeyCode setIntensity;

    public Image imagePreviewColor;
    private void Start()
    {
        imagePreviewColor.color = colorTest;
    }
    private void Update()
    {
        if (Input.GetKeyDown(increaseIntensity))
        {
            HDRColor.IncreaseIntensity(ref colorTest, 1f);
            imagePreviewColor.color = colorTest;

            Debug.Log("increase intensity");
        }
        if (Input.GetKeyDown(reduceIntensity))
        {
            HDRColor.IncreaseIntensity(ref colorTest, -1f);
            imagePreviewColor.color = colorTest;

            Debug.Log("reduce intensity");
        }
        if(Input.GetKeyDown(setIntensity))
        {
            HDRColor.SetIntensity(ref colorTest, 2f);
            imagePreviewColor.color = colorTest;
        }
    }
}