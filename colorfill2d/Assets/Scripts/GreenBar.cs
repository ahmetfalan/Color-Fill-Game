using UnityEngine;
using UnityEngine.UI;

public class GreenBar : MonoBehaviour
{
    public Slider slider;
    GridManager gridManager;
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    void Start()
    {
        slider.maxValue = gridManager.xSize * gridManager.ySize;
    }

    void Update()
    {
        slider.value = GameObject.FindGameObjectsWithTag("Green").Length;
    }
}
