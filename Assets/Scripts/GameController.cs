using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Description: GameController
 * Author:      JiangShu
 * Create Time: 2015/7/10 15:49:00
 */
public class GameController : MonoBehaviour {

    public enum GameState
    {
        START,
        GAME,
        GAME_OVER
    }
    public GameState state = GameState.START;

    public GameObject pot1;
    public GameObject pot2;
    public GameObject cat;
    public Item catItem;

    public byte rowNum = 9;//行数
    public byte columnNum = 9;//列数

    public GameObject startUI;
    public GameObject victoryUI;
    public GameObject defeatUI;
    public GameObject replayBtn;

    public Animator catAnimator;
    public RuntimeAnimatorController stayAnima;
    public RuntimeAnimatorController happyAnima;

    private bool[,] data;
    public List<GameObject> pot2List;
    void Start()
    {
        data = new bool[rowNum,columnNum];
        pot2List = new List<GameObject>();

        cat.SetActive(false);

        startUI.SetActive(true);
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        replayBtn.SetActive(false);
    }
    public void StartGame()
    {
        for(byte y = 0;y<rowNum;y++)
        {
            for(byte x = 0;x<columnNum;x++)
            {
                GameObject pot = CreatePot(pot1, x, y);
                pot.GetComponent<PotClicker>().controller = this;
            }
        }
        cat.SetActive(true);
        MoveCat((byte)Random.Range(3,columnNum - 3), (byte)Random.Range(3,rowNum - 3));

        startUI.SetActive(false);

        state = GameState.GAME;
    }
    public void Replay()
    {
        victoryUI.SetActive(false);
        defeatUI.SetActive(false);
        replayBtn.SetActive(false);

        data = new bool[rowNum, columnNum];

        foreach (GameObject pt2 in pot2List)
            Destroy(pt2);
        pot2List.Clear();


        catAnimator.runtimeAnimatorController = stayAnima;
        MoveCat((byte)Random.Range(3,columnNum - 3), (byte)Random.Range(3,rowNum - 3));

        state = GameState.GAME;
    }
    public void MoveCat(byte x,byte y)
    {
        catItem.SetPosition(x, y);
    }
    public void ClickPot(Item item)
    {
        if (state != GameState.GAME || data[item.x, item.y]  )
            return;
        //set pot2
        GameObject pot = CreatePot(pot2, item.x, item.y);
        pot2List.Add(pot);
        data[item.x, item.y] = true;
        //move cat
        Vector2[] steps = FindSteps();
        if (steps.Length > 0)
        {
            int idx = Random.Range(0, steps.Length);
            MoveCat((byte)steps[idx].x,(byte)steps[idx].y);

            if(escaped(catItem.x,catItem.y))
            {
                Defeat();
            }
            else
            {
                //移动之后继续判断
                steps = FindSteps();
                if (steps.Length == 0)
                    Victory();
            }
        }
        else
        {
            Victory();
        }
    }
    public void Victory()
    {
        victoryUI.SetActive(true);
        replayBtn.SetActive(true);
        catAnimator.runtimeAnimatorController = happyAnima;
        state = GameState.GAME_OVER;
    }
    public void Defeat()
    {
        defeatUI.SetActive(true);
        replayBtn.SetActive(true);
        catAnimator.runtimeAnimatorController = happyAnima;
        state = GameState.GAME_OVER;
    }
    private Vector2[] FindSteps()
    {
        List<Vector2> steps = new List<Vector2>();
        //上
        if (movable(catItem.x, (byte)(catItem.y + 1))) steps.Add(new Vector2(catItem.x, catItem.y + 1));
        //下
        if (movable(catItem.x, (byte)(catItem.y - 1))) steps.Add(new Vector2(catItem.x, catItem.y - 1));
        //左
        if (movable((byte)(catItem.x - 1), (byte)(catItem.y))) steps.Add(new Vector2(catItem.x - 1, catItem.y));
        //右
        if (movable((byte)(catItem.x + 1), (byte)(catItem.y))) steps.Add(new Vector2(catItem.x + 1, catItem.y));
        //奇数 左上 左下
        if(catItem.y % 2 != 0)
        {
            if (movable((byte)(catItem.x - 1), (byte)(catItem.y + 1))) steps.Add(new Vector2(catItem.x - 1, catItem.y + 1));
            if (movable((byte)(catItem.x - 1), (byte)(catItem.y - 1))) steps.Add(new Vector2(catItem.x - 1, catItem.y - 1));
        }
        else
        //偶数 右上 右下
        {
            if (movable((byte)(catItem.x + 1), (byte)(catItem.y + 1))) steps.Add(new Vector2(catItem.x + 1, catItem.y + 1));
            if (movable((byte)(catItem.x + 1), (byte)(catItem.y - 1))) steps.Add(new Vector2(catItem.x + 1, catItem.y - 1));
        }
        return steps.ToArray();
    }
    private bool movable(byte x,byte y)
    {
        if (x < 0 || x >= columnNum || y < 0 || y >= rowNum)
            return false;

        return !data[x, y];
    }
    private bool escaped(byte x,byte y)
    {
        return x == 0 || x == columnNum - 1 || y == 0 || y == rowNum - 1;
    }

    private GameObject CreatePot(GameObject potfab,byte x,byte y)
    {
        GameObject pot = Instantiate(potfab) as GameObject;
        Item item = pot.GetComponent<Item>();
        pot.transform.parent = transform;
        item.SetPosition(x, y);
        return pot;
    }
}
