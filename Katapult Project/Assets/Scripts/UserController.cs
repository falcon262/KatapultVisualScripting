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
using SimpleJSON;

public class UserController : MonoBehaviour
{
    [Serializable]
    public struct userInfo
    {
        public string success;
        public string code;
        public data data;
    }
    public userInfo info;

    [Serializable]
    public struct updateInfo
    {
        public string success;
        public string code;
        public datas data;
    }
    private updateInfo update;

    [Serializable]
    public struct allUsersInfo
    {
        public string success;
        public string code;
        public List<datas> data;
    }
    public allUsersInfo usersInfo;

    [Serializable]
    public struct data
    {
        public string token;
        public user user;
    }

    [Serializable]
    public struct datas
    {
        public string id;
        public string email;
        public string username;
        public string score;
        public settings settings;
    }

    [Serializable]
    public struct user
    {
        public string id;
        public string email;
        public string username;
        public string score;
        public settings settings;
    }

    [Serializable]
    public struct settings
    {
        public string sex;
        public string level;
        public string hat;
        public string hair;
        public string top;
        public string bottom;
        public string shoe;

    }

    public string fullName;
    public string localId;
    public string isNewUser;
    public string authEmail;



    /*
     {
  "federatedId": "https://accounts.google.com/114917102882576394672",
  "providerId": "google.com",
  "emailVerified": false,
  "firstName": "Joseph",
  "fullName": "Joseph Asante",
  "lastName": "Asante",
  "photoUrl": "https://lh3.googleusercontent.com/a-/AOh14GiibbkCC93_0FP1-5uZQ_9FQCDWF213tI8mL3fX4g=s96-c",
  "originalEmail": "thommpson19@gmail.com",
  "localId": "apk7meFmZHZra7N7044zJfQuPZC2",
  "displayName": "Joseph Asante",
  "idToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6ImYyNGYzMTQ4MTk3ZWNlYTUyOTE3YzNmMTgzOGFiNWQ0ODg3ZWEwNzYiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL3NlY3VyZXRva2VuLmdvb2dsZS5jb20va2F0YXB1bHQtYWxwaGEiLCJhdWQiOiJrYXRhcHVsdC1hbHBoYSIsImF1dGhfdGltZSI6MTY0NDU2MjQ5MCwidXNlcl9pZCI6ImFwazdtZUZtWkhacmE3TjcwNDR6SmZRdVBaQzIiLCJzdWIiOiJhcGs3bWVGbVpIWnJhN043MDQ0ekpmUXVQWkMyIiwiaWF0IjoxNjQ0NTYyNDkwLCJleHAiOjE2NDQ1NjYwOTAsImVtYWlsIjoidGhvbW1wc29uMTlAZ21haWwuY29tIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsImZpcmViYXNlIjp7ImlkZW50aXRpZXMiOnsiZ29vZ2xlLmNvbSI6WyIxMTQ5MTcxMDI4ODI1NzYzOTQ2NzIiXSwiZW1haWwiOlsidGhvbW1wc29uMTlAZ21haWwuY29tIl19LCJzaWduX2luX3Byb3ZpZGVyIjoiZ29vZ2xlLmNvbSJ9fQ.yAcwYrDFADdx8lDyhAocIuZ_X4aJKoQHXN3jHJmf9DzELk5Qyn_mluVtOvvgeB7GPcyeb4Xb-JTI7m2ZxVIkPCbvg4hGY_opjBK4HguN6qIoG4La0Z_EZPgz1O8S7-mgz_E9Impjg4LLwzqi9b11TARYe40EUmeV9fShTTN9u93FpMc8Kgbi1fdgtW_zLlkqpU_FBvCt1rM5NaHGfbcaWxNIGGQaXkn-JYAxbAioVgm8nsaAA_6-WoUYq8XxFruys88zEwlH4LrVwHEhuSMpsdanVncmF5EIEhQMrnKKfwHzToJM7a-Th66mllvyIgl51MGdcVmejGanIdkZC-ELMg",
  "refreshToken": "AIwUaOmiuYyjpbVqdWI9rCJVWpWphbLkfgCah1xFrjzN5CWhiJhkwmjdBA8Ltb7ZJSo1OdimA59E4lcJzIPxctGdBxbMFXqn6hYmKqswcWDZ0dwNKtaCrArhz7uxa5p5oSSG49Y0M_MVMCkvjWCTmKcF6ogtDS_8Y_c_VnIbq4JSmfAYwJPoxGYi2qN4l-5C6acHeI99H2hcWlYNbqhqvJtIYdYpwYtX720O885wH4BKA2aSMRgKdOj6dk3BAkr9eCda8ipOzj1uG5T7P7LszmKQ6kcf_mDJ9VcWFnwHiI3LI4pFY-HpAq7OyX5FGcBkIFet_uuEObZlcQpM0vRiogfpsUW2AV-sbQevDB71xJqCGCJK42WhuI3JOjjkqeBW0NBljrf7sUN5",
  "expiresIn": "3600",
  "oauthIdToken": "eyJhbGciOiJSUzI1NiIsImtpZCI6ImY0YmIyMjBjZDA5NGIwYWU5MGRkNzNlMTBjMTBlN2RiNTRiODkyODAiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2FjY291bnRzLmdvb2dsZS5jb20iLCJhenAiOiI2Njg1Mzc4NjY3NjItcDAyY3M3a3N1NnJnMG4xcmdqb2dmbWVlYWp2MWhqbmQuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJhdWQiOiI2Njg1Mzc4NjY3NjItcDAyY3M3a3N1NnJnMG4xcmdqb2dmbWVlYWp2MWhqbmQuYXBwcy5nb29nbGV1c2VyY29udGVudC5jb20iLCJzdWIiOiIxMTQ5MTcxMDI4ODI1NzYzOTQ2NzIiLCJhdF9oYXNoIjoiSTlUNjNaRFZhNnhXYkl6RmdPNTRKdyIsIm5hbWUiOiJKb3NlcGggQXNhbnRlIiwicGljdHVyZSI6Imh0dHBzOi8vbGgzLmdvb2dsZXVzZXJjb250ZW50LmNvbS9hLS9BT2gxNEdpaWJia0NDOTNfMEZQMS01dVpRXzlGUUNEV0YyMTN0SThtTDNmWDRnPXM5Ni1jIiwiZ2l2ZW5fbmFtZSI6Ikpvc2VwaCIsImZhbWlseV9uYW1lIjoiQXNhbnRlIiwibG9jYWxlIjoiZW4iLCJpYXQiOjE2NDQ1NjI0ODksImV4cCI6MTY0NDU2NjA4OX0.TgAJrMJW7aoL1ynio5JA1_xBQr55e7MyA_PrWialO1JQDciSjVA-iEHStpf4gtw9o4K8zVkpRYzvoOP3Nwcq8I7TBxEZ_H3-OEv6OlPCXmOlNbS0Y6WMuHL7TCXDs12Emq-arg_Enot0FEACMMAatSA1_fzwOQ9oUYauGBsJnOnTVnjJIgnscaxWZ_LDm1YhD-OnO0-aGdOz6zEudvuK-LQnoRkHpv-pp2bKLXcv1k9Iba34SDc9fwir-enhjEEQQ-ZhlFG67DGTnU9XZV0BIWmCp0QLYUETD4FR6MTliY2heu0ebXoCH38YdmX_vmFcQ5uQ_PurSlFsmyg8pWJH8Q",
  "rawUserInfo": "{\"iss\":\"https://accounts.google.com\",\"azp\":\"668537866762-p02cs7ksu6rg0n1rgjogfmeeajv1hjnd.apps.googleusercontent.com\",\"aud\":\"668537866762-p02cs7ksu6rg0n1rgjogfmeeajv1hjnd.apps.googleusercontent.com\",\"sub\":\"114917102882576394672\",\"at_hash\":\"I9T63ZDVa6xWbIzFgO54Jw\",\"name\":\"Joseph Asante\",\"picture\":\"https://lh3.googleusercontent.com/a-/AOh14GiibbkCC93_0FP1-5uZQ_9FQCDWF213tI8mL3fX4g=s96-c\",\"given_name\":\"Joseph\",\"family_name\":\"Asante\",\"locale\":\"en\",\"iat\":1644562489,\"exp\":1644566089}",
  "kind": "identitytoolkit#VerifyAssertionResponse"
}

     
     */

