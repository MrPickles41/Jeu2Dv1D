using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Movements : MonoBehaviour
{
    public Transform player;
    public Sprite invisible;
    public GameObject background;
    public GameObject sky;
    public GameObject deathMessage;
    public GameObject restartMessage;
    public bool isgrounded = true;
    public bool jumping = false;
    public bool death = false;
    public float increase;
    private bool pause = false;
    private float fixedDeltaTime;
    public GameObject bird;
    public bool birdfly = false;
   

    //timer
    public TextMeshProUGUI timerText;
    public float currentTime;
    public bool countUp;


    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("s"))
        {
            if (pause == false)
            {
                pause = true;
            }
            else
                pause = false;
                
        }
        

        //delta time = 0 to stop physics
        if (Input.GetKeyDown("s"))
        {
            if (Time.timeScale == 1.0f)
                Time.timeScale = 0;
            else
                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }

        //background movement + timer
        if (death == false && pause == false)
        {

            background.transform.Translate(new Vector3(Convert.ToSingle(-4 * Time.deltaTime - increase), 0, 0)); //ground mov
            sky.transform.Translate(new Vector3(-1.5f * Time.deltaTime - increase, 0, 0)); //sky mov
            //background linear increase
            increase += Time.deltaTime * 0.0001f;


            if (background.transform.localPosition.x < -4227)
            {
                background.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (sky.transform.localPosition.x < -1423)
            {
                sky.transform.localPosition = new Vector3(2804, 469, 0);
            }

            //no jumping in air
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(player.transform.position.x - 0.39f, player.transform.position.y - 1), Vector2.down, 0.01f);
            RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(player.transform.position.x + 0.25f, player.transform.position.y - 1), Vector2.down, 0.01f);
            if ((hit.collider != null) || (hit2.collider != null))
            {
                isgrounded = true;
                jumping = false;
            }
            else
            {
                isgrounded = false;
                jumping = true;
            }

            //horse controls
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isgrounded == true)
                {
                    player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                }
            }
            if (Input.GetKey("d"))
            {
                player.Translate(new Vector3(0.25f * Time.deltaTime, 0, 0));
            }
            if (Input.GetKey("a"))
            {
                player.Translate(new Vector3(-0.5f * Time.deltaTime, 0, 0));
            }
            
            
            //timer
            currentTime = countUp ? currentTime -= Time.deltaTime : currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("0.000" + "s");


            //                                                                                                            not sure if works here
            if (currentTime >= 10)
            {
                birdfly = true;
            //elseif(currentTime < 15)
            
                    birdfly = false;
                
            }

            if (birdfly == true)
            {
                if (currentTime >= 10)
                {
                    if (bird.transform.localPosition.x < -75)
                    {
                        bird.transform.Translate(new Vector3(-8 * Time.deltaTime, 5 * Time.deltaTime, 0));
                    }
                    else
                        bird.transform.Translate(new Vector3(-10 * Time.deltaTime, 0, 0));

                }
            }











        }

        //player death
        if (player.transform.localPosition.x < -530)
        {
            death = true;
            background.transform.Translate(new Vector3(0, 0, 0)); //background stop
            sky.transform.Translate(new Vector3(0, 0, 0));        //sky stop
            player.GetComponent<Image>().sprite = invisible;      //horse invis
        }
        
        //restart button
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(sceneBuildIndex: 0);
        }
        if (death == true) //death message
        {
            if (deathMessage.transform.localPosition.y < 47)
            {
                deathMessage.transform.Translate(new Vector3(0, 0, 0));
            }
            else
                deathMessage.transform.Translate(new Vector3(0, -4 * Time.deltaTime, 0));

            if (restartMessage.transform.localPosition.y > 5)
            {
                restartMessage.transform.Translate(new Vector3(0, 0, 0));
            }
            else
                restartMessage.transform.Translate(new Vector3(0, 4.5f * Time.deltaTime, 0));
        }

        

    }
}