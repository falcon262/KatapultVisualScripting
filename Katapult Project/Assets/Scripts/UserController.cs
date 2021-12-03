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
                    for (int j = i+1; j < instance.usersInfo.data.Count; j++)
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
