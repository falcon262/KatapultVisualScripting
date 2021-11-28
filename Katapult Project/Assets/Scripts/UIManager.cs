using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using UnityEngine.Video;
using AdvancedCustomizableSystem;
using Lean.Gui;
using Doozy.Engine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Block Toggles")]
    public GameObject Events;
    public GameObject Hats;
    public GameObject Tops;
    public GameObject Bottoms;
    public GameObject Hair;
    public GameObject Shoes;

    [Header("Block Types")]
    public GameObject EventBlocks;
    public GameObject HatsBlocks;
    public GameObject TopsBlocks;
    public GameObject BottomsBlocks;
    public GameObject HairBlocks;
    public GameObject ShoesBlocks;
    
    [Header("Block TogglesF")]
    public GameObject HatsF;
    public GameObject TopsF;
    public GameObject BottomsF;
    public GameObject HairF;
    public GameObject ShoesF;
    public GameObject EventsF;

    [Header("Block TypesF")]
    public GameObject HatsBlocksF;
    public GameObject TopsBlocksF;
    public GameObject BottomsBlocksF;
    public GameObject HairBlocksF;
    public GameObject ShoesBlocksF;
    public GameObject EventsBlocksF;

    public CharacterCustomization Male;
    public CharacterCustomization Female;

    [Header("Instructions")]
    public LeanToggle[] instructionToggles;
    public UIView instructionsPanel;
    public UIView MessageBox;
    public UIView[] infos;
    public UIView suitUpError;

    [Header("Male Character")]
    public GameObject maleCharacter;
    public GameObject maleCanvas;

    [Header("Female Character")]
    public GameObject femaleCharacter;
    public GameObject femaleCanvas;

    public VideoPlayer vidPlayer;
    private void Awake()
    {
        vidPlayer.url = Path.Combine(Application.streamingAssetsPath, "Outboarding Story.mp4");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (UserController.instance.info.data.user.settings.sex == "Male")
        {
            onMale();
        }
        else if (UserController.instance.info.data.user.settings.sex == "Female")
        {
            onFemale();
        }
    }

    public void CloseInfo()
    {
        instructionsPanel.Hide();
        MessageBox.Hide();
    }

    public void OpenInfo()
    {
        instructionsPanel.Show();
        MessageBox.Show();
    }

    public void onCreate()
    {
       
        if(UserController.instance.top == "" || UserController.instance.bottom == "" || UserController.instance.shoe == "")
        {
            suitUpError.Show();
        }
        else
        {
            suitUpError.Hide();
            StartCoroutine(UserController.instance.UpdateSettings(UserController.instance.info.data.user.id, UserController.instance.sex, UserController.instance.hat, UserController.instance.hair, UserController.instance.top, UserController.instance.bottom, UserController.instance.shoe));
            suitUpError.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Loading...";
        }

    }

    public void onMale()
    {
        maleCharacter.SetActive(true);
        maleCanvas.SetActive(true);
        femaleCharacter.SetActive(false);
        femaleCanvas.SetActive(false);
        Male.SetElementByIndex(ClothesPartType.Shirt, 1);
        Male.SetElementByIndex(ClothesPartType.Pants, 1);
    }

    public void onFemale()
    {
        maleCharacter.SetActive(false);
        maleCanvas.SetActive(false);
        femaleCharacter.SetActive(true);
        femaleCanvas.SetActive(true);
        Female.SetElementByIndex(ClothesPartType.Shirt, 1);
        Female.SetElementByIndex(ClothesPartType.Pants, 1);
    }

    public void NextInfo()
    {
        try
        {
            foreach (LeanToggle toggle in instructionToggles)
            {
                if (toggle.On)
                {
                    int index = Array.IndexOf(instructionToggles, toggle);
                    instructionToggles[index + 1].TurnOn();
                    infos[index].Hide();
                    infos[index + 1].Show();
                    break;
                }
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    public void PreviousInfo()
    {
        try
        {
            foreach (LeanToggle toggle in instructionToggles)
            {
                if (toggle.On)
                {
                    int index = Array.IndexOf(instructionToggles, toggle);
                    instructionToggles[index - 1].TurnOn();
                    infos[index].Hide();
                    infos[index - 1].Show();
                    break;
                }
            }
        }
        catch (Exception e)
        {

            Debug.Log(e.Message);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TypeSwitch();
        TypeSwitchF();
    }

    #region BlockTypeSwitching
    //Func to switch between block types
    public void TypeSwitch()
    {
        if (Hats.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(true);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
            EventBlocks.SetActive(false);
        }
        else if (Tops.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(true);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
            EventBlocks.SetActive(false);
        }
        else if (Bottoms.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(true);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
            EventBlocks.SetActive(false);
        }
        else if (Hair.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(true);
            ShoesBlocks.SetActive(false);
            EventBlocks.SetActive(false);
        }
        else if (Shoes.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(true);
            EventBlocks.SetActive(false);
        }
        else if (Events.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
            EventBlocks.SetActive(true);
        }
    }
    #endregion    
    
    #region BlockTypeSwitchingF
    //Func to switch between block types
    public void TypeSwitchF()
    {
        if (HatsF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(true);
            TopsBlocksF.SetActive(false);
            BottomsBlocksF.SetActive(false);
            HairBlocksF.SetActive(false);
            ShoesBlocksF.SetActive(false);
            EventsBlocksF.SetActive(false);
        }
        else if (TopsF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(false);
            TopsBlocksF.SetActive(true);
            BottomsBlocksF.SetActive(false);
            HairBlocksF.SetActive(false);
            ShoesBlocksF.SetActive(false);
            EventsBlocksF.SetActive(false);
        }
        else if (BottomsF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(false);
            TopsBlocksF.SetActive(false);
            BottomsBlocksF.SetActive(true);
            HairBlocksF.SetActive(false);
            ShoesBlocksF.SetActive(false);
            EventsBlocksF.SetActive(false);
        }
        else if (HairF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(false);
            TopsBlocksF.SetActive(false);
            BottomsBlocksF.SetActive(false);
            HairBlocksF.SetActive(true);
            ShoesBlocksF.SetActive(false);
            EventsBlocksF.SetActive(false);
        }
        else if (ShoesF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(false);
            TopsBlocksF.SetActive(false);
            BottomsBlocksF.SetActive(false);
            HairBlocksF.SetActive(false);
            ShoesBlocksF.SetActive(true);
            EventsBlocksF.SetActive(false);
        }
        else if (EventsF.GetComponent<LeanToggle>().On)
        {
            HatsBlocksF.SetActive(false);
            TopsBlocksF.SetActive(false);
            BottomsBlocksF.SetActive(false);
            HairBlocksF.SetActive(false);
            ShoesBlocksF.SetActive(false);
            EventsBlocksF.SetActive(true);
        }
    }
    #endregion
}
