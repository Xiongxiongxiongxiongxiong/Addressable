using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UISceneController : MonoBehaviour
{

    #region ÓÃ»§²Ù¿Ø

       // public bool isUIScene;
        public Camera m_camera;
        public Transform world;
    public float dragspeed=0.05f;
    

       
        [Space(10)]

        public bool useOrthographic=true;

        [Space(10)]
        [HideInInspector]
        public float MaxFOV = 120;
        [HideInInspector]
        public float MinFOV = 30;

        [Space(10)]
        public float MaxSize = 10;
        public float MinSize = 3.17f;
        [Space(10)]
        public float MoveRange_x=2, MoveRange_y=2;

        //===============================private
        private float origin_scale;
      
        private Vector2 move_total;
        private Coroutine co, co2;
    //===============================private
    #endregion
    
 

  


    private bool GetTouchBegin()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonDown(0);
#else
        return Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Began;
#endif
    }

    private bool GetTouchMoved()
    {
#if UNITY_EDITOR
        return Input.GetMouseButton(0);
#else
        return Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Moved;
#endif
    }

    private bool GetTouchEnd()
    {
#if UNITY_EDITOR
        return Input.GetMouseButtonUp(0);
#else
        return Input.touchCount == 1 && Input.touches[0].phase == TouchPhase.Ended;
#endif
    }

    private Vector2 GetAxis()
    {
#if UNITY_EDITOR
        return  new Vector2( Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) ;
#else
        return Input.touches[0].deltaPosition *0.1f;
#endif
    }

    private void Start()
    {
       
       // Shader.SetGlobalFloat("_Radiu", 10);
        Shader.SetGlobalFloat("_LutIntensity", 0);
        m_camera.orthographic = useOrthographic;
    }
    // Update is called once per frame
    void LateUpdate()
    {

        if (GetTouchBegin())
        {
            if (co != null)
            {
                StopCoroutine(co);
                co = null;
            }
        }

        if(GetTouchMoved())
        {
           if( (!useOrthographic && m_camera.fieldOfView<=30 ) || (useOrthographic && m_camera.orthographicSize <= MinSize+(MaxSize-MinSize)*0.5f))
            {
                move_total += GetAxis()* dragspeed;
             
                move_total.x = Mathf.Clamp(move_total.x, -MoveRange_x, MoveRange_x);
                move_total.y = Mathf.Clamp(move_total.y, -MoveRange_y, MoveRange_y);

                MoveWorld(move_total);
            }
          

          



        }
        
        //if(GetTouchEnd())
        //{
        //    if(rotate_total.sqrMagnitude>0)
        //    {
        //       co= StartCoroutine(Co_rotate());
        //    }
        //}
        //===================================================

        if(Input.touchCount>=2  )
        {

            if (Input.touches[1].phase== TouchPhase.Began)
            {
                if(co2!=null)
                {
                    StopCoroutine(co2);
                    co2 = null;
                }

                origin_scale = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);
            }
            else if (Input.touches[1].phase== TouchPhase.Moved)
            {
                float _scale = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                float viewScale =   origin_scale/ _scale;
                if (viewScale > 1.05f) viewScale = 1.05f;
                if (viewScale < 0.95f) viewScale = 0.95f;

                if(useOrthographic)
                {
                    if (viewScale > 1 && m_camera.orthographicSize > MaxSize)
                        viewScale = 1;
                    if (viewScale < 1 && m_camera.orthographicSize < MinSize)
                        viewScale = 1;
                    m_camera.orthographicSize = m_camera.orthographicSize * viewScale;
                    origin_scale = _scale;

                    float m_scale = Mathf.Clamp01((m_camera.orthographicSize - MinSize) / MinSize);
                    world.position = Vector3.Lerp(world.position, Vector3.zero, m_scale);
                    move_total = world.position;
                }
                else
                {
                    if (viewScale > 1 && m_camera.fieldOfView > MaxFOV)
                        viewScale = 1;
                    if (viewScale < 1 && m_camera.fieldOfView < MinFOV)
                        viewScale = 1;
                    m_camera.fieldOfView = m_camera.fieldOfView * viewScale;
                    origin_scale = _scale;

                    float m_scale = Mathf.Clamp01((m_camera.fieldOfView - MinFOV) / MinFOV);
                    world.position = Vector3.Lerp(world.position, Vector3.zero, m_scale);
                    move_total = world.position;
                }

               

            }
            else if (Input.touches[1].phase == TouchPhase.Ended || Input.touches[0].phase== TouchPhase.Ended || Input.touches[1].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Canceled)
            {

                if(useOrthographic)
                {
                    if (m_camera.orthographicSize >= MaxSize && co2 == null)
                    {
                        co2 = StartCoroutine(Co_scale(MaxSize - 1f));
                    }
                    else if (m_camera.orthographicSize <= MinSize && co2 == null)
                    {
                        co2 = StartCoroutine(Co_scale(MinSize + 0.5f));
                    }
                }
                else
                {
                    if (m_camera.fieldOfView >= MaxFOV && co2 == null)
                    {
                        co2 = StartCoroutine(Co_scale(MaxFOV - 5));
                    }
                    else if (m_camera.fieldOfView <= MinFOV && co2 == null)
                    {
                        co2 = StartCoroutine(Co_scale(MinFOV + 5));
                    }
                }
               
            }



        }





    }

    //IEnumerator Co_rotate()
    //{
    //    while (rotate_total.sqrMagnitude > 0)
    //    {
    //        rotate_total -= rotate_total * 1.0f / 30;
    //        RotateWorld(rotate_total);
    //        yield return new WaitForSeconds(0.0167f);

    //    }
    //}
    IEnumerator Co_scale(float fv)
    {
        float t = 0;
        while (t<1)
        {
            t += 0.0167f;
            yield return new WaitForSeconds(0.0167f);
            if(useOrthographic)
                m_camera.orthographicSize = Mathf.SmoothStep(m_camera.orthographicSize, fv, t);
            else
                m_camera.fieldOfView = Mathf.SmoothStep(m_camera.fieldOfView, fv, t);
        }
    }
   
    private void RotateWorld(Vector2 rotate)
    {
        world.localRotation= Quaternion.Euler(rotate.y, 0,0);
       world.rotation *= Quaternion.Euler(0, -rotate.x, 0);
    }
    private void MoveWorld(Vector2 move)
    {
        world.position = new Vector3( move.x, move.y,0);
    }

}
