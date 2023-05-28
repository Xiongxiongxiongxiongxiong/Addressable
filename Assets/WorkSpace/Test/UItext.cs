using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItext : MonoBehaviour
{
    private List<string> animList = new List<string> 
    { "one_girl","three_girl_1","three_girl_2","three_girl_3",
        "two_girl_1","two_girl_2","boy_idle","boy_lover","girl_lover"
    };
    // Start is called before the first frame update
    private ABLoader sc;
    void Start()
    {
         sc = GetComponent<ABLoader>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(  sc.LoadAnim("one_girl"));
            StartCoroutine(sc.Load("avatarskeleton", sc.PartLoaded));
          //  Debug.Log("按下A了");
        }

        if (Input.GetKey(KeyCode.B))
        {
            StartCoroutine(sc.Load("TOPS26_UB", sc.PartLoaded));
            
            
        }
    }
    
    
    
}
