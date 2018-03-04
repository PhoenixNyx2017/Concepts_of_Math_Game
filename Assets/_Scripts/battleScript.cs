using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleScript : MonoBehaviour {
    int health = 5;
    string thing= "true";
    public GameObject playerControl;
    playerController playerCont;
    public GameObject gameControl;
    gameController gameCont;
    public Text proof;
    // Use this for initialization
    void Start () {

        playerCont = playerControl.GetComponent<playerController>();
        gameCont = gameControl.GetComponent<gameController>();
        proof.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void clicked(string p)
    {
        if (p == thing)
        {
            playerCont.mainMode();
            gameCont.mainMode();
        }
        else health -= 1;
        if (health <= 2)
        {
            playerCont.talkMode(true);
        }
    }
    public void ballClick()
    {
        Debug.Log("in");
        proof.enabled = true;
        proof.text = "something";

    }

    void speaking()
    {

    }
}
