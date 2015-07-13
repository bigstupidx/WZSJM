using UnityEngine;
using System.Collections;
/*
 * Description: Item
 * Author:      JiangShu
 * Create Time: 2015/7/10 15:49:00
 */
public class Item : MonoBehaviour
{
    #region Properties
    public byte x;
    public byte y;

    public float offX = -2.25f;
    public float offY = -3f;
    #endregion Properties

    #region Functions
    public void SetPosition(byte x,byte y)
    {
        this.x = x;
        this.y = y;
        UpdatePosition();
    }
    public void UpdatePosition()
    {
        float drawX = (float)(offX + x * 0.5);
        float drawY = (float)(offY + y * 0.5);
        if (y % 2 == 0)
            drawX += 0.25f;
        transform.position = new Vector3(drawX,drawY, 0);
    }
    #endregion Functions
}
