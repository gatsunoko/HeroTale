using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class UGUIMenuItem : MonoBehaviour
{
    jInputSettings SetScript;
    UGUIMenuVerticalScroll VerticalScrollScript;
    UGUIButtonTextColor ButtonTextColorScript;
    [HideInInspector]
    public int MenuNum;
    string MenuNumString;
    int NameCheck;
    [HideInInspector]
    public Text InputText;
    GameObject ThisButtonGO;
    Image ThisButtonImageCpnt;
    Color ThisButtonBaseColor;
    Text ThisHeadingTextCpnt;
    Color ThisHeadingBaseColor;
    Color ThisAlertMarkBaseColor;
    GameObject AlertMarkGO;
    [HideInInspector]
    public bool AlertMarkCheck;
    [HideInInspector]
    public bool ScrollOverOutsideCheck;
    [HideInInspector]
    public bool ScrollUnderOutsideCheck;
    bool UseScrollCheck;
    bool ComparisonNotReadyCheck;
    RectTransform ScrollRangeRectTrns;
    Vector3[] ScrollRectCorners = new Vector3[4];
    Vector3[] ThisItemCorners = new Vector3[4];

    void Start()
    {
        if (SetScript == null)
            SetScript = GetComponentInParent<jInputSettings>();
        if (SetScript == null)
            Debug.LogError("[jInput] jInputSettings script is Not Found!!");
        NameCheck = gameObject.name.IndexOf("MapperMenuItem");
        if (NameCheck != 0 || gameObject.name.Length != 16)
        {
            Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
        }
        else
        {
            MenuNumString = gameObject.name.Substring(gameObject.name.Length - 2, 2);
            MenuNum = int.Parse(MenuNumString);
        }

        if (AlertMarkGO == null)
        {
            if (transform.Find("AlertMark"))
                AlertMarkGO = transform.Find("AlertMark").gameObject;
        }
        if (AlertMarkGO != null)
        {
            if (AlertMarkGO.GetComponent<Image>())
                ThisAlertMarkBaseColor = AlertMarkGO.GetComponent<Image>().color;
            #if (UNITY_EDITOR)
            if (EditorApplication.isPlaying || EditorApplication.isPaused)
            #endif
                AlertMarkGO.SetActive(false);
        }
        else
        {
            Debug.LogError("[jInput] Error!! AlertMark gameObject in " + this.gameObject.name + " is Not Found!");
        }

        if (ThisButtonGO == null)
            ThisButtonGO = transform.Find("Button").gameObject;
        if (ThisButtonGO == null)
        {
            Debug.LogError("[jInput] Error!! Button in " + this.gameObject.name + " is Not Found!");
        }
        else
        {
            if (ThisButtonGO.transform.Find("Text"))
            {
                if (ThisButtonGO.transform.Find("Text").GetComponent<Text>())
                    InputText = ThisButtonGO.transform.Find("Text").GetComponent<Text>();
                if (ThisButtonGO.transform.Find("Text").GetComponent<UGUIButtonTextColor>())
                    ButtonTextColorScript = ThisButtonGO.transform.Find("Text").GetComponent<UGUIButtonTextColor>();
            }
            if (InputText == null)
                Debug.LogError("[jInput] Error!! InputText in " + this.gameObject.name + " is Not Found!");
            if (ThisButtonGO.GetComponent<Image>())
                ThisButtonImageCpnt = ThisButtonGO.GetComponent<Image>();
            if (ThisButtonImageCpnt == null)
                Debug.LogError("[jInput] Error!! Image Component with Button in " + this.gameObject.name + " is Not Found!");
            else
                ThisButtonBaseColor = ThisButtonImageCpnt.color;
        }
        if (transform.Find("HeadingText") != null && transform.Find("HeadingText").GetComponent<Text>() != null)
        {
            ThisHeadingTextCpnt = transform.Find("HeadingText").GetComponent<Text>();
            ThisHeadingBaseColor = ThisHeadingTextCpnt.color;
        }
        if (ThisHeadingTextCpnt == null)
        {
            Debug.LogError("[jInput] Error!! ThisHeadingTextCpnt in " + this.gameObject.name + " is Not Found!");
        }
        if (VerticalScrollScript == null && GetComponentInParent<UGUIMenuVerticalScroll>())
        {
            VerticalScrollScript = GetComponentInParent<UGUIMenuVerticalScroll>();
        }
        if (VerticalScrollScript != null && VerticalScrollScript.UseVerticalScroll)
        {
            UseScrollCheck = true;
            ScrollRangeRectTrns = VerticalScrollScript.GetComponent<RectTransform>();
        }
        else
        {
            UseScrollCheck = false;
        }

        #if (UNITY_EDITOR)
        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            #endif
            HeadingTextPour();
            //スクロール範囲外で透明~半透明のitemがキー設定窓に移行した瞬間一瞬表示されてしまうので透明にしておく
            if (UseScrollCheck)
            {
                if (ThisButtonImageCpnt != null)
                    ThisButtonImageCpnt.color = Color.clear;
                if (ThisHeadingTextCpnt != null)
                    ThisHeadingTextCpnt.color = Color.clear;
                if (InputText != null)
                    InputText.color = Color.clear;
            }
            #if (UNITY_EDITOR)
        }
        #endif

    }

    void OnDisable()
    {
        #if (UNITY_EDITOR)
        if (EditorApplication.isPlaying || EditorApplication.isPaused)
        {
            #endif
            //スクロール範囲外で透明~半透明のitemがキー設定窓に移行した瞬間一瞬表示されてしまうので透明にしておく
            if (UseScrollCheck)
            {
                if (ThisButtonImageCpnt != null)
                    ThisButtonImageCpnt.color = Color.clear;
                if (ThisHeadingTextCpnt != null)
                    ThisHeadingTextCpnt.color = Color.clear;
                if (InputText != null)
                    InputText.color = Color.clear;
            }
            #if (UNITY_EDITOR)
        }
        #endif
    }

    void Update()
    {

        #if (UNITY_EDITOR)
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            if (SetScript == null)
                SetScript = GetComponentInParent<jInputSettings>();
            if (ThisHeadingTextCpnt == null && transform.Find("HeadingText").GetComponent<Text>() != null)
                ThisHeadingTextCpnt = transform.Find("HeadingText").GetComponent<Text>();
            HeadingTextPour();
            if (InputText == null)
                InputText = transform.Find("Button/Text").GetComponent<Text>();
            if (InputText != null)
                InputText.text = "Key Name";
        }
        else
        {
            #endif
            if (EventSystem.current.currentSelectedGameObject == this.gameObject
                || EventSystem.current.currentSelectedGameObject == ThisButtonGO)
                SetScript.CullentTextCpnt = InputText;

            if (AlertMarkGO != null)
            {
                if (AlertMarkCheck)
                    AlertMarkGO.SetActive(true);
                else
                    AlertMarkGO.SetActive(false);
            }
            #if (UNITY_EDITOR)
        }
        #endif
    }


    void LateUpdate()
    {
        //スクロール使用の時は範囲外に出たら選択状態にならないように処理
        if (UseScrollCheck && ScrollRangeRectTrns != null)
        {
            ScrollRangeRectTrns.GetWorldCorners(ScrollRectCorners);
            ComparisonNotReadyCheck = false;
        }
        else
        {
            ComparisonNotReadyCheck = true;
        }
        if (ThisButtonGO.GetComponent<RectTransform>() != null && ComparisonNotReadyCheck != true)
        {
            ThisButtonGO.GetComponent<RectTransform>().GetWorldCorners(ThisItemCorners);
            ComparisonNotReadyCheck = false;
        }
        else
        {
            ComparisonNotReadyCheck = true;
        }
        if (ComparisonNotReadyCheck != true)
        {
            if (ScrollRectCorners[2].y < (ThisItemCorners[2].y - 5))
            {
                ScrollOverOutsideCheck = true;
                ScrollUnderOutsideCheck = false;
                float OutsideVal = Mathf.Abs(ScrollRectCorners[2].y - ThisItemCorners[2].y) + 5;
                if (ButtonTextColorScript != null)
                    ButtonTextColorScript.SetTextAlphaRatio(OutsideVal * 0.04f);
                if (ThisButtonImageCpnt != null)
                {
                    ThisButtonImageCpnt.raycastTarget = false;
                    ThisButtonImageCpnt.color = new Color(ThisButtonBaseColor.r, ThisButtonBaseColor.g, ThisButtonBaseColor.b, ThisButtonBaseColor.a * (1.0f - (OutsideVal * 0.03f)));
                }
                if (ThisHeadingTextCpnt != null)
                    ThisHeadingTextCpnt.color = new Color(ThisHeadingBaseColor.r, ThisHeadingBaseColor.g, ThisHeadingBaseColor.b, ThisHeadingBaseColor.a * (1.0f - (OutsideVal * 0.04f)));
                if (AlertMarkGO.GetComponent<Image>())
                    AlertMarkGO.GetComponent<Image>().color = new Color(ThisAlertMarkBaseColor.r, ThisAlertMarkBaseColor.g, ThisAlertMarkBaseColor.b, ThisAlertMarkBaseColor.a * (1.0f - (OutsideVal * 0.04f)));
            }
            else if (ScrollRectCorners[0].y > (ThisItemCorners[0].y + 5))
            {
                ScrollOverOutsideCheck = false;
                ScrollUnderOutsideCheck = true;
                float OutsideVal = Mathf.Abs(ScrollRectCorners[0].y - ThisItemCorners[0].y) + 5;
                if (ButtonTextColorScript != null)
                    ButtonTextColorScript.SetTextAlphaRatio(OutsideVal * 0.04f);
                if (ThisButtonImageCpnt != null)
                {
                    ThisButtonImageCpnt.raycastTarget = false;
                    ThisButtonImageCpnt.color = new Color(ThisButtonBaseColor.r, ThisButtonBaseColor.g, ThisButtonBaseColor.b, ThisButtonBaseColor.a * (1.0f - (OutsideVal * 0.03f)));
                }
                if (ThisHeadingTextCpnt != null)
                    ThisHeadingTextCpnt.color = new Color(ThisHeadingBaseColor.r, ThisHeadingBaseColor.g, ThisHeadingBaseColor.b, ThisHeadingBaseColor.a * (1.0f - (OutsideVal * 0.04f)));
                if (AlertMarkGO.GetComponent<Image>())
                    AlertMarkGO.GetComponent<Image>().color = new Color(ThisAlertMarkBaseColor.r, ThisAlertMarkBaseColor.g, ThisAlertMarkBaseColor.b, ThisAlertMarkBaseColor.a * (1.0f - (OutsideVal * 0.04f)));
            }
            else
            {
                ScrollOverOutsideCheck = false;
                ScrollUnderOutsideCheck = false;
                if (ButtonTextColorScript != null)
                    ButtonTextColorScript.SetTextAlphaRatio(0.0f);
                if (ThisButtonImageCpnt != null)
                {
                    ThisButtonImageCpnt.raycastTarget = true;
                    ThisButtonImageCpnt.color = ThisButtonBaseColor;
                }
                if (ThisHeadingTextCpnt != null)
                    ThisHeadingTextCpnt.color = ThisHeadingBaseColor;
                if (AlertMarkGO.GetComponent<Image>() != null)
                    AlertMarkGO.GetComponent<Image>().color = ThisAlertMarkBaseColor;

            }
        }

    }


    void HeadingTextPour()
    {
        if (SetScript != null && ThisHeadingTextCpnt != null)
        {
            if (SetScript.MenuItemHeadings.Length > MenuNum)
                ThisHeadingTextCpnt.text = SetScript.MenuItemHeadings[MenuNum];
        }
    }

    public void OnSelectItemProcess()
    {
        if (SetScript != null && ScrollOverOutsideCheck != true && ScrollUnderOutsideCheck != true)
        {
            SetScript.OperateItemLine = false;
            SetScript.PreparedOperateSelectPos = 0;
            SetScript.OperateLineSelectPosition = 0;
            if (SetScript.MappingMode != true)
                SetScript.SelectPosition = MenuNum;
        }
    }

    public void OnClickItemProcess()
    {
        if (SetScript != null && ScrollOverOutsideCheck != true && ScrollUnderOutsideCheck != true)
        {
            OnSelectItemProcess();
            if (SetScript.MappingPointerUpPreventNotify())
                return;
            if (SetScript.MappingMode != true)
            {
                SetScript.KeyDelayCall();
                SetScript.MappingMode = true;
            }
        }
    }

}
