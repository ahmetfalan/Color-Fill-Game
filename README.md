# Color Fill Game

This repository created for case study of Maestro Game Studios.
WebGL Build: https://play.unity.com/mg/other/webgl-builds-121696

The case: Color Fill 3D Game on [Play Store](https://play.google.com/store/apps/details?id=com.gjg.colorfill3d&hl=tr&gl=US) or [App Store](https://apps.apple.com/us/app/color-fill-3d/id1473024868)

## Work Progress
Firstly, I researched how to convert 0s surrounded by 1s to 1s in a two-dimensional array of 0s and 1s. In short, I did a research on how to fill a closed area. The algorithm used for this is the Flood Fill Algorithm. I used a recursive Flood Fill Algorithm in this project.

This is my Flood method:
```c#
public IEnumerator Flood(int x, int y, Color oldColor, Color newColor) // I was define it IEnumerator because the delay and delay for optimization.
{
    WaitForSeconds wait = new WaitForSeconds(fillDelay);
    if (x >= 0 && x < gridManager.xSize && y >= 0 && y < gridManager.ySize) //If in borders and there is the grid
    {
        yield return wait; // Delay
        if (gridManager.grid[x, y].GetComponent<SpriteRenderer>().color == oldColor) // If current sprite equals the old color
        {
            gridManager.grid[x, y].GetComponent<SpriteRenderer>().color = newColor; // Turn it color green
            gridManager.grid[x, y].tag = "Green"; // 
            gridManager.grid[x, y].layer = 8; //
            StartCoroutine(Flood(x + 1, y, oldColor, newColor)); //Do the right sprite
            StartCoroutine(Flood(x - 1, y, oldColor, newColor)); //Do the left sprite
            StartCoroutine(Flood(x, y + 1, oldColor, newColor)); //Do the up sprite
            StartCoroutine(Flood(x, y - 1, oldColor, newColor)); //Do the down sprite
            GameManager.Instance.LevelDone(); //Check every sprite is green.
        }
    }
}
```



### Flood Fill Algorithm

From [Wikipedia](https://en.wikipedia.org/wiki/Flood_fill),

Flood fill, also called seed fill, is an algorithm that determines and alters the area connected to a given node in a multi-dimensional array with some matching attribute. It is used in the "bucket" fill tool of paint programs to fill connected, similarly-colored areas with a different color, and in games such as Go and Minesweeper for determining which pieces are cleared. A variant called boundary fill uses the same algorithms but is defined as the area connected to a given node that does not have a particular attribute.

![FloodFill](https://github.com/ahmetfalan/color-fill-game/blob/main/imgs/Recursive_Flood_Fill_4_(aka).gif)
