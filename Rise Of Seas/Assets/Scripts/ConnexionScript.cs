using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnexionScript : MonoBehaviour {

    public string SERVER_URL;

    public InputField usernameText;
    public InputField passwordText;

    public void Connect()
    {
        StartCoroutine(TryConnect());
    }

    IEnumerator TryConnect()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", usernameText.text);
        form.AddField("password", passwordText.text);

        UnityWebRequest www = UnityWebRequest.Post(SERVER_URL + "/gameAuth.php", form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }


        Debug.Log(System.Text.Encoding.UTF8.GetString(www.downloadHandler.data));

    }
}
