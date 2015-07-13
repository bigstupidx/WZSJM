using UnityEngine;
using System.Collections;
/*
 * Description: ReplayHandler
 * Author:      JiangShu
 * Create Time: 2015/7/13 15:36:05
 */
public class ReplayHandler : MonoBehaviour
{
    #region Properties
    public GameController controller;
    #endregion Properties

    #region Functions
    void Awake()
    {
    }
    void Start()
    {
    }
    void Update()
    {
    }
    public void OnMouseUp()
    {
        controller.Replay();
    }
    #endregion Functions
}
