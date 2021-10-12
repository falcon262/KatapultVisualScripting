using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using AdvancedCustomizableSystem;
using Lean.Gui;
using Doozy.Engine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Block Toggles")]
    public GameObject Hats;
    public GameObject Tops;
    public GameObject Bottoms;
    public GameObject Hair;
    public GameObject Shoes;

    [Header("Block Types")]
    public GameObject HatsBlocks;
    public GameObject TopsBlocks;
    public GameObject BottomsBlocks;
    public GameObject HairBlocks;
    public GameObject ShoesBlocks;

    public CharacterCustomization Male;

    [Header("Instructions")]
    public LeanToggle[] instructionToggles;
    public UIView instructionsPanel;
    public UIView MessageBox;
    public UIView[] infos;

    // Start is called before the first frame update
    void Start()
    {
        Male.SetElementByIndex(ClothesPartType.Shirt, 1);
        Male.SetElementByIndex(ClothesPartType.Pants, 1);
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
        }
        else if (Tops.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(true);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
        }
        else if (Bottoms.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(true);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(false);
        }
        else if (Hair.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(true);
            ShoesBlocks.SetActive(false);
        }
        else if (Shoes.GetComponent<LeanToggle>().On)
        {
            HatsBlocks.SetActive(false);
            TopsBlocks.SetActive(false);
            BottomsBlocks.SetActive(false);
            HairBlocks.SetActive(false);
            ShoesBlocks.SetActive(true);
        }
    }
    #endregion
}
