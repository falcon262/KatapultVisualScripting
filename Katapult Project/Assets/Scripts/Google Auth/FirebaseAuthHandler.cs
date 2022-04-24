using Proyecto26;
using SimpleJSON;
using System;
using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Handles authentication calls to Firebase
/// </summary>

public static class FirebaseAuthHandler
{
    private const string ApiKey = "AIzaSyBgHSa_3-zvxW0DwrUrSU4nZBrbXJnJnQ0"; //TODO: Change [API_KEY] to your API_KEY

    /// <summary>
    /// Signs in a user with their Id Token
    /// </summary>
    /// <param name="token"> Id Token </param>
    /// <param name="providerId"> Provider Id </param>
    public static void SingInWithToken(string token, string providerId)
    {
        var payLoad =
            $"{{\"postBody\":\"id_token={token}&providerId={providerId}\",\"requestUri\":\"http://localhost\",\"returnIdpCredential\":true,\"returnSecureToken\":true}}";
        RestClient.Post($"https://identitytoolkit.googleapis.com/v1/accounts:signInWithIdp?key={ApiKey}", payLoad).Then(
            response =>
            {
                // You now have the userId (localId) and the idToken of the user!
                Debug.Log(response.Text);
                var obj = JSON.Parse(response.Text);
                foreach (var kvp in obj)
                {
                    if(kvp.Key == "fullName")
                    {
                        UserController.instance.fullName = kvp.Value.Value;
                        Debug.Log(UserController.instance.fullName);
                    }

                    if(kvp.Key == "localId")
                    {
                        UserController.instance.localId = kvp.Value.Value;
                        Debug.Log(UserController.instance.localId);
                    }
                    
                    if(kvp.Key == "originalEmail")
                    {
                        UserController.instance.authEmail = kvp.Value.Value;
                        Debug.Log(UserController.instance.authEmail);
                    }

                    if (kvp.Key == "isNewUser")
                    {
                        UserController.instance.isNewUser = kvp.Value.Value;
                        Debug.Log(UserController.instance.isNewUser);
                    }
                    //Debug.Log("Dict = " + kvp.Key + " : " + kvp.Value.Value);
                }
                if (String.IsNullOrEmpty(UserController.instance.isNewUser))
                {
                    UnityEngine.Object.FindObjectOfType<GameManager>().AuthSignIn();                
                }
                else
                {
                    UnityEngine.Object.FindObjectOfType<GameManager>().AuthSignUp();
                }

            }).Catch(Debug.Log);    
    }
}
