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

    public Sprite fullstar;

    public GameObject[] leaders;

    private void Awake()
    {
        StartCoroutine(UserController.instance.GetAllUsers());

        string[] data = UserController.instance.info.data.user.settings.sex.Split(':');
        UserController.instance.genderIndex = data[0];
        UserController.instance.s1 = int.Parse(data[1]);
        UserController.instance.s2 = int.Parse(data[2]);
        UserController.instance.s3 = int.Parse(data[3]);
        UserController.instance.sex = UserController.instance.genderIndex;

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
        StartCoroutine(WelcomeMessage());
    }

    public void PopulateLeaderBoard()
    {
        try
        {
            for (int i = 0; i < leaders.Length; i++)
            {
                leaders[i].transform.Find("Username").GetComponent<TextMeshProUGUI>().text = UserController.instance.usersInfo.data[i].username;
                leaders[i].transform.Find("Score").GetComponent<TextMeshProUGUI>().text = UserController.instance.usersInfo.data[i].score;

                if (int.Parse(UserController.instance.usersInfo.data[i].score) >= 2000)
                {
                    leaders[i].transform.Find("star1").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star2").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star3").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star4").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star5").GetComponent<Image>().sprite = fullstar;
                }
                else if (int.Parse(UserController.instance.usersInfo.data[i].score) >= 1000 && int.Parse(UserController.instance.usersInfo.data[i].score) < 2000)
                {
                    leaders[i].transform.Find("star1").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star2").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star3").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star4").GetComponent<Image>().sprite = fullstar;
                }
                else if (int.Parse(UserController.instance.usersInfo.data[i].score) >= 500 && int.Parse(UserController.instance.usersInfo.data[i].score) < 1000)
                {
                    leaders[i].transform.Find("star1").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star2").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star3").GetComponent<Image>().sprite = fullstar;
                }
                else if (int.Parse(UserController.instance.usersInfo.data[i].score) >= 300 && int.Parse(UserController.instance.usersInfo.data[i].score) < 500)
                {
                    leaders[i].transform.Find("star1").GetComponent<Image>().sprite = fullstar;
                    leaders[i].transform.Find("star2").GetComponent<Image>().sprite = fullstar;
                }
                else if (int.Parse(UserController.instance.usersInfo.data[i].score) >= 0 && int.Parse(UserController.instance.usersInfo.data[i].score) < 300)
                {
                    leaders[i].transform.Find("star1").GetComponent<Image>().sprite = fullstar;
                }

            }
        }
        catch (Exception e)
        {

            Debug.Log("Out of bounds: " + e);
        }
        
    }

    IEnumerator WelcomeMessage()
    {
        Welcome.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Welcome " + UserController.instance.info.data.user.username;
        Welcome.Show();
        yield return new WaitForSeconds(2f);
        Welcome.Hide();
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
    
    public void AvatarLoad()
    {
        SceneManager.LoadScene(1);
    }

    public void SoundOn()
    {

    }

    public void SoundOff()
    {

    }  
}
