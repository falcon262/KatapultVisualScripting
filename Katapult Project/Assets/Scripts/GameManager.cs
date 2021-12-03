using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using Doozy.Engine.UI;
using TMPro;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TMP_InputField fullNameT;    

    [SerializeField]
    TMP_InputField emailT; 
    
    [SerializeField]
    TMP_InputField usernameT;    

    [SerializeField]
    TMP_InputField passwordT;

    [SerializeField]
    TMP_InputField confirmPasswordT;

    [SerializeField]
    TMP_InputField usernameLogIn;
    [SerializeField]
    TMP_InputField passwordLogIn;

    [SerializeField]
    UIView InvalidDetails;

    public UIView createAccountView;
    public UIView charactersShy;
    public UIView charactersBehind;
    public UIView charactersInfront;
    public UIView passwordView;
    public UIView SignInView;
    public Button continueButton;
    public Button createButton;
    public Button createAccountButton;
    public Button OK;
    public UIToggle rememberMe;

    string username;
    string password;
    string conPassword;
    string fullname;
    string email;

    public int inputSelected;

    public InputEntry entry;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            inputSelected--;
            if (inputSelected < 0) inputSelected = 0;
            onSelected();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            inputSelected++;
            if (createAccountView.IsVisible && inputSelected > 2) inputSelected = 2;
            if ((passwordView.IsVisible || SignInView.IsVisible) && inputSelected > 1) inputSelected = 1;
            onSelected();

        }
            
        void onSelected()
        {
            if (createAccountView.IsVisible)
            {
                switch (inputSelected)
                {
                    case 0: fullNameT.Select();
                        break;
                    case 1: emailT.Select();
                        break;
                    case 2: usernameT.Select();
                        break;
                  
                }
            }
            else if (passwordView.IsVisible)
            {
                switch (inputSelected)
                {
                    case 0: passwordT.Select();
                        break;
                    case 1: confirmPasswordT.Select();
                        break;
                }
            }
            
            else if (SignInView.IsVisible)
            {
                switch (inputSelected)
                {
                    case 0: usernameLogIn.Select();
                        break;
                    case 1: passwordLogIn.Select();
                        break;
                }
            }
        }
    }

    public void EnterFullname() => inputSelected = 0;
    public void EnterMail() => inputSelected = 1;
    public void EnterUsername() => inputSelected = 2;
    public void EnterPassword() => inputSelected = 0;
    public void EnterConPassword() => inputSelected = 1;
    public void EnterSignUsername() => inputSelected = 0;
    public void EnterSignPassword() => inputSelected = 1;

    IEnumerator RegisterUser(string email, string password, string conPassword, string username, string name)
    {
        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("password_confirmation", conPassword);
        form.AddField("username", username);
        form.AddField("name", name);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/users", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                //NoInternet.SetActive(true);
                Debug.Log(www.error);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> dict in www.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                //Print Headers
                Debug.Log(sb.ToString());

                //Print Body
                Debug.Log(www.downloadHandler.text);

                StartCoroutine(UserController.instance.UpdateGender(JsonUtility.FromJson<UserController.userInfo>(www.downloadHandler.text).data.user.id, UserController.instance.sex + ":" + UserController.instance.s1 + ":" + UserController.instance.s2 + ":" + UserController.instance.s3));
/*                if (www.downloadHandler.text.Contains("false"))
                {
                    Debug.Log("Error Occured(404)");
                }
                else if (www.downloadHandler.text.Contains("200"))
                {
                    Debug.Log("User Registered Successfully");
                }*/
            }
        }


    }

    public void GenderMale()
    {
        UserController.instance.sex = "Male";
    }

    public void GenderFemale()
    {
        UserController.instance.sex = "Female";
    }
    
    IEnumerator UserLogin(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/token", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> dict in www.GetResponseHeaders())
                {
                    sb.Append(dict.Key).Append(": \t[").Append(dict.Value).Append("]\n");
                }

                //Print Headers
                Debug.Log(sb.ToString());

                //Print Body
                Debug.Log(www.downloadHandler.text);
                if (www.downloadHandler.text.Contains("false"))
                {
                    InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid username or password";
                    InvalidDetails.Show();
                }
                else if (www.downloadHandler.text.Contains("200"))
                {
                    Debug.Log("LoggedIn Successfully");
                    InvalidDetails.Hide();
                    //info = JsonUtility.FromJson<userInfo>(www.downloadHandler.text);
                    UserController.instance.UserParse(www.downloadHandler.text);
                    if (String.IsNullOrEmpty(UserController.instance.info.data.user.settings.top))
                    {
                        if (rememberMe.IsOn)
                        {
                            entry = new InputEntry(usernameLogIn.text, passwordLogIn.text);
                            SaveData.SaveToJSON(entry, "RememberMe");
                        }

                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        if (rememberMe.IsOn)
                        {
                            entry = new InputEntry(usernameLogIn.text, passwordLogIn.text);
                            SaveData.SaveToJSON(entry, "RememberMe");
                        }
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                    }
                    
                }
            }
        }


    }

    public void ReadOnCreate()
    {

        entry = SaveData.ReadFromJSON<InputEntry>("RememberMe");

        if(entry != null) usernameLogIn.text = entry.username;
        if(entry != null) passwordLogIn.text = entry.password;

    }

    private void Start()
    {

        continueButton.onClick.AddListener(delegate {

            if (fullNameT.text.Equals("") || !emailT.text.Contains("@") || emailT.text.Equals("") || usernameT.text.Equals(""))
            {
                InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid Details Entered";
                if (InvalidDetails.IsHidden)
                {
                    InvalidDetails.Show();
                }
                
            }
            else
            {
                fullname = fullNameT.text;
                email = emailT.text;
                username = usernameT.text;
                fullNameT.text = "";
                emailT.text = "";
                usernameT.text = "";
                UserController.instance.sex = "Male";
                InvalidDetails.Hide();
                createAccountView.Hide();
                passwordView.Show();
                charactersBehind.Hide();
                charactersShy.Show();
                inputSelected = 0;
            }


        });
        
        createButton.onClick.AddListener(delegate {

            if (passwordT.text.Equals("") || confirmPasswordT.text.Equals(""))
            {
                InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Password can not be empty";
                if (InvalidDetails.IsHidden)
                {
                    InvalidDetails.Show();
                }
                
            }
            else
            {
                if (passwordT.text.Equals(confirmPasswordT.text))
                {
                    password = passwordT.text;
                    conPassword = password;
                    StartCoroutine(RegisterUser(email, password, conPassword, username, fullname));
                    InvalidDetails.Hide();
                    passwordView.Hide();
                    SignInView.Show();
                    inputSelected = 0;
                    charactersShy.Hide();
                    charactersInfront.Show();
                    passwordT.text = "";
                    confirmPasswordT.text = "";
                    
                }
                else
                {
                    InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Passwords do not match";
                    if (InvalidDetails.IsHidden)
                    {
                        InvalidDetails.Hide();
                        InvalidDetails.Show();
                    }
                }
            }


        });
        
        OK.onClick.AddListener(delegate {

            if (usernameLogIn.text.Equals("") || passwordLogIn.text.Equals(""))
            {
                InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid username or password";
                if (InvalidDetails.IsHidden)
                {
                    InvalidDetails.Show();
                }
                
            }
            else
            {
                StartCoroutine(UserLogin(usernameLogIn.text, passwordLogIn.text));
            }


        });

        createAccountButton.onClick.AddListener(delegate
        {
            SignInView.Hide();
            charactersInfront.Hide();
            createAccountView.Show();
            charactersBehind.Show();

        });
    }

}
