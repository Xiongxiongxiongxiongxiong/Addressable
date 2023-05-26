using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ABLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadShaders());

        StartCoroutine(Load("avatarskeleton", PartLoaded));
    }

    private IEnumerator LoadShaders()
    {
        string url = Application.streamingAssetsPath + "/avatar_shaders" ;
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();
        if (www.error != string.Empty || www.error != "")
        {
            var handle = www.downloadHandler as DownloadHandlerAssetBundle;

            handle.assetBundle.LoadAllAssets();

        }
        else
        {
            Debug.LogError(www.error);
        }

        yield return null;
    }

    public IEnumerator Load(string name, Action<GameObject> callback)
    {
        string url = Application.streamingAssetsPath + "/"+name;
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();
        if(www.error!=string.Empty || www.error!="")
        {
            var handle = www.downloadHandler as DownloadHandlerAssetBundle;

            var instance=   Instantiate<GameObject>(handle.assetBundle.LoadAsset<GameObject>(name));

            if(callback!=null)
            {
                callback(instance);
            }
        }
        else
        {
            Debug.LogError(www.error);
        }

        yield return null;

    }

    private void PartLoaded(GameObject go)
    {

      //  go.GetComponent<Animation>()

      //  go.GetComponent<runti>

        Debug.Log(go);
    }

}
