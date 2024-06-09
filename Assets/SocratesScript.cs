using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SocratesScript : MonoBehaviour
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Image theGreatBlack;

    [SerializeField]
    private Image TextImage;
    [SerializeField]
    private Image smollSocrates;

    [SerializeField]
    private TextMeshProUGUI SocratesWisdom;


    [SerializeField]
    private AudioSource sfx;
    [SerializeField]
    private AudioSource music;
    [SerializeField]
    private AudioSource sfx2;

    [SerializeField]
    private GameObject BIGSocrates;
    [SerializeField]
    private GameObject BuffedSocratesSpeaks;
    [SerializeField]
    private GameObject YouWereHere;
    
    private bool playerOutOfBounds = false;
    private bool theGreatBlackVisible = false;

    private bool socratesSharingWisdom = false;
    private bool buffedTime = false;

    private float timer;

    private bool played = false;

    void Update()
    {
        if (player.transform.position.x >= 10 || player.transform.position.x <= -10 || player.transform.position.y <= -6 || player.transform.position.y >= 6)
        {
            music.volume = 0f;
            playerOutOfBounds = true;
            canvas.sortingOrder = 1;
            if (PlayerPrefs.GetInt("WasOutOfBounds") == 1)
            {
                canvas.sortingOrder = 1;
                YouWereHere.SetActive(true);
                timer += Time.deltaTime;
                if (timer > 5)
                {
                    Application.Quit();
                }
            }
        }



        if (playerOutOfBounds && !theGreatBlackVisible && PlayerPrefs.GetInt("WasOutOfBounds") != 1)
        {
            if (timer <= 20)
            {
                timer += Time.deltaTime * 10;
                theGreatBlack.color = new Color(theGreatBlack.color.r, theGreatBlack.color.g, theGreatBlack.color.b, timer);
            }else
            {
                theGreatBlack.color = new Color(theGreatBlack.color.r, theGreatBlack.color.g, theGreatBlack.color.b, 255);
                timer = 0;
                theGreatBlackVisible = true;
            }
        }

        if (theGreatBlackVisible && !socratesSharingWisdom)
        {
            if (timer <= 5)
            {
                timer += Time.deltaTime * 5;
                TextImage.color = new Color(TextImage.color.r, TextImage.color.g, TextImage.color.b, timer);
                smollSocrates.color = new Color(smollSocrates.color.r, smollSocrates.color.g, smollSocrates.color.b, timer);
            }
            else
            {
                timer += Time.deltaTime;
                if (timer >= 5 && timer <= 10)
                {
                    TextImage.color = new Color(TextImage.color.r, TextImage.color.g, TextImage.color.b, 255);
                    smollSocrates.color = new Color(smollSocrates.color.r, smollSocrates.color.g, smollSocrates.color.b, 255);
                    SocratesWisdom.text = "you poor fool ... you comited a sin ...";
                }
                else if(timer >= 10f && timer <= 16f)
                {
                    SocratesWisdom.text = "You went out of bounds into this endless void ";
                }
                else if(timer >= 16f && timer <= 21f)
                {
                    SocratesWisdom.text = "Seems like I have to educate you a little";
                }
                else
                {
                    socratesSharingWisdom = true;
                    timer = 3;
                }
            }
        }

        if (socratesSharingWisdom && !buffedTime)
        {
            if (timer <= 3 && !buffedTime && timer>= -1)
            {
                timer -= Time.deltaTime * 5;
                TextImage.color = new Color(TextImage.color.r, TextImage.color.g, TextImage.color.b, timer);
                smollSocrates.color = new Color(smollSocrates.color.r, smollSocrates.color.g, smollSocrates.color.b, timer);
                SocratesWisdom.text = "";
                if (!played)
                {
                    sfx.Play();
                    played = true;
                }
            }
            else
            {
                buffedTime = true;
                timer = 3;
                played = false;
            }

        }
        if (buffedTime)
        {
            if (!sfx.isPlaying && !played)
            {
                if (!played)
                {
                    BIGSocrates.SetActive(true);
                    sfx2.Play();
                    played = true;
                }
            }
            else if (!sfx2.isPlaying && !sfx.isPlaying)
            {
                BIGSocrates.SetActive(false);
                BuffedSocratesSpeaks.SetActive(true);
                TextImage.color = new Color(TextImage.color.r, TextImage.color.g, TextImage.color.b, 255);
                timer -= Time.deltaTime*5;
                theGreatBlack.color = new Color(theGreatBlack.color.r, theGreatBlack.color.g, theGreatBlack.color.b, timer);
                if(timer <= 3 && timer>= -28)
                {
                    SocratesWisdom.text = "After all I know that you know nothing";
                }
                else if (timer <= -28 && timer >= -48)
                {
                    SocratesWisdom.text = "You can simply press restart and you can finish the level as intended";
                }
                else if (timer <= -48 && timer >= -68)
                {
                    SocratesWisdom.text = "Don't do it again";
                }
                else if (timer <= -69 )
                {
                    PlayerPrefs.SetInt("WasOutOfBounds", 1);
                    SceneManager.LoadScene("Level1");
                }

                
            }
            
        }


    }
}
