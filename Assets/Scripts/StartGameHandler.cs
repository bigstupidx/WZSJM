using UnityEngine;
using System.Collections;

//Author:Luna
public class StartGameHandler : MonoBehaviour {

    public GameController controller;
	void Start () {
	
	}
	
	void Update () {
	
	}

    public void OnMouseDown()
    {
        controller.StartGame();
    
    }
}
