using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using jagat.art;
public class LoadPrefabFromAB : MonoBehaviour
{

    public static LoadPrefabFromAB Instance;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
    }

    public void LoadPart(string name, string type, Action<GameObject,string> action)
    {
        StartCoroutine(LoadPart_co(name, type, action));
    }

    public void LoadMaskTexture(string name,Action<Texture2D> action)
    {
        StartCoroutine(LoadMask_co(name, action));
    }
    public void LoadAnimationClip(string name, Action<AnimationClip> action)
    {
        StartCoroutine(LoadAnimationClip_co(name, action));
    }


    public void LoadAvatar(string name, string type, Action<GameObject, string> action)
    {
        StartCoroutine(LoadPart_co(name, type, action));
    }



    // Start is called before the first frame update
    IEnumerator LoadPart_co(string name,string type, Action<GameObject, string> callback)
    {

         var handle=   Addressables.LoadAssetAsync<GameObject>(name);
         yield return handle;
        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            if (callback != null)
                callback(handle.Result, type);
        }

        yield return null;
    }
    IEnumerator LoadMask_co(string name, Action<Texture2D> callback)
    {

        var handle = Addressables.LoadAssetAsync<Texture2D>(name);
        yield return handle;
        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            if (callback != null)
                callback(handle.Result);
        }

        yield return null;
    }

    IEnumerator LoadAnimationClip_co(string name, Action<AnimationClip> callback)
    {

        var handle = Addressables.LoadAssetAsync<AnimationClip>(name);
        yield return handle;
        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            if (callback != null)
                callback(handle.Result);
        }

        yield return null;
    }

    // Update is called once per frame

#if UNITY_EDITOR
    void Update()
    {
          if(Input.GetKeyDown(KeyCode.Space))
        {
            LoadPart("TOPS00_UB","top", LoadPart_Callback);
        }
    }

    private void LoadPart_Callback(GameObject go, string type)
    {
        var part = Instantiate(go);
        part.transform.parent = GameObject.Find("AvatarSkeleton").transform.Find("SkinRoot");
        part.GetComponent<LoadBoneinfoRuntime>().boneRoot = GameObject.Find("AvatarSkeleton").transform.Find("Bip001");
        part.GetComponent<LoadBoneinfoRuntime>().enabled = true;
    }

#endif




}
