using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Lean.Gui;
using Doozy.Engine.UI;
using AdvancedCustomizableSystem;
using TMPro;
using Borodar.FarlandSkies.LowPoly;
public class ModernCity : MonoBehaviour
{
    [Header("Instructions")]
    public LeanToggle[] instructionToggles;
    public UIView instructionsPanel;
    public UIView MessageBox;
    public UIView[] infos;
    public CharacterCustomization male;
    public CharacterCustomization female;
    public UIView ChallengeComplete;
    public UIView starLeft;
    public GameObject starLeftE;
    public UIView starCenter;
    public GameObject starCenterE;
    public UIView starRight;
    public GameObject starRightE;

    public TextMeshProUGUI finalScore;
    public TextMeshProUGUI finalTimeUsed;
    public TextMeshProUGUI blocksUsed;

    public int blockCount;

    bool isMale;
    public bool timeUp;
    public SkyboxCycleManager skybox; 
    public TextMeshProUGUI timerText;
    public float startTime;
    public float timer;

    public TransformFollower follower;

    public Animator Modcamera;
    public Button maleRun;
    public Button femaleRun;
    public Dino dino;

    public InputField saveInput;
    public InputField saveInputF;
    public SaveLoadMenu saveMenu;
    public SaveLoadMenu saveMenuF;


    private void Awake()
    {
        if (UserController.instance.genderIndex == "Male")
        {
            male.gameObject.SetActive(true);
            follower.target = male.gameObject.transform;
            //Modcamera.gameObject.transform.SetParent(male.gameObject.transform);
            isMale = true;
            if (!String.IsNullOrEmpty(UserController.instance.info.data.user.settings.hat))
            {
                male.SetElementByIndex(ClothesPartType.Hat, int.Parse(UserController.instance.info.data.user.settings.hat));
            }
            if (!String.IsNullOrEmpty(UserController.instance.info.data.user.settings.hair))
            {
                male.SetHairByIndex(int.Parse(UserController.instance.info.data.user.settings.hair));
            }
            male.SetElementByIndex(ClothesPartType.Shirt, int.Parse(UserController.instance.info.data.user.settings.top));
            male.SetElementByIndex(ClothesPartType.Pants, int.Parse(UserController.instance.info.data.user.settings.bottom));
            male.SetElementByIndex(ClothesPartType.Shoes, int.Parse(UserController.instance.info.data.user.settings.shoe));

        }

        else if (UserController.instance.genderIndex == "Female")
        {
            female.gameObject.SetActive(true);
            follower.target = female.gameObject.transform;
            //Modcamera.gameObject.transform.SetParent(female.gameObject.transform);
            isMale = false;
            if (!String.IsNullOrEmpty(UserController.instance.info.data.user.settings.hat))
            {
                female.SetElementByIndex(ClothesPartType.Hat, int.Parse(UserController.instance.info.data.user.settings.hat));
            }
            if (!String.IsNullOrEmpty(UserController.instance.info.data.user.settings.hair))
            {
                female.SetHairByIndex(int.Parse(UserController.instance.info.data.user.settings.hair));
            }
            female.SetElementByIndex(ClothesPartType.Shirt, int.Parse(UserController.instance.info.data.user.settings.top));
            female.SetElementByIndex(ClothesPartType.Pants, int.Parse(UserController.instance.info.data.user.settings.bottom));
            female.SetElementByIndex(ClothesPartType.Shoes, int.Parse(UserController.instance.info.data.user.settings.shoe));
        }
    }

