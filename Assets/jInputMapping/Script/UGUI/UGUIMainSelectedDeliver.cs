using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UGUIMainSelectedDeliver : MonoBehaviour
{

    [SerializeField, HideInInspector]
    GameObject
        FirstSelectedMenuItem;
    [Space(5)]
    public GameObject
        MenuItemsGO;
    [Space(2)]
    public GameObject
        OperateItemsGO;
    GameObject PrevSelectedMenuItem;
    GameObject PrevSelectedGO;
    jInputSettings SetScript;
    UGUIMenuVerticalScroll VerticalScrollScript;
    GameObject[] MenuItemButtonGOsAry;
    GameObject[] OperateItemsAry;
    bool FirstSelectCheck;
    bool StartSelectNullCheck;
    bool ReturnSelectCheck;
    bool FromMostOverCheck;
    bool FromMostUnderCheck;
    bool EndToEndCheck;
    Vector3[] ScrollRectCorners = new Vector3[4];
    Vector3[] CurrentItemCorners = new Vector3[4];
    GameObject CurrentSelectedGO;
    int CurrentItemNum;
    bool OverIntoRectCheck;
    bool UnderIntoRectCheck;
    float ScrollIntoRatio = 0.2f;
    float ScrollIntoInertia = 0.1f;
    float ScrollSpeedValue;

    void Start()
    {
        if (SetScript == null)
            SetScript = GetComponentInParent<jInputSettings>();
        if (SetScript == null)
        {
            Debug.LogError("[jInput] jInputSettings script is Not Found!!");
        }
        MenuItemButtonGOsAry = new GameObject[SetScript.MenuItemHeadings.Length];
        OperateItemsAry = new GameObject[4];
        FirstSelectCheck = true;
        StartSelectNullCheck = true;
        FromMostOverCheck = false;
        FromMostUnderCheck = false;
        EndToEndCheck = false;
        IntoRectFalse();

        if (MenuItemsGO != null)
        {
            for (int i = 0; i < MenuItemButtonGOsAry.Length; i++)
            {
                for (int j = 0; j < MenuItemsGO.transform.childCount; j++)
                {
                    string TempFindName = null;
                    GameObject TempItem = MenuItemsGO.transform.GetChild(j).gameObject;
                    if (0 <= i && i <= 9)
                        TempFindName = "MapperMenuItem0" + i;
                    else if (i >= 10)
                        TempFindName = "MapperMenuItem" + i;
                    if (TempItem.name == TempFindName)
                    {
                        if (TempItem.transform.Find("Button"))
                            MenuItemButtonGOsAry[i] = TempItem.transform.Find("Button").gameObject;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("[jInput] Error!! OperateItemsGO is Not Found!!");
        }
        if (MenuItemButtonGOsAry[0] != null)
            FirstSelectedMenuItem = MenuItemButtonGOsAry[0];
        else
            FirstSelectedMenuItem = transform.parent.Find("InMapperMenuItems/MapperMenuItem00/Button").gameObject;
        if (FirstSelectedMenuItem != null)
            PrevSelectedMenuItem = FirstSelectedMenuItem;
        else
            Debug.LogError("[jInput] Error!! Button in MapperMenuItem00 is Not Found!!");

        if (OperateItemsGO != null)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < OperateItemsGO.transform.childCount; j++)
                {
                    GameObject TempItem = OperateItemsGO.transform.GetChild(j).gameObject;
                    if (TempItem.name == "MapperOperateItem0" + i)
                    {
                        OperateItemsAry[i] = TempItem;
                        break;
                    }
                }
                if (0 <= i && i < 3)
                {
                    if (OperateItemsAry[i] == null)
                        Debug.LogError("[jInput] Error!! MapperOperateItem0" + i + " is Not Found!!");
                }
            }
            OperateItemsSetNonInteractable();
        }
        else
        {
            Debug.LogError("[jInput] Error!! OperateItemsGO is Not Found!!");
        }
        if (transform.GetComponentInParent<UGUIMenuVerticalScroll>())
        {
            VerticalScrollScript = transform.GetComponentInParent<UGUIMenuVerticalScroll>();
            VerticalScrollScript.GetComponent<ScrollRect>().verticalNormalizedPosition = 1;
        }
    }

    void OnEnable()
    {
        Start();
    }

    void OnDisable()
    {
        OperateItemsSetNonInteractable();
    }

    void Update()
    {
        if (StartSelectNullCheck)
        {
            StartSelectNullCheck = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        if (VerticalScrollScript != null)
        {
            ScrollIntoRatio = VerticalScrollScript.ScrollIntoRatio;
            ScrollIntoInertia = VerticalScrollScript.ScrollIntoInertia;
        }
        if (SetScript == null)
            return;

        if (SetScript.FirstSet || SetScript.MappingMode || SetScript.WindowSituation != 0)
        {
            OperateItemsSetNonInteractable();
            if (SetScript.WindowSituation != 0 && SetScript.FirstSet != true)
                ReturnSelectCheck = true;
        }
        else
        {
            if (SetScript.CurrentryRestore)
                OperateItemsSetSameSaveData();
            else
                OperateItemsSetNormal();

            if (FirstSelectCheck)
            {
                EventSystem.current.SetSelectedGameObject(FirstSelectedMenuItem);
                FirstSelectCheck = false;
                ReturnSelectCheck = false;
            }
            else if (ReturnSelectCheck)
            {
                if (PrevSelectedGO != null)
                    EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
                else
                    EventSystem.current.SetSelectedGameObject(FirstSelectedMenuItem);
                ReturnSelectCheck = false;
            }
        }
        CurrentSelectedGO = EventSystem.current.currentSelectedGameObject;

        //選択状態の項目の位置がスクロール窓の範囲外になったら常に選択項目が範囲内にあるようにする処理
        if (VerticalScrollScript != null && VerticalScrollScript.UseVerticalScroll
        && CurrentSelectedGO != null)
        {
            bool ComparisonNotReadyCheck = false;
            if (CurrentSelectedGO.transform.parent.parent.gameObject == MenuItemsGO)
            {
                if (CurrentSelectedGO.transform.parent.GetComponent<UGUIMenuItem>())
                {
                    CurrentItemNum = CurrentSelectedGO.transform.parent.GetComponent<UGUIMenuItem>().MenuNum;
                    if (CurrentItemNum == 0)
                    {
                        FromMostOverCheck = true;
                    }
                    else if (CurrentItemNum == (MenuItemButtonGOsAry.Length - 1))
                    {
                        FromMostUnderCheck = true;
                    }
                    else
                    {
                        FromMostOverCheck = false;
                        FromMostUnderCheck = false;
                    }
                }
                else
                {
                    ComparisonNotReadyCheck = true;
                }
                if (transform.parent.GetComponent<RectTransform>() != null && ComparisonNotReadyCheck != true)
                {
                    transform.parent.GetComponent<RectTransform>().GetWorldCorners(ScrollRectCorners);
                }
                else
                {
                    ComparisonNotReadyCheck = true;
                }
                if (CurrentSelectedGO.GetComponent<RectTransform>() != null && ComparisonNotReadyCheck != true)
                {
                    CurrentSelectedGO.GetComponent<RectTransform>().GetWorldCorners(CurrentItemCorners);
                }
                else
                {
                    ComparisonNotReadyCheck = true;
                }
            }
            else
            {
                ComparisonNotReadyCheck = true;
                FromMostOverCheck = false;
                FromMostUnderCheck = false;
            }
            if (ComparisonNotReadyCheck != true)
            {
                bool CurrentItemOverOutsideCheck = false;
                bool CurrentItemUnderOutsideCheck = false;
                if (CurrentSelectedGO.transform.parent.GetComponent<UGUIMenuItem>())
                {
                    CurrentItemOverOutsideCheck = CurrentSelectedGO.transform.parent.GetComponent<UGUIMenuItem>().ScrollOverOutsideCheck;
                    CurrentItemUnderOutsideCheck = CurrentSelectedGO.transform.parent.GetComponent<UGUIMenuItem>().ScrollUnderOutsideCheck;
                }
                if (CurrentItemOverOutsideCheck || CurrentItemUnderOutsideCheck)
                {
                    if (SetScript.MappingMode)
                        SetScript.EscBehavior();
                    SetScript.InhibitMappingModeCheck = true;
                }
                if (CurrentItemOverOutsideCheck)
                {
                    if (CurrentItemNum == 0 && FromMostUnderCheck)
                    { //最下から最上へのループ的移動
                        FromMostUnderCheck = false;
                        EndToEndCheck = true;
                        if (IsInvoking("IntoRectFalse"))
                            CancelInvoke("IntoRectFalse");
                        OverIntoRectCheck = true;
                        UnderIntoRectCheck = false;
                    }
                    if (OverIntoRectCheck != true)
                    {
                        if (VerticalScrollScript.ScrollingNotify() != true) //スクロール中でないなら
                        {
                            if (IsInvoking("IntoRectFalse"))
                                CancelInvoke("IntoRectFalse");
                            OverIntoRectCheck = true;
                            UnderIntoRectCheck = false;
                        }
                        else
                        {
                            if (CurrentItemNum < (MenuItemButtonGOsAry.Length - 1))
                                EventSystem.current.SetSelectedGameObject(MenuItemButtonGOsAry[CurrentItemNum + 1]);
                        }
                    }
                }
                else if (CurrentItemUnderOutsideCheck)
                {
                    if (CurrentItemNum == (MenuItemButtonGOsAry.Length - 1) && FromMostOverCheck)
                    { //最上から最下へのループ的移動
                        FromMostOverCheck = false;
                        EndToEndCheck = true;
                        if (IsInvoking("IntoRectFalse"))
                            CancelInvoke("IntoRectFalse");
                        OverIntoRectCheck = false;
                        UnderIntoRectCheck = true;
                    }
                    if (UnderIntoRectCheck != true)
                    {
                        if (VerticalScrollScript.ScrollingNotify() != true) //スクロール中でないなら
                        {
                            if (IsInvoking("IntoRectFalse"))
                                CancelInvoke("IntoRectFalse");
                            OverIntoRectCheck = false;
                            UnderIntoRectCheck = true;
                        }
                        else
                        {
                            if (CurrentItemNum > 0)
                                EventSystem.current.SetSelectedGameObject(MenuItemButtonGOsAry[CurrentItemNum - 1]);
                        }
                    }
                }
                else
                {
                    if (VerticalScrollScript.ScrollingNotify())
                    {
                        VerticalScrollScript.ScrollingCheckFalse();
                        if (OverIntoRectCheck || UnderIntoRectCheck)
                            Invoke("IntoRectFalse", ScrollIntoInertia);
                    }
                    EndToEndCheck = false;
                }
            }

            if (OverIntoRectCheck || UnderIntoRectCheck)
            {
                Mapper.MoveDelayCall();
                SetScript.InhibitMappingModeCheck = true;
                if (OverIntoRectCheck)
                    ScrollSpeedValue = (ScrollIntoRatio * 0.1f);
                else if (UnderIntoRectCheck)
                    ScrollSpeedValue = -(ScrollIntoRatio * 0.1f);
                if (EndToEndCheck)
                {
                    if (MenuItemsGO.GetComponent<RectTransform>() != null && transform.parent.GetComponent<RectTransform>())
                        ScrollSpeedValue = ScrollSpeedValue * (MenuItemsGO.GetComponent<RectTransform>().rect.height / transform.parent.GetComponent<RectTransform>().rect.height) * 1.5f;
                    else
                        ScrollSpeedValue = ScrollSpeedValue * 2;
                }
                VerticalScrollScript.GetComponent<ScrollRect>().verticalNormalizedPosition = VerticalScrollScript.GetComponent<ScrollRect>().verticalNormalizedPosition + ScrollSpeedValue;
            }

        }

        if (SetScript.FirstSet)
        { //FirstSet中にカーソルオンでどこかが選択状態になった場合の挙動
            if (CurrentSelectedGO != null)
            {
                PrevSelectedGO = CurrentSelectedGO;
                FirstSelectCheck = false;
                SetScript.FirstSet = false;
            }
            return;
        }

        //無関係な場所をクリックしても非選択状態にならないように
        if (SetScript.WindowSituation == 0)
        {
            if (CurrentSelectedGO != null)
            {
                if (CurrentSelectedGO != this.gameObject)
                    PrevSelectedGO = CurrentSelectedGO;
            }
            else
            {
                if (PrevSelectedGO != null)
                    EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
            }
        }

        if (CurrentSelectedGO != this.gameObject)
        {
            //PrevSelectedMenuItem の更新
            if (PrevSelectedGO != null && SetScript.OperateItemLine != true)
            {
                if (PrevSelectedGO.transform.parent.parent.gameObject.name == "InMapperMenuItems"
                && PrevSelectedGO.transform.parent.name.Length == 16
                && PrevSelectedGO.transform.parent.name.IndexOf("MapperMenuItem") == 0)
                    PrevSelectedMenuItem = PrevSelectedGO;
            }
        }
        else
        {
            if (SetScript.OperateItemLine)
            {
                if (PrevSelectedMenuItem == null)
                {
                    if (FirstSelectedMenuItem != null)
                        PrevSelectedMenuItem = FirstSelectedMenuItem;
                    else
                        PrevSelectedMenuItem = transform.parent.Find("InMapperMenuItems/MapperMenuItem00/Button").gameObject;
                }
                if (PrevSelectedMenuItem == null)
                {
                    if (PrevSelectedGO != null)
                        EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(PrevSelectedMenuItem);
                }
            }
            else
            {
                if (OperateItemsAry[0].GetComponent<Button>().interactable)
                {
                    EventSystem.current.SetSelectedGameObject(OperateItemsAry[0]);
                }
                else
                {
                    if (OperateItemsAry[2].GetComponent<Button>().interactable)
                    {
                        EventSystem.current.SetSelectedGameObject(OperateItemsAry[2]);
                    }
                    else
                    {
                        EventSystem.current.SetSelectedGameObject(PrevSelectedGO);
                    }
                }
            }
        }

        if (SetScript.CurrentryRestore)
        {
            if (CurrentSelectedGO == OperateItemsAry[0]
            || CurrentSelectedGO == OperateItemsAry[1])
            {
                EventSystem.current.SetSelectedGameObject(OperateItemsAry[2]);
            }
        }

    }

    void IntoRectFalse()
    {
        OverIntoRectCheck = false;
        UnderIntoRectCheck = false;
        SetScript.InhibitMappingModeCheck = false;
    }

    void OperateItemsSetNonInteractable()
    {
        for (int i = 0; i < OperateItemsAry.Length; i++)
        {
            OperateItemsAry[i].GetComponent<Button>().interactable = false;
        }
    }

    void OperateItemsSetSameSaveData()
    {
        OperateItemsAry[0].GetComponent<Button>().interactable = false;
        OperateItemsAry[1].GetComponent<Button>().interactable = false;
        OperateItemsAry[2].GetComponent<Button>().interactable = true;
        if (OperateItemsAry[3] != null)
        {
            OperateItemsAry[3].GetComponent<Button>().interactable = true;
            if (OperateItemsAry[2].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav2 = OperateItemsAry[2].GetComponent<Button>().navigation;
                customNav2.selectOnUp = OperateItemsAry[3].GetComponent<Button>();
                OperateItemsAry[2].GetComponent<Button>().navigation = customNav2;
            }
            if (OperateItemsAry[3].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav3 = OperateItemsAry[3].GetComponent<Button>().navigation;
                customNav3.selectOnDown = OperateItemsAry[2].GetComponent<Button>();
                OperateItemsAry[3].GetComponent<Button>().navigation = customNav3;
            }
        }
        else
        {
            if (OperateItemsAry[2].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav2 = OperateItemsAry[2].GetComponent<Button>().navigation;
                customNav2.selectOnUp = customNav2.selectOnDown = null;
                OperateItemsAry[2].GetComponent<Button>().navigation = customNav2;
            }
        }
    }

    void OperateItemsSetNormal()
    {
        for (int i = 0; i < OperateItemsAry.Length; i++)
        {
            OperateItemsAry[i].GetComponent<Button>().interactable = true;
        }

        if (OperateItemsAry[3] != null)
        {
            if (OperateItemsAry[2].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav2 = OperateItemsAry[2].GetComponent<Button>().navigation;
                customNav2.selectOnUp = OperateItemsAry[1].GetComponent<Button>();
                OperateItemsAry[2].GetComponent<Button>().navigation = customNav2;
            }
            if (OperateItemsAry[3].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav3 = OperateItemsAry[3].GetComponent<Button>().navigation;
                customNav3.selectOnDown = OperateItemsAry[0].GetComponent<Button>();
                OperateItemsAry[3].GetComponent<Button>().navigation = customNav3;
            }
        }
        else
        {
            if (OperateItemsAry[2].GetComponent<Button>().navigation.mode == Navigation.Mode.Explicit)
            {
                Navigation customNav2 = OperateItemsAry[2].GetComponent<Button>().navigation;
                customNav2.selectOnUp = OperateItemsAry[1].GetComponent<Button>();
                customNav2.selectOnDown = OperateItemsAry[0].GetComponent<Button>();
                OperateItemsAry[2].GetComponent<Button>().navigation = customNav2;
            }
        }

    }

}
