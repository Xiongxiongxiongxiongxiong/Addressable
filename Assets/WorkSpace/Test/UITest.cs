using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using jagat.art;

public class UITest : MonoBehaviour
{
    public Button btn_Add, btn_Del;
    private GameObject avatar;
    // Start is called before the first frame update
    void Start()
    {
        btn_Add.onClick.AddListener(() => {
           var room= GameObject.FindObjectOfType<InteractiveRoom>();

            room.AddPlayer(null);
         
        });


        btn_Del.onClick.AddListener(() => {

            var room = GameObject.FindObjectOfType<InteractiveRoom>();
            room.RemovePlayer("");

        });

    }

        // Update is called once per frame
        void Update()
    {
        
    }
}