    private void Update()
    {
        if (isMale)
        {
            if (male.transform.gameObject.GetComponent<BETargetObject>().crystalCount <= 0 && timeUp == false)
            {
                timeUp = true;
                ChallengeComplete.Show();
                starLeft.Show();
                starCenter.Show();
                starRight.Show();

                male.transform.gameObject.GetComponent<BETargetObject>().score += (int)timer;
                finalScore.text = male.transform.gameObject.GetComponent<BETargetObject>().score.ToString();
                finalTimeUsed.text = ((int)(startTime - timer)).ToString() + "s";
                blocksUsed.text = blockCount.ToString();

                if(SceneManager.GetActiveScene().name == "ModernCity")
                {

                    if (male.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s1)
                    {
                        UserController.instance.s1 = male.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 120 && blockCount < 8)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("23", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 50 && (int)timer < 120 && blockCount < 8)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("22", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 50 && blockCount >= 8)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("21", UserController.instance.info.data.user.id));
                    }
                }
                else if (SceneManager.GetActiveScene().name == "SpaceWorld")
                {
                    if (male.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s2)
                    {
                        UserController.instance.s2 = male.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 120 && blockCount < 14)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("2333", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 50 && (int)timer < 120 && blockCount < 14)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("2232", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 50 && blockCount >= 14)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("2131", UserController.instance.info.data.user.id));
                    }
                }
                
                else if (SceneManager.GetActiveScene().name == "JurrasicWorld")
                {
                    if (male.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s3)
                    {
                        UserController.instance.s3 = male.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 240 && blockCount < 10)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("233343", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 120 && (int)timer < 240 && blockCount < 10)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("223242", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 60 && blockCount >= 10)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("213141", UserController.instance.info.data.user.id));
                    }
                }

                
            }
            else if (timer <= 0 && timeUp == false)
            {
                timeUp = true;
                ChallengeComplete.Show();
                starLeft.Show();
                starCenter.Show();
                starRight.Show();
                male.transform.gameObject.GetComponent<BETargetObject>().score += (int)timer;
                finalScore.text = male.transform.gameObject.GetComponent<BETargetObject>().score.ToString();
                finalTimeUsed.text = ((int)(startTime - timer)).ToString() + "s";
                blocksUsed.text = blockCount.ToString();
            }
        }
        else if (!isMale)
        {
            if (female.transform.gameObject.GetComponent<BETargetObject>().crystalCount <= 0 && timeUp == false)
            {
                timeUp = true;
                ChallengeComplete.Show();
                starLeft.Show();
                starCenter.Show();
                starRight.Show();

                female.transform.gameObject.GetComponent<BETargetObject>().score += (int)timer;
                finalScore.text = female.transform.gameObject.GetComponent<BETargetObject>().score.ToString();
                finalTimeUsed.text = ((int)(startTime - timer)).ToString() + "s";
                blocksUsed.text = blockCount.ToString();

                if (SceneManager.GetActiveScene().name == "ModernCity")
                {
                    if (female.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s1)
                    {
                        UserController.instance.s1 = female.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 120 && blockCount < 8)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("23", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 50 && (int)timer < 120 && blockCount < 8)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("22", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 50 && blockCount >= 8)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("21", UserController.instance.info.data.user.id));
                    }
                }
                else if (SceneManager.GetActiveScene().name == "SpaceWorld")
                {
                    if (female.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s2)
                    {
                        UserController.instance.s2 = female.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 120 && blockCount < 14)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("2333", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 50 && (int)timer < 120 && blockCount < 14)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("2232", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 50 && blockCount >= 14)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("2131", UserController.instance.info.data.user.id));
                    }
                }
                else if (SceneManager.GetActiveScene().name == "JurrasicWorld")
                {
                    if (female.transform.gameObject.GetComponent<BETargetObject>().score > UserController.instance.s3)
                    {
                        UserController.instance.s3 = female.transform.gameObject.GetComponent<BETargetObject>().score;
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }
                    else
                    {
                        StartCoroutine(UserController.instance.UpdateGender(UserController.instance.info.data.user.id, UserController.instance.genderIndex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
                        int highscore = UserController.instance.s1 + UserController.instance.s2 + UserController.instance.s3;
                        StartCoroutine(UserController.instance.UpdateScore(highscore.ToString(), UserController.instance.info.data.user.id));
                    }

                    if ((int)timer >= 240 && blockCount < 6)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        starRightE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("233343", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer >= 120 && (int)timer < 240 && blockCount < 8)
                    {
                        starLeftE.SetActive(true);
                        starCenterE.SetActive(true);
                        StartCoroutine(UserController.instance.UpdateSettings("223242", UserController.instance.info.data.user.id));
                    }
                    else if ((int)timer < 60 && blockCount >= 8)
                    {
                        starLeftE.SetActive(true); ;
                        StartCoroutine(UserController.instance.UpdateSettings("213141", UserController.instance.info.data.user.id));
                    }
                }
            }
            else if (timer <= 0 && timeUp == false)
            {
                timeUp = true;
                ChallengeComplete.Show();
                starLeft.Show();
                starCenter.Show();
                starRight.Show();
                female.transform.gameObject.GetComponent<BETargetObject>().score += (int)timer;
                finalScore.text = female.transform.gameObject.GetComponent<BETargetObject>().score.ToString();
                finalTimeUsed.text = ((int)(startTime - timer)).ToString() + "s";
                blocksUsed.text = blockCount.ToString();
            }
        }

        
    }

    public void CloseInfo()
    {
        instructionsPanel.Hide();
        MessageBox.Hide();
        StartCoroutine(StartTimer());
        skybox.Paused = false;
        if (UserController.instance.isRestart)
        {
            if (UserController.instance.genderIndex == "Male")
            {
                try
                {
                    saveInput.text = "temp.BE";
                    saveMenu.Load();
                    saveInput.text = "";

                }
                catch (Exception e)
                {

                    Debug.Log(e);
                }
                
            }
            else if (UserController.instance.genderIndex == "Female")
            {
                try
                {
                    saveInputF.text = "temp.BE";
                    saveMenuF.Load();
                    saveInput.text = "";
                }
                catch (Exception e)
                {

                    Debug.Log(e);
                }
            }
        }
    }

    public void OpenInfo()
    {
        instructionsPanel.Show();
        MessageBox.Show();
    }

    public void setDino()
    {
        dino.moveSpeed = 4.5f;
        dino.transform.gameObject.GetComponentInChildren<Animator>().SetBool("Follow", true);
    }

    public void Run()
    {
        try
        {
            Modcamera.SetTrigger("zoom");
        }
        catch (Exception e)
        {

            Debug.Log("Null Animator " + e);
        }
        
        if (isMale)
        {
            maleRun.interactable = false;
        }
        else if (!isMale)
        {
            femaleRun.interactable = false;
        }
    }

    public void Back()
    {
        UserController.instance.isRestart = false;
    }

    public void Restart()
    {
        if (UserController.instance.genderIndex == "Male")
        {
                UserController.instance.isRestart = true;
                try
                {
                    saveInput.text = "temp";
                    saveMenu.ConfirmSaveCode();
                }
                catch (Exception e)
                {

                    Debug.Log(e);
                }

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (UserController.instance.genderIndex == "Female")
        {
                UserController.instance.isRestart = true;
                try
                {
                    saveInputF.text = "temp";
                    saveMenuF.ConfirmSaveCode();
                }
                catch (Exception e)
                {

                    Debug.Log(e);
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        
    }

    public void ToHome()
    {
        UserController.instance.isRestart = false;
        SceneManager.LoadScene(2);
    }

    public void NextInfo()
    {
        foreach (LeanToggle toggle in instructionToggles)
        {
            if (toggle.On)
            {
                int index = Array.IndexOf(instructionToggles, toggle);
                if ((index + 1) < instructionToggles.Length)
                {
                    instructionToggles[index + 1].TurnOn();
                    infos[index].Hide();
                    infos[index + 1].Show();
                    break;
                }

            }
        }
    }

    public void PreviousInfo()
    {
        foreach (LeanToggle toggle in instructionToggles)
        {
            if (toggle.On)
            {
                int index = Array.IndexOf(instructionToggles, toggle);
                if ((index - 1) >= 0)
                {
                    instructionToggles[index - 1].TurnOn();
                    infos[index].Hide();
                    infos[index - 1].Show();
                    break;
                }
            }
        }
    }

    public IEnumerator StartTimer()
    {
        timer = startTime;
        timeUp = false;

        do
        {
            timer -= Time.deltaTime;
            FormatText();
            yield return null;
        } while (timer > 0 && timeUp == false);
    }

    void FormatText()
    {
        int minutes = (int)(timer / 60) % 60;
        int seconds = (int)(timer % 60);

        //timerText.text = "TIME: " + minutes + ":" + seconds;
        if (minutes < 10 && seconds > 9)
        {
            timerText.text = "0" + minutes + ":" + seconds;
        }
        else if (minutes < 10 && seconds < 10)
        {
            timerText.text = "0" + minutes + ":0" + seconds;
        }
        else if (minutes > 9 && seconds < 10)
        {
            timerText.text = minutes + ":0" + seconds;
        }
        else
        {
            timerText.text = minutes + ":" + seconds;
        }
    }
}
