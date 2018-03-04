using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerController : MonoBehaviour
{
    public int health;
    float speed = 5;
    string text1;
    public Text bottom;
    public Canvas black;
    public bool talking;
    Animator animControl;
    public GameObject background;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    bool upDown = false;
    public bool moveMode = false;
    private int layermask = (~0) ^ (1 << 10);
    // Use this for initialization
    void Start()
    {
        //mainMode();
        animControl = GetComponent<Animator>();
        animControl.speed = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveMode)
        inputMovement();
    }

    void inputMovement()
    {
        RaycastHit2D [] hitArr=rayCast();
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (!talking)
            {
                for (int i = 0; i < hitArr.Length; i++)
                {
                    if (hitArr[i].collider !=null && hitArr[i].collider.gameObject.tag == "Int")
                    {
                        talkMode(false);
                        returnText(hitArr[i].collider.gameObject.name);
                    }
                }
            }
        else mainMode();
        }
        if (!talking)
        {
            Vector2 direction = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (!down)
                {
                    upDown = true;
                    animControl.speed = 1f;
                    animControl.Play("downMove");
                    direction.y -= 1;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {

                if (!up)
                {
                    upDown = true;
                    animControl.speed = 1f;
                    animControl.Play("upMove");
                    direction.y += 1;
                }
            }
            else
            {
                upDown = false;
                animControl.speed = 0f;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (!right)
                {
                    animControl.speed = 1f;
                    if (!upDown)
                        animControl.Play("rightMove");
                    direction.x += 1;
                }
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (!left)
                {
                    animControl.speed = 1f;
                    if (!upDown)
                        animControl.Play("leftMove");
                    direction.x -= 1;
                }
            }
            else
            {
                if (!upDown)
                    animControl.speed = 0f;
            }
            direction = direction.normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
    void returnText(string name)
    {
        switch (name)
        {
            case "Tabasco": setText("Those are real spicy.");
                break;
            case "cilantro": setText("It's a giant pile of cilantro, yum.");
                break;
            case "bell pepper": setText("What a sweet bell pepper");
                break;

        }

    }
    /*void OnTriggerStay2D(Collider2D other)
    {   
        if (Input.GetKeyUp(KeyCode.Return))
        {
            Debug.Log("Return");
            if (!talking)
            {
                talking = true;
                animControl.speed = 0f;
                //bottom.text = returnText(other.gameObject.tag);
                black.enabled = true;
            }
            else
            {
                mainMode();
               
            }
        }
    }*/

    RaycastHit2D [] rayCast()
    {
        RaycastHit2D[] hitArr = new RaycastHit2D[12]; 
       
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.8f,layermask);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position, new Vector2(1f, -1f), 1.8f*Mathf.Sqrt(2f),layermask);
        RaycastHit2D hit3 = Physics2D.Raycast(transform.position, new Vector2(0.5f, -1f), 1.8f*Mathf.Sqrt(1.25f),layermask);
        hitArr[0]=hit;
        hitArr[1] = hit2;
        hitArr[2] = hit3;
        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            Debug.Log("down");
            down = true;
        }
        else down = false;
        hit = Physics2D.Raycast(transform.position, Vector2.up, .0001f,layermask);
        hit2 = Physics2D.Raycast(transform.position, new Vector2(0.5f, 1f), Mathf.Sqrt(.00001f),layermask);
        hit3 = Physics2D.Raycast(transform.position, new Vector2(1f, 1f), Mathf.Sqrt(.00001f),layermask);
        hitArr[3] = hit;
        hitArr[4] = hit2;
        hitArr[5] = hit3;
        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            up = true;
             Debug.Log("up");
        }
        else up = false;
        hit = Physics2D.Raycast(transform.position, Vector2.right, 1.5f,layermask);
        hit2 = Physics2D.Raycast(transform.position, new Vector2(1f, -1f), 1.5f*Mathf.Sqrt(2f),layermask);
        hit3 = Physics2D.Raycast(transform.position, new Vector2(1f, -.5f), 1.5f*Mathf.Sqrt(1.25f),layermask);
        hitArr[6] = hit;
        hitArr[7] = hit2;
        hitArr[8] = hit3;

        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {

            Debug.Log("right");
            right = true;
        }
        else right = false;
        hit = Physics2D.Raycast(transform.position, Vector2.left, .0001f,layermask);
        hit2 = Physics2D.Raycast(transform.position, new Vector2(1f, -1f), Mathf.Sqrt(0.1f),layermask);
        hit3 = Physics2D.Raycast(transform.position, new Vector2(1f, -.5f), Mathf.Sqrt(.1f),layermask);
        hitArr[9] = hit;
        hitArr[10] = hit2;
        hitArr[11] = hit3;
        if ((hit.collider != null) || (hit2.collider != null) || (hit3.collider != null))
        {
            left = true;
            Debug.Log("Left");
        }
        else left = false;
        return hitArr;
    }
    public void mainMode()
    {
        talking = false;
        bottom.text = "";
        black.enabled = false;
    }
    public void talkMode(bool pict)
    {
        talking = true;
        black.enabled = true;
    }
    public void setText(string t)
    {
        bottom.text = t;
    }
    /*void OnCollisionStay2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag);
        //bottom.text = returnText(other.gameObject.tag);
        if (Input.GetKey(KeyCode.Return))
        {
            talking = true;
            animControl.speed = 0f;
            black.enabled = true;
        }
      
    }*/
}
