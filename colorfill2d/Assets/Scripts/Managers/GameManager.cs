using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GridManager gridManager;

    public bool isLevelDone = false;
    void Awake()
    {
        Instance = this;
        isLevelDone = false;
        gridManager = FindObjectOfType<GridManager>();
    }

    public void LevelDone()
    {
        if (GameObject.FindGameObjectsWithTag("Green").Length >= (gridManager.xSize * gridManager.ySize))
        {
            isLevelDone = true;
        }
    }
}