    public string sex = "";
    public string level = "";
    public string hat = "";
    public string hair = "";
    public string top = "";
    public string bottom = "";
    public string shoe = "";

    public string genderIndex;
    public int s1 = 0;
    public int s2 = 0;
    public int s3 = 0;

    public AudioSource tick;
    public AudioSource zapCrystal;
    public AudioSource gameMusic;


    public bool isRestart;

    public static UserController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }



    private void Start()
    {
        //StartCoroutine(UpdateSettings("", "31fcee14-e79f-475d-b67d-8f0758370438"));
        //StartCoroutine(UpdateScore("", "31fcee14-e79f-475d-b67d-8f0758370438"));
    }

    public void UserParse(string json)
    {
        info = JsonUtility.FromJson<userInfo>(json);
    }

    public void AllUsersParse(string json)
    {
        usersInfo = JsonUtility.FromJson<allUsersInfo>(json);
    }

    public IEnumerator GetAllUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://katapult-api.herokuapp.com/api/v1/users"))
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
                instance.AllUsersParse(www.downloadHandler.text);

                for (int i = 0; i < instance.usersInfo.data.Count; i++)
                {
                    for (int j = i + 1; j < instance.usersInfo.data.Count; j++)
                    {
                        if (String.IsNullOrEmpty(instance.usersInfo.data[j].score) && String.IsNullOrEmpty(instance.usersInfo.data[i].score))
                        {
                            datas locVar = instance.usersInfo.data[j];
                            locVar.score = "0";
                            instance.usersInfo.data[j] = locVar;

                            datas locVar2 = instance.usersInfo.data[i];
                            locVar2.score = "0";
                            instance.usersInfo.data[i] = locVar2;

                            if (int.Parse(instance.usersInfo.data[j].score) > int.Parse(instance.usersInfo.data[i].score))
                            {
                                //Swap
                                datas tmp = instance.usersInfo.data[i];
                                instance.usersInfo.data[i] = instance.usersInfo.data[j];
                                instance.usersInfo.data[j] = tmp;

                            }
                        }
                        else if (String.IsNullOrEmpty(instance.usersInfo.data[j].score) && !String.IsNullOrEmpty(instance.usersInfo.data[i].score))
                        {
                            datas locVar = instance.usersInfo.data[j];
                            locVar.score = "0";
                            instance.usersInfo.data[j] = locVar;

                            if (int.Parse(instance.usersInfo.data[j].score) > int.Parse(instance.usersInfo.data[i].score))
                            {
                                //Swap
                                datas tmp = instance.usersInfo.data[i];
                                instance.usersInfo.data[i] = instance.usersInfo.data[j];
                                instance.usersInfo.data[j] = tmp;

                            }
                        }
                        else if (!String.IsNullOrEmpty(instance.usersInfo.data[j].score) && String.IsNullOrEmpty(instance.usersInfo.data[i].score))
                        {

                            datas locVar2 = instance.usersInfo.data[i];
                            locVar2.score = "0";
                            instance.usersInfo.data[i] = locVar2;

                            if (int.Parse(instance.usersInfo.data[j].score) > int.Parse(instance.usersInfo.data[i].score))
                            {
                                //Swap
                                datas tmp = instance.usersInfo.data[i];
                                instance.usersInfo.data[i] = instance.usersInfo.data[j];
                                instance.usersInfo.data[j] = tmp;

                            }
                        }
                        else
                        {
                            if (int.Parse(instance.usersInfo.data[j].score) > int.Parse(instance.usersInfo.data[i].score))
                            {
                                //Swap
                                datas tmp = instance.usersInfo.data[i];
                                instance.usersInfo.data[i] = instance.usersInfo.data[j];
                                instance.usersInfo.data[j] = tmp;

                            }
                        }
                    }
                }
            }
        }


    }

    public IEnumerator UpdateScore(string score, string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("score", score);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/user_update/" + id, form))
        {
            yield return www.SendWebRequest();
            //Debug.Log(form.data.ToString());
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
                instance.update = JsonUtility.FromJson<updateInfo>(www.downloadHandler.text);
                instance.info.data.user.score = update.data.score;
                instance.info.data.user.settings.level = update.data.settings.level;
            }
        }


    }
    public IEnumerator UpdateSettings(string id, string sex, string hat, string hair, string top, string bottom, string shoe)
    {
        WWWForm form = new WWWForm();
        form.AddField("sex", sex);
        form.AddField("hat", hat);
        form.AddField("hair", hair);
        form.AddField("top", top);
        form.AddField("bottom", bottom);
        form.AddField("shoe", shoe);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/user_setting_update/" + id, form))
        {
            yield return www.SendWebRequest();
            //Debug.Log(form.data.ToString());
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
                instance.update = JsonUtility.FromJson<updateInfo>(www.downloadHandler.text);
                instance.info.data.user.settings.sex = update.data.settings.sex;
                instance.info.data.user.settings.hat = update.data.settings.hat;
                instance.info.data.user.settings.hair = update.data.settings.hair;
                instance.info.data.user.settings.top = update.data.settings.top;
                instance.info.data.user.settings.bottom = update.data.settings.bottom;
                instance.info.data.user.settings.shoe = update.data.settings.shoe;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }


    }

    public IEnumerator UpdateGender(string id, string sex)
    {
        WWWForm form = new WWWForm();
        form.AddField("sex", sex);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/user_setting_update/" + id, form))
        {
            yield return www.SendWebRequest();
            //Debug.Log(form.data.ToString());
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
                if (www.downloadHandler.text.Contains("false"))
                {
                    Debug.Log("Error Occured(404)");
                }
                else if (www.downloadHandler.text.Contains("200"))
                {
                    Debug.Log("User Registered Successfully");
                }
                instance.update = JsonUtility.FromJson<updateInfo>(www.downloadHandler.text);
                instance.info.data.user.settings.sex = update.data.settings.sex;

                if (!String.IsNullOrEmpty(UserController.instance.isNewUser))
                {
                    if (String.IsNullOrEmpty(UserController.instance.info.data.user.settings.top))
                    {

                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
                    }
                }

            }
        }
    }

    public IEnumerator UpdateSettings(string level, string id)
    {
        WWWForm form = new WWWForm();
        form.AddField("level", level);

        using (UnityWebRequest www = UnityWebRequest.Post("https://katapult-api.herokuapp.com/api/v1/user_setting_update/" + id, form))
        {
            yield return www.SendWebRequest();
            //Debug.Log(form.data.ToString());
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
                instance.update = JsonUtility.FromJson<updateInfo>(www.downloadHandler.text);
                instance.info.data.user.settings.level = update.data.settings.level;
            }
        }


    }
}
