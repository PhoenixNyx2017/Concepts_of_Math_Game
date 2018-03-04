using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameController : MonoBehaviour
{
    
    float time = 0;
    bool checking;
    int scriptIndex = 0;
    public Sprite background;
    public GameObject cont;
    playerController playerCont;
    ArrayList script = new ArrayList();
    public GameObject bottom;
    public GameObject top;
    int place;
    public GameObject [] cloudArr;
    public GameObject blue;
    public Canvas battle;
    public Image mon;
    public Image ball;
    bool battleMode = false;
    // Use this for initialization
    void Start()
    {
        playerCont = cont.GetComponent<playerController>();
        playerCont.mainMode();
        battle.enabled = false;
        //playerCont.talking = true;
        script.Add("What the- where am I? What is this?");
        script.Add("First I’m failing my classes, then... something and now I’m in a giant field ? ");
        script.Add("Great, just great.I have a concepts test to study for!I don’t have time for this!");
        script.Add("Sigh… I guess I have no choice but to figure out where I am.");


    }

    // Update is called once per frame
    void Update()
    {
        if (battleMode)
        {
            battleScript();
        }
    if (place == 0)
    { 
        timeUpdate(true);
        if (time < 3)
        {
            place = 0;
            top.GetComponent<Transform>().Translate(new Vector2(0, 1) * 3f * Time.deltaTime);
            bottom.GetComponent<Transform>().Translate(new Vector2(0, -1) * 3f * Time.deltaTime);
        }
        else
        {
            playerCont.talkMode(true);
                place = 1;
            playerCont.setText((string)script[scriptIndex]);
            scriptIndex++;
            time = 0;
        }
    }
        if (place == 1 || place == 0)
        {
           
            for (int i = 0; i < cloudArr.Length; i++)
            {
                cloudArr[i].GetComponent<Transform>().Translate(new Vector2(1, 0) * (Random.Range(0, 3f) * Time.deltaTime));
                if (cloudArr[i].GetComponent<Transform>().position.x >= 15)
                {
                    float y = cloudArr[i].GetComponent<Transform>().position.y;
                    float z = cloudArr[i].GetComponent<Transform>().position.z;
                    cloudArr[i].GetComponent<Transform>().position = new Vector3(-15, y, z);
                }
            }
        }
        if (place == 1)
        { 
            if ( Input.GetKeyUp(KeyCode.Return))
            {
                if (scriptIndex < script.Count)
                {
                    playerCont.talkMode(true);
                    playerCont.setText((string)script[scriptIndex]);
                    //mainText.text = (string)script[scriptIndex];
                    scriptIndex += 1;
                }
                else
                {
                    playerCont.moveMode = true;
                    place = 2;
                    for (int i=0; i<cloudArr.Length;i++)
                    {
                        Destroy(cloudArr[i],0);
                        Destroy(bottom);
                        Destroy(top);
                        Destroy(blue);
                    }
                    
                    playerCont.mainMode();
                    scriptIndex = 0;
                    checking = true;
                }
            }
        }
        if (place == 2)
        {
            timeUpdate(true);
            if (time > 10)
            {
                battleMode = true;  
            }
        }

   }
        

    void timeUpdate(bool check)
    {
        if (check)
        {
            time += Time.deltaTime;
        }
    }
    void battleScript()
    {
        place = 3;
        battle.enabled = true;
        Vector2 pos=mon.GetComponent<RectTransform>().position;
        float x = pos.x;
        float y = pos.y;
        mon.GetComponent<RectTransform>().Translate(new Vector2(1, 0) * (Random.Range(0, 3f) * Time.deltaTime));
        int dir = Random.Range(0, 3);
        switch (dir)
        {
            case 1: ball.GetComponent<RectTransform>().Translate(new Vector2(1, 0) * (Random.Range(1, 10f) * Time.deltaTime));
                break;
            case 2: ball.GetComponent<RectTransform>().Translate(new Vector2(-1, 0) * (Random.Range(1, 10f) * Time.deltaTime));
                break;
            case 3: ball.GetComponent<RectTransform>().Translate(new Vector2(0,1) * (Random.Range(1, 10f) * Time.deltaTime));
                break;
            case 4: ball.GetComponent<RectTransform>().Translate(new Vector2(0, -1) * (Random.Range(1, 10f) * Time.deltaTime));
                break;

        }
    }
    public void mainMode()
    {
        battleMode = false;
        Debug.Log("Got here");
        battle.enabled = false;
        playerCont.mainMode();
    }
}