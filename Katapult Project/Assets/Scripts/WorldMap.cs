using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Doozy.Engine.UI;

public class WorldMap : MonoBehaviour
{
    public UIView Welcome;
    public Button ViewPlay2;
    public Button ViewPlay3;
    public TextMeshProUGUI score;
    public GameObject lev2;
    public GameObject lev3;
    public GameObject lev1s1;
    public GameObject lev1s2;
    public GameObject lev1s3;
    public GameObject lev2s1;
    public GameObject lev2s2;
    public GameObject lev2s3;
    public GameObject lev3s1;
    public GameObject lev3s2;
    public GameObject lev3s3;

    private void Awake()
    {
        if (String.IsNullOrEmpty(UserController.instance.info.data.user.score))
        {
            score.text = "0";
        }
        else
        {
            score.text = UserController.instance.info.data.user.score.ToString();
        }

        try
        {
            if (!String.IsNullOrEmpty(UserController.instance.info.data.user.settings.level))
            {
                if (UserController.instance.info.data.user.settings.level[0] == '2')
                {
                    lev2.SetActive(true);
                    ViewPlay2.interactable = true;
                    if (UserController.instance.info.data.user.settings.level[1] == '1')
                    {
                        lev1s1.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[1] == '2')
                    {
                        lev1s2.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[1] == '3')
                    {
                        lev1s3.SetActive(true);
                    }
                }

                if (UserController.instance.info.data.user.settings.level[2] == '3')
                {
                    lev3.SetActive(true);
                    ViewPlay3.interactable = true;
                    if (UserController.instance.info.data.user.settings.level[3] == '1')
                    {
                        lev2s1.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[3] == '2')
                    {
                        lev2s2.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[3] == '3')
                    {
                        lev2s3.SetActive(true);
                    }
                }

                if (UserController.instance.info.data.user.settings.level[4] == '4')
                {
                    if (UserController.instance.info.data.user.settings.level[5] == '1')
                    {
                        lev3s1.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[5] == '2')
                    {
                        lev3s2.SetActive(true);
                    }
                    else if (UserController.instance.info.data.user.settings.level[5] == '3')
                    {
                        lev3s3.SetActive(true);
                    }
                }
            }
        }
        catch (Exception e)
        {

            Debug.Log("Message: " + e);
        }

            
        
    }

    private void Start()
    {
        Welcome.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Welcome " + UserController.instance.info.data.user.username;
        Welcome.Show();
    }

    public void lev1Load()
    {
        SceneManager.LoadScene(3);
    }
    public void lev2Load()
    {
        SceneManager.LoadScene(4);
    }
    public void lev3Load()
    {
        SceneManager.LoadScene(5);
    }

    public void SoundOn()
    {

    }

    public void SoundOff()
    {

    }  
}
