using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ABLoader : MonoBehaviour
{
    // Start is called before the first frame update
  //  private  GameObject instance;
    private AnimationClip anim;
    void Start()
    {
        
        
        
        
        
        StartCoroutine(LoadShaders());

      //  StartCoroutine(Load("avatarskeleton", PartLoaded));
    }
    public IEnumerator LoadAnim(string name)
    {
        string url = Application.streamingAssetsPath + "/"+name;
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();
        if(www.error!=string.Empty || www.error!="")
        {
            var handle = www.downloadHandler as DownloadHandlerAssetBundle;

          //  instance=   Instantiate<GameObject>(handle.assetBundle.LoadAsset<GameObject>(name));
          anim=   handle.assetBundle.LoadAsset<AnimationClip>(name);
        Debug.Log("加载了动画one_girl");
        }
        else
        {
            Debug.LogError(www.error);
        }

        yield return null;
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

        if (name != "avatarskeleton")
        {
            var player = GameObject.Find("AvatarSkeleton");
            // instance.transform.parent = GameObject.Find("AvatarSkeleton(Clone)").transform.Find("SkinRoot");
            Debug.Log(player.name);
        }
        
        
        

        yield return null;

    }

    public void PartLoaded(GameObject go)
    {

        var anims= go.GetComponent<Animation>();
        var clip = anim;
       if (anims.GetClip(clip.name) == null)
           anims.AddClip(clip, clip.name);
       //anims[i].clip = clip;
       // anims[i].Blend(anims[i].clip.name, 0.5f, 1.2f);
       anims.CrossFade(clip.name, 0.5f);
       Debug.Log(clip.name);
      //  go.GetComponent<runti>

        Debug.Log(go);
    }



    public void Loadwujian(GameObject go)
    {
        
    }
    
    
    public void LoadAnim(List<string> list, int index)
    {
        string name = list[index];


        //LoadPrefabFromAB.Instance.LoadAnimationClip(name, (clip) =>
        //{

        //    var animation = GameObject.FindObjectOfType<Animation>();
        //    if (animation.GetClip(clip.name) == null)
        //    {
        //        animation.AddClip(clip, clip.name);
        //    }

        //    animation.CrossFade(clip.name);

        //});

        LoadPrefabFromAB.Instance.LoadAnimationClip(name, animationClip);

    }
    
    private void animationClip(AnimationClip clip)
    {
        var anims = GameObject.Find("AvatarSkeleton").GetComponentInChildren<Animation>();

        if (anims.GetClip(clip.name) == null)
            anims.AddClip(clip, clip.name);
        //anims[i].clip = clip;
        // anims[i].Blend(anims[i].clip.name, 0.5f, 1.2f);
        anims.CrossFade(clip.name, 0.5f);
        Debug.Log(clip.name);
        
    } 
    
    
    
    
}
