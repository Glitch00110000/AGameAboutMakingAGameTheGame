using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiControllerScript : MonoBehaviour
{

    [SerializeField]
    private GameObject credits;
    private bool switc = true;
    [SerializeField]
    private bool purgeValues;

    public void Start()
    {
        if (purgeValues == true || PlayerPrefs.GetInt("FirstTime") != 1 )
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.SetFloat("MusicVolume", 0.282f);
            PlayerPrefs.SetFloat("OtherSoundVolume", 0.123f);
            PlayerPrefs.SetInt("WasOutOfBounds", 0);
        }
    }

    public void Credits()
    {
        switch (switc) 
        {
            case true:
                credits.SetActive(true);
                switc = false;
                break;
            case false:
                credits.SetActive(false);
                switc = true;
                break;
        }

    }

    public void Level1()
    {
        SceneManager.LoadScene("Level-4");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level-3");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Level1");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Level2");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
