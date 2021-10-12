using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using Doozy.Engine.UI;
using TMPro;

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

    [SerializeField]
    List<UserEntries> userDatas;

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

    string username;
    string password;
    string fullname;
    string email;



    private void Awake()
    {

        if (File.Exists(Application.persistentDataPath + "/userData.json"))
        {
            userDatas = SaveData.ReadListFromJSON<UserEntries>("userData.json");
        }
        else
        {
            userDatas = new List<UserEntries>();
        }

/*        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);*/
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
                InvalidDetails.Hide();
                createAccountView.Hide();
                passwordView.Show();
                charactersBehind.Hide();
                charactersShy.Show();
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
                    userDatas.Add(new UserEntries(username, password, fullname, email));
                    SaveData.SaveToJSON(userDatas, "userData.json");
                    InvalidDetails.Hide();
                    passwordView.Hide();
                    SignInView.Show();
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
                int match = 0;

                foreach (UserEntries entry in userDatas)
                {
                    if (entry.username == usernameLogIn.text && entry.password == passwordLogIn.text)
                    {
                        match = 1;
                        break;
                    }
                    else
                    {
                        match = 0;
                    }
                }

                if (match == 1)
                {

                        InvalidDetails.Hide();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else
                {
                    InvalidDetails.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Invalid username or password";
                    InvalidDetails.Show();
                }
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
