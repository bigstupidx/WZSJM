using UnityEngine;
using System.Collections;
/*
 * Description: PotClicker
 * Author:      JiangShu
 * Create Time: 2015/7/10 16:01:33
 */
public class PotClicker : MonoBehaviour
{
    #region Properties
    public GameController controller;
    #endregion Properties

    #region Functions
    public void OnMouseDown()
    {
        controller.ClickPot(GetComponent<Item>());
    }
    #endregion Functions

}
