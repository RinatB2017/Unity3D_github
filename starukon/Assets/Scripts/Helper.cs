using System;
using UnityEngine;

/* 
 * ======================================================================================================
 * THIS PART OF CODE COPIED FROM https://github.com/Newbilius/ColorLinesUnity BY DIMA "NEWBILIUS" MOISEEV
 * ======================================================================================================
 */

public class Helper : MonoBehaviour
{
    public static void Set2DCameraToObject(GameObject field)
    {
        var size1 = field.GetComponent<SpriteRenderer>().bounds.size.y
            * Screen.height
            / Screen.width * 0.5f * Camera.main.aspect; ;

        var size2 = field.GetComponent<SpriteRenderer>().bounds.size.x
            * Screen.height
            / Screen.width * 0.5f; ;

        Camera.main.orthographicSize = Math.Max(size1, size2);
    }
}
