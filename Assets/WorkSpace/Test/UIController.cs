using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using jagat.art;

public class UIController : MonoBehaviour
{
    public Button btn_top_next, btn_top_prev;
    public Button btn_pants_next, btn_pants_prev;
    public Button btn_dress_next, btn_dress_prev;
    public Button btn_shoes_next, btn_shoes_prev;
    public Button btn_hair_next, btn_hair_prev;
    public Button btn_jewelry_next, btn_jewelry_prev;
    public Button btn_anim_next, btn_anim_prev;

    public Button btn_boy, btn_girl;
    private GameObject currTop, currPants, currShoes, currJewelry,currHair, currDress,currBoy, currGirl,anim;
    private GameObject eye, brow, mouth;

    private Material material;

    private int topIndex = -1;
    private int pantsIndex = -1;
    private int dressIndex = -1;
    private int shoesIndex = -1;
    private int hairIndex = -1;
    private int jewelryIndex = -1;
    private int animIndex = -1;
    public Text text;
    private string state_top = "暂无", state_shoe = "暂无", state_pant = "暂无", state_jewelry = "暂无", state_hair = "暂无", state_dress = "暂无", state_anim = "anim_OneGirl_MerryGoround";
    private List<GameObject> faceparts = new List<GameObject> { };
    /// <summary>
    /// 衣服部件对应身体的遮罩图
    /// </summary>
    private Dictionary<string, string> topDic = new Dictionary<string, string> { 
        //衣服
        {"TOPS00_UB","UB_Mask_01_duanxiu" },
        {"TOPS02_UB","UB_Mask_03_beixin" },
        {"TOPS03_UB","UB_Mask_02_changxiu" },
        {"TOPS04_UB","UB_Mask_02_changxiu" },
        {"TOPS05_UB","UB_Mask_02_changxiu" },
        {"TOPS07_UB","UB_Mask_01_duanxiu" },
        {"TOPS16_UB","UB_Mask_01_duanxiu" },
        {"TOPS17_UB","UB_Mask_01_duanxiu" },
        {"TOPS19_UB","UB_Mask_01_duanxiu" },
        {"TOPS20_UB","UB_Mask_01_duanxiu" },
        {"TOPS25_UB","UB_Mask_03_beixin" },
        {"TOPS26_UB","UB_Mask_01_duanxiu" },
        {"TOPS27_UB","UB_Mask_02_changxiu" },

    };
    private Dictionary<string, string> pantsDic = new Dictionary<string, string> { 
       
        //裤子
        {"BOTTOM00_LB","LB_Mask_01_duanku" },
        {"BOTTOM02_LB","LB_Mask_01_duanku" },
        {"BOTTOM03_LB","LB_Mask_01_duanku" },
        {"BOTTOM04_LB","LB_Mask_01_duanku" },
        {"BOTTOM05_LB","LB_Mask_01_changku" },
        {"BOTTOM07_LB","LB_Mask_01_changku" },
        {"BOTTOM16_LB","LB_Mask_01_duanku" },
        {"BOTTOM17_LB","LB_Mask_01_duanku" },
        {"BOTTOM19_LB","LB_Mask_01_duanku" },
        {"BOTTOM20_LB","LB_Mask_01_duanku" },
        {"BOTTOM25_LB","LB_Mask_01_duanku" },
        {"BOTTOM26_LB","LB_Mask_01_changku" },
        {"BOTTOM27_LB","LB_Mask_01_changku" },
        {"BOTTOM627_LB","LB_Mask_01_changku" },

    };
    private Dictionary<string, string> dressDic = new Dictionary<string, string> { 
       
        //连衣裙
        {"DRESS04_HB","LB_Mask_01_changqun04" },
        {"DRESS05_HB","LB_Mask_01_changqun05" },
        {"DRESS09_HB","LB_Mask_01_changqun09" },
        {"DRESS10_HB","LB_Mask_01_duanku" },
    };
    private List<string> shoesList = new List<string> {
        "SHOES03_SO",
        "SHOES04_SO",
        "SHOES05_SO",
        "SHOES06_SO",
        "SHOES08_SO",
        "SHOES16_SO",
        "SHOES17_SO",
        "SHOES19_SO",
        "SHOES20_SO",
        "SHOES23_SO",
        "SHOES25_SO",
        "SHOES30_SO",
        "SHOES31_SO",
        "SHOES31_SO_01",
        "SHOES31_SO_02",
        "SHOES31_SO_03",
        "SHOES32_SO",
        "SHOES33_SO",
        "SHOES627_SO",
        "SHOSE24_SO",
        "SOCK00_SK",

    };
    private List<string> hairList = new List<string>
    {"HAIR03_HR","HAIR04_HR","HAIR04x_HR","HAIR05_HR","HAIR06_HR","HAIR08_HR","HAIR15_HR","HAIR16_HR","HAIR18_HR","HAIR19_HR","HAIR23_HR","HAIR24_HR","HAIR28_HR","HAIR32_HR","HAIR33_HR","HAIR34_HR","HAIR39x_HR","HAIR56_HR","HAIR76_HR","HAIR80_HR",

    };
    private List<string> jewelryList = new List<string>
    {
        "EARRING03_EAC", "EARRING04_EAC", "EARRING06_EAC", "EARRING07_EAC", "EARRING11_EAC", "GLASS02_GLS", "GLASS03_GLS", "GLASS04_GLS", "GLASS05_GLS", "GLASS10_GLS", "GLASS11_GLS", "GLASS12_GLS", "GLASS14_GLS", "GLASS16_GLS", "HAT02_HT", "HAT03_HT","HAT04_HT","HAT05_HT","HAT07_HT","HAT14_HT","HAT22_HT","TOY02_HT",
    };
    private List<string> animList = new List<string> 
    { "one_girl","three_girl_1","three_girl_2","three_girl_3",
        "two_girl_1","two_girl_2","boy_idle","boy_lover","girl_lover"
    };

