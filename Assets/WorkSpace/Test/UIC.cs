using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

using jagat.art;


public class UIC : MonoBehaviour
{

    public Texture2D _LutTex;

    public GameObject _go_start;


    public Button btn_Add, btn_Del ;


    public AnimationClip _lover_boy,_idle_boy;

    private float range_max = 30;
    private float range_min = -0.1f;

    public GameObject Boy;
    public GameObject[] Girls;

    public GameObject[] fx;
    private bool fx_offon;
    public Material[] Emi;
    private bool Emi_offon;
    private bool Anim_offon;
    private bool B_OffOn;

    private GameObject currentGirl;
    
    void ChangeAnimationToMe(AnimationClip clip)
    {
       var anims=   Boy.GetComponentsInChildren<Animation>();
        for (int i = 0; i < anims.Length; i++)
        {
            if(anims[i].GetClip(clip.name)==null)
                 anims[i].AddClip(clip, clip.name);
            //anims[i].clip = clip;
           // anims[i].Blend(anims[i].clip.name, 0.5f, 1.2f);
            anims[i].CrossFade(clip.name,0.5f );
        }

         anims = currentGirl.GetComponentsInChildren<Animation>();
        for (int i = 0; i < anims.Length; i++)
        {
            anims[i].Stop();
            
            anims[i].CrossFade(anims[i].clip.name, 0.5f);
        }

    }

    private void Update()
    {

        var pos=  GameObject.Find("facelightingcenter");
        if (pos !=null)
        {
            Shader.SetGlobalVector("_facelightingcenter", pos.transform.position);
        }
       
    }



    IEnumerator co_start()
    {
        currentGirl = Girls[Random.Range(0,2)];
        float r = range_min;
        while (r < range_max)
        {
            r += Time.deltaTime * 10 * range_max / 10f;
            if (r >= 3 && !currentGirl.activeSelf)
            {
                _go_start.SetActive(true);
                currentGirl.SetActive(true);
                ChangeAnimationToMe(_lover_boy);
                Emi_offon = true;
                LightOn();
                fx_offon = true;
                FxOn();
                Anim_offon = true;
                AnimationForStarsOff();
            }
          
            Shader.SetGlobalFloat("_Radiu", r);
            yield return new WaitForEndOfFrame();
        }
        
        yield return new WaitForSeconds(1);
        _go_start.SetActive(false);
       
        
        
    }

    IEnumerator co_remove()
    {
        float r = range_max;
        while(r> range_min)
        {
            r -= Time.deltaTime * 10* range_max/10f;
            Shader.SetGlobalFloat("_Radiu", r);
            if (r<=3 && currentGirl.activeSelf)
            {
                currentGirl.SetActive(false);
                ChangeAnimationToMe(_idle_boy);
                fx_offon = false;
                FxOn();
                Emi_offon = false;
                LightOn();
                Anim_offon = false;
                AnimationForStarsOff();
            }


            yield return new WaitForEndOfFrame();

        }

    }

    // Start is called before the first frame update
    void Start()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("LoveRoom"))
        {
            Shader.SetGlobalVector("_centerPos", new Vector3(-0.1761f, 3.66f, 0.70176f));
        }
        else if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Contains("merry_goround_final"))
        {
            Shader.SetGlobalVector("_centerPos", new Vector3(0.02f, -1.38f, -0.04f));
        }

        B_OffOn = true;
        Shader.SetGlobalFloat("_Style2", 1);
        // InitToggles();
        InitColorSelector();
        Shader.SetGlobalTexture("_LutTex", _LutTex);


       btn_Add.onClick.AddListener(() => {
           if (B_OffOn)
           {
               StartCoroutine(co_start());
           }
           else
           {
               return;
           }
           B_OffOn = false;   
        });


        btn_Del.onClick.AddListener(() => {
            
            if (!B_OffOn)
            {
                StartCoroutine(co_remove());
            }
            else
            {
                return;
            }
            B_OffOn = true;
        });

    }

    private void LightOn()
    {
        if (Emi.Length == 0)
        {
            return;
        }
        if (Emi_offon)
        {
            Emi[0].SetColor("_EmissionColor",new Color(0.746094f,0.625f,0.300781f)*2);
            Emi[1].SetColor("_EmissionColor",new Color(0.746094f,0.625f,0.300781f)*2);
        }
        else
        {
            Emi[0].SetColor("_EmissionColor",new Color(0.5f,0.5f,0.5f));
            Emi[1].SetColor("_EmissionColor",new Color(0.5f,0.5f,0.5f));
        }

    }

    private void FxOn()
    {

        for (int i = 0; i < fx.Length; i++)
        {
            if (fx_offon)
            {
                fx[i].SetActive(true);
            }
            else
            {
                fx[i].SetActive(false);
            }
             
        }
    }

    private void AnimationForStarsOff()
    {
        var Anima = GameObject.FindObjectsOfType<AnimationForStars>();
        for (int i = 0; i < Anima.Length; i++)
        {
            if (Anim_offon)
            {
                Anima[i].enabled = true;
            }
            else
            {
                Anima[i].enabled = false; 
            }
        }

        
        
    }
    //=====================================

    public ColorSelector colorSelector;
    public Button btn_colorSelector;
    private void InitColorSelector()
    {
        colorSelector.gameObject.SetActive(false);
        btn_colorSelector.onClick.AddListener(() => {
            colorSelector.gameObject.SetActive(!colorSelector.gameObject.activeSelf);

            if(colorSelector.gameObject.activeSelf)
            {
                GameObject.FindObjectOfType<UserController>().enabled = false;
            }
            else
            {
                GameObject.FindObjectOfType<UserController>().enabled = true;
            }

        });
    }
    private void InitToggles()
    {
        Shader.SetGlobalFloat("_Radiu",-0.1f);


        var group=   GameObject.FindObjectOfType<ToggleGroup>();
        var tg= group.GetFirstActiveToggle();


        if(tg.name.Contains("soft"))
        {
           // range_max = 30;
            Shader.SetGlobalFloat("_Style2", 1);
        }
        else
        {
           // range_max = 10;
            Shader.SetGlobalFloat("_Style2", 0);
        }


       var tgs= GameObject.FindObjectsOfType<Toggle>();

        foreach (var t in tgs)
        {
            t.onValueChanged.AddListener((v) => {

                if (v)
                {
                   
                    if (t.name.Contains("soft"))
                    {
                      //  range_max = 30;
                        Shader.SetGlobalFloat("_Style2", 1);
                    }
                    else
                    {
                      //  range_max = 10;
                        Shader.SetGlobalFloat("_Style2", 0);
                    }
                }
              

            });
        }


    }





}
