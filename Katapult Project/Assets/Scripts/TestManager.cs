using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestManager : MonoBehaviour
{
    public GameObject SignIn;
    public GameObject SignUp;
    public GameObject Menu;

    public void onSignUp()
    {
        SignIn.SetActive(false);
        SignUp.SetActive(true);
        Menu.SetActive(false);

    }
    public void onSignIn()
    {
        SignIn.SetActive(true);
        SignUp.SetActive(false);
        Menu.SetActive(false);
    }
    public void onMenus()
    {
        SignIn.SetActive(false);
        SignUp.SetActive(false);
        Menu.SetActive(true);
    }
    public void Level1()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