    private void Awake()
    {
        text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {


        btn_top_next.onClick.AddListener(() =>
        {
          
            topIndex = (++topIndex) % topDic.Count;
            LoadDic(topIndex,"top");

        });

        btn_top_prev.onClick.AddListener(()=> {
            

            topIndex = ((--topIndex)+topDic.Count) % topDic.Count;
            LoadDic(topIndex,"top");


        });

        btn_pants_next.onClick.AddListener(() =>
        {

            pantsIndex = (++pantsIndex) % pantsDic.Count;
            LoadDic(pantsIndex,"pants");
        });

        btn_pants_prev.onClick.AddListener(() => {


            pantsIndex = ((--topIndex) + pantsDic.Count) % pantsDic.Count;
            LoadDic(pantsIndex,"pants");


        });


        btn_dress_next.onClick.AddListener(() =>
        {

            dressIndex = (++dressIndex) % dressDic.Count;
            LoadDic(dressIndex,"dress");
        });

        btn_dress_prev.onClick.AddListener(() => {


            dressIndex = ((--dressIndex) + dressDic.Count) % dressDic.Count;
            LoadDic(dressIndex,"dress");


        });

        btn_shoes_next.onClick.AddListener(() =>
        {

            shoesIndex = (++shoesIndex) % shoesList.Count;
            LoadList(shoesList,   shoesIndex, "shoes");
        });

        btn_shoes_prev.onClick.AddListener(() => {


            shoesIndex = ((--shoesIndex) + shoesList.Count) % shoesList.Count;
            LoadList(shoesList,shoesIndex, "shoes");


        });
        btn_hair_next.onClick.AddListener(() =>
        {

            hairIndex = (++hairIndex) % hairList.Count;
            LoadList(hairList, hairIndex, "hair");
        });

        btn_hair_prev.onClick.AddListener(() => {


            hairIndex = ((--hairIndex) + hairList.Count) % hairList.Count;
            LoadList(hairList, hairIndex, "hair");


        });


        btn_jewelry_next.onClick.AddListener(() =>
        {

            jewelryIndex = (++jewelryIndex) % jewelryList.Count;
            LoadList(jewelryList, jewelryIndex, "jewelry");
        });

        btn_jewelry_prev.onClick.AddListener(() => {


            jewelryIndex = ((--jewelryIndex) + jewelryList.Count) % jewelryList.Count;
            LoadList(jewelryList, jewelryIndex, "jewelry");


        });

        btn_girl.onClick.AddListener(() =>
        {
            LoadPrefabFromAB.Instance.LoadPart("BROW03_BW", "brow", InitPart);
            LoadPrefabFromAB.Instance.LoadPart("EYE03_EY", "eye", InitPart);
            LoadPrefabFromAB.Instance.LoadPart("MOUTH05_MTH", "mouth", InitPart);

        });

        btn_boy.onClick.AddListener(() => {

            LoadPrefabFromAB.Instance.LoadPart("BROW34_BW", "brow", InitPart);
            LoadPrefabFromAB.Instance.LoadPart("EYE27_EY", "eye", InitPart);
            LoadPrefabFromAB.Instance.LoadPart("MOUTH24_MTH", "mouth", InitPart);
        });

        material = GameObject.Find("BODY_ST").GetComponent<SkinnedMeshRenderer>().material;



        btn_anim_next.onClick.AddListener(() =>
        {

            animIndex = (++animIndex) % animList.Count;
            LoadAnim(animList, animIndex);
            text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
        });

        btn_anim_prev.onClick.AddListener(() => {


            animIndex = ((--animIndex) + animList.Count) % animList.Count;
            LoadAnim(animList, animIndex);

            text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
        });




        // Debug.Log(boyList.Count);
    }

    

    private void LoadAnim(List<string> list, int index)
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

    private void LoadList(List<string> list,int index ,string type)
    {
        
        string name = list[index] ;
        

        LoadPrefabFromAB.Instance.LoadPart(name, type, InitPart);
    }
    private void LoadDic(int Index, string type)
    {
        int i = 0;
        string name = "";
        var dic = new Dictionary<string, string> { };

        if (type == "top")
        {
            dic = topDic;
        }
        if (type == "pants")
        {
            dic = pantsDic;
        }
        if (type == "dress")
        {
            dic = dressDic;
        }

        foreach (var key in dic.Keys)
        {
            if (Index == i)
            {
                name = key;
                break;
            }
            i++;
        }

        if (dic.ContainsKey(name))
        {
            string dressMaskName = dic[name];

            LoadPrefabFromAB.Instance.LoadMaskTexture(dressMaskName,(mask)=> {
                if (dic == topDic)
                {
                    material.SetTexture("_MaskTexture_3", null);
                    material.SetTexture("_MaskTexture_1", mask);
                }
                if (dic == pantsDic)
                {
                    material.SetTexture("_MaskTexture_3", null);
                    material.SetTexture("_MaskTexture_2", mask);
                }
                if (dic == dressDic)
                {
                    material.SetTexture("_MaskTexture_3", mask);
                    material.SetTexture("_MaskTexture_1", null);
                    material.SetTexture("_MaskTexture_2", null);
                }

            });
        }
        LoadPrefabFromAB.Instance.LoadPart(name, type, InitPart);
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

    /// <summary>
    /// ======================================================================
    /// </summary>
    /// <param name="go"></param>
    /// <param name="type"></param>
    private void InitPart(GameObject go, string type)
    {

        var part = Instantiate(go);
        part.transform.parent = GameObject.Find("AvatarSkeleton").transform.Find("SkinRoot");
        var lbr = part.transform.GetComponentsInChildren<LoadBoneinfoRuntime>();
        foreach (var item in lbr)
        {
            item.boneRoot = GameObject.Find("AvatarSkeleton").transform.Find("Bip001");
            item.enabled = true;
        }
        switch(type)
        {
            case "top": 
                if(currTop != null ) DestroyImmediate(currTop);
                if (currDress != null) DestroyImmediate(currDress);
                currTop = part;
                state_top = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "pants":
                if (currPants != null) DestroyImmediate(currPants);
                if (currDress != null) DestroyImmediate(currDress);
                currPants = part;
                state_pant = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "shoes":
                if (currShoes != null) DestroyImmediate(currShoes);
                currShoes = part;
                state_shoe = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "jewelry":
                if (currJewelry != null) DestroyImmediate(currJewelry);
                currJewelry = part;
                state_jewelry = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "hair":
                if (currHair != null) DestroyImmediate(currHair);
                currHair = part;
                state_hair = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "dress":
                if (currDress != null) DestroyImmediate(currDress);
                if (currPants != null) DestroyImmediate(currPants);
                if (currTop != null) DestroyImmediate(currTop);
                currDress = part;
                state_dress = part.name;
                text.text = state_top.ToString() + "\n" + state_shoe.ToString() + "\n" + state_pant.ToString() + "\n" + state_jewelry + "\n" + state_hair.ToString() + "\n" + state_dress.ToString() + "\n" + state_anim.ToString();
                break;
            case "eye":
                if (eye != null) DestroyImmediate(eye);
                 eye = part;
                break;
            case "brow":
                if (brow != null) DestroyImmediate(brow);
                brow = part;
                break;
            case "mouth":
                if (mouth != null) DestroyImmediate(mouth);
                mouth = part;
                break;





            default: break;
        }

    }
}
