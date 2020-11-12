﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickSecond : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    [Header("Settings")]
    [SerializeField, Range(1, 15)] private float Radio = 5;//the ratio of the circumference of the joystick
    [SerializeField, Range(0.01f, 1)] private float SmoothTime = 0.5f;//return to default position speed
    [SerializeField, Range(0.5f, 4)] private float OnPressScale = 1.5f;//return to default position speed
    public Color NormalColor = new Color(1, 1, 1, 1);
    public Color PressColor = new Color(1, 1, 1, 1);
    [SerializeField, Range(0.1f, 5)] private float Duration = 1;

    public static bool isSecondJoystickUp = false;
   
    //Privates
    private Vector3 DeathArea;
    private Vector3 currentVelocity;  
    private int lastId = -2;
    private Image stickImage;
    private Image backImage;
    private Canvas m_Canvas;
    private float diff;
    private Vector3 PressScaleVector;
    private RectTransform StickRect;
    private RectTransform CenterReference;

    private delegate void AttackHandler();
    private event AttackHandler AttackNotify;


    void Start()
    {
        AttackNotify += GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Attack;
        StickRect = transform.Find("Stick").GetComponent<RectTransform>();
        CenterReference = transform.Find("Center").GetComponent<RectTransform>();

        if (StickRect == null)
        {
            Debug.LogError("Please add the stick for joystick work!.");
            this.enabled = false;
            return;
        }

        if (transform.root.GetComponent<Canvas>() != null)
        {
            m_Canvas = transform.root.GetComponent<Canvas>();
        }
        else if (transform.root.GetComponentInChildren<Canvas>() != null)
        {
            m_Canvas = transform.root.GetComponentInChildren<Canvas>();
        }
        else
        {
            Debug.LogError("Required at lest one canvas for joystick work.!");
            this.enabled = false;
            return;
        }

        //Get the default area of joystick
        DeathArea = CenterReference.position;
        diff = CenterReference.position.magnitude;
        PressScaleVector = new Vector3(OnPressScale, OnPressScale, OnPressScale);
        if (GetComponent<Image>() != null)
        {
            backImage = GetComponent<Image>();
            stickImage = StickRect.GetComponent<Image>();
            backImage.CrossFadeColor(NormalColor, 0.1f, true, true);
            stickImage.CrossFadeColor(NormalColor, 0.1f, true, true);
        }
    }

    void Update()
    {
        DeathArea = CenterReference.position;
        //If this not free (not touched) then not need continue
        if (!isSecondJoystickUp)
            return;
        //Return to default position with a smooth movement
        StickRect.position = Vector3.SmoothDamp(StickRect.position, DeathArea, ref currentVelocity, smoothTime);
        //When is in default position, we not need continue update this
        if (Vector3.Distance(StickRect.position, DeathArea) < .1f)
        {
            isSecondJoystickUp = false;
            StickRect.position = DeathArea;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        //Detect if is the default touchID
        if (lastId == -2)
        {
            //then get the current id of the current touch.
            //this for avoid that other touch can take effect in the drag position event.
            //we only need get the position of this touch
            lastId = data.pointerId;
            StopAllCoroutines();
            StartCoroutine(ScaleJoysctick(true));
            OnDrag(data);
            if (backImage != null)
            {
                backImage.CrossFadeColor(PressColor, Duration, true, true);
                stickImage.CrossFadeColor(PressColor, Duration, true, true);
            }
        }
    }

    public void OnDrag(PointerEventData data)
    {  
       
        //If this touch id is the first touch in the event
        if (data.pointerId == lastId)
        {
            isSecondJoystickUp = false;
            //Get Position of current touch
            Vector3 position = JoystickUtils.TouchPosition(m_Canvas, GetTouchID);

            //Rotate into the area circumferential of joystick
            if (Vector2.Distance(DeathArea, position) < radio)
            {
                StickRect.position = position;
            }
            else
            {
                StickRect.position = DeathArea + (position - DeathArea).normalized * radio;
            }
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        isSecondJoystickUp = true;
        AttackNotify?.Invoke();  //if(Notify !=null) Notify();
        currentVelocity = Vector3.zero;
        //leave the default id again
        if (data.pointerId == lastId)
        {
            //-2 due -1 is the first touch id
            lastId = -2;
            StopAllCoroutines();
            StartCoroutine(ScaleJoysctick(false));
            if (backImage != null)
            {
                backImage.CrossFadeColor(NormalColor, Duration, true, true);
                stickImage.CrossFadeColor(NormalColor, Duration, true, true);
            }
        }
    }

    IEnumerator ScaleJoysctick(bool increase)
    {
        float _time = 0;

        while (_time < Duration)
        {
            Vector3 v = StickRect.localScale;
            if (increase)
            {
                v = Vector3.Lerp(StickRect.localScale, PressScaleVector, (_time / Duration));
            }
            else
            {
                v = Vector3.Lerp(StickRect.localScale, Vector3.one, (_time / Duration));
            }
            StickRect.localScale = v;
            _time += Time.deltaTime;
            yield return null;
        }
    }

    public int GetTouchID
    {
        get
        {
            //find in all touches
            for (int i = 0; i < Input.touches.Length; i++)
            {
                if (Input.touches[i].fingerId == lastId)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    private float radio { get { return (Radio * 5 + Mathf.Abs((diff - CenterReference.position.magnitude))); } }
    private float smoothTime { get { return (1 - (SmoothTime)); } }

    public float Horizontal2
    {
        get
        {
            return (StickRect.position.x - DeathArea.x) / Radio;
        }
    }
    public float Vertical2
    {
        get
        {
            return (StickRect.position.y - DeathArea.y) / Radio;
        }
    }
}