using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class jInputSettings : MonoBehaviour
{
    //"Escape"key is unusable Mapping by default, and always have regular working

    public jInput jInputSOData;
    [Space(15)]
    public string[]
        MenuItemHeadings = new string[]
    {
        "UpMove",
        "DownMove",
        "RightMove",
        "LeftMove",
        "Rotate",
        "ChangeColor",
        "Particle"
    };
    [HideInInspector]
    public string[]
        DefaultInputArray = new string[]
    {
        "UpArrow",
        "DownArrow",
        "RightArrow",
        "LeftArrow",
        "Mouse0",
        "Z",
        "MouseWheel-"
    };
    [HideInInspector]
    public string[]
        DefaultInputArray2p;
    [HideInInspector]
    public string[]
        DefaultInputArray3p;
    [HideInInspector]
    public string[]
        DefaultInputArray4p;
    #if (UNITY_EDITOR)
    [HideInInspector]
    public List<string>
        DefaultInputArrayCopy;
    [HideInInspector]
    public List<string>
        DefaultInputArray2pCopy;
    [HideInInspector]
    public List<string>
        DefaultInputArray3pCopy;
    [HideInInspector]
    public List<string>
        DefaultInputArray4pCopy;
    [HideInInspector]
    public string[]
        MenuItemHeadingsCopy;
    [HideInInspector]
    public bool DefaKeySetModeCheck;
    [HideInInspector]
    public bool CompareThroughCheck;
    [HideInInspector] public int UnusableKeySize;
    [HideInInspector] public int[] UnusableKeyInts = new int[300];
    #endif
    //publicにしておかないとPlay時に初期化されてしまい決定動作除外キー設定が効かなくなる
    [HideInInspector]
    public List<int>
        ExcludeNumList = new List<int>();
    //複数選択ドロップダウンのbit値の合計が入る
    [HideInInspector]
    public int
        ExcludeDecisionFlags = 0;
    [HideInInspector]
    public List<int>
        UpMoveNumList = new List<int>();
    [HideInInspector]
    public List<int>
        DownMoveNumList = new List<int>();
    [HideInInspector]
    public List<int>
        RightMoveNumList = new List<int>();
    [HideInInspector]
    public List<int>
        LeftMoveNumList = new List<int>();
    [HideInInspector]
    public List<int>
        UGUISubmitNumList = new List<int>();
    [HideInInspector]
    public List<int>
        UGUICancelNumList = new List<int>();
    //[0] is not used
    [HideInInspector]
    public int[]
        ExcludeDeviceAry = new int[5];
    [HideInInspector]
    public int
        UGUIUpFlags = 0;
    [HideInInspector]
    public int
        UGUIDownFlags = 0;
    [HideInInspector]
    public int
        UGUIRightFlags = 0;
    [HideInInspector]
    public int
        UGUILeftFlags = 0;
    [HideInInspector]
    public int
        UGUISubmitFlags = 0;
    [HideInInspector]
    public int
        UGUICancelFlags = 0;
    [HideInInspector]
    public int
        PlayerNum = 1;
    string[] TemporaryInputArray;
    string[] TemporaryInputArray2p;
    string[] TemporaryInputArray3p;
    string[] TemporaryInputArray4p;
    string[] ProvisionalArray;
    [HideInInspector]
    public float
        DeadZone = 0.15f;
    [HideInInspector]
    public float
        Gravity = 5.0f;
    [HideInInspector]
    public float
        Sensitivity = 3.0f;
    [HideInInspector]
    public int
        SelectPosition;
    [HideInInspector]
    public int
        PreparedSelectPosition;
    [HideInInspector]
    public int
        OperateLineSelectPosition;
    [HideInInspector]
    public int PreparedOperateSelectPos;
    [HideInInspector]
    public bool
        MappingMode;
    [HideInInspector]
    public bool
        FirstSet;
    [HideInInspector]
    public bool
        CloseButtonSelect;
    [HideInInspector]
    public bool
        SaveNoSelectPosition;
    [HideInInspector]
    public int
        ExitSelectPosition;
    [HideInInspector]
    public Text
        CullentTextCpnt;
    [HideInInspector]
    public TextMesh
        CullentTextMesh;
    [HideInInspector]
    public string
        PreviousText;
    [HideInInspector]
    public bool
        OperateItemLine;
    [HideInInspector]
    public bool
        CurrentryRestore;
    [HideInInspector]
    public bool
        PrecludeSameMappingCheck;
    Mapper MapperScript;
    float verticalPositive;
    float verticalNegative;
    float horizontalPositive;
    float horizontalNegative;
    int MaxMenuSize;
    int MaxOperateMenuSize;
    int InputLength;
    int HeadDiff;
    string CurrentInput;
    string AxisName;
    string ComparisonInputName;
    string MinHeadingKeep;
    float AxisDelayTimer;
    float KeyDelayTimer;
    bool AxisDelay;
    bool KeyDelay;
    bool NothingCloseButtonBool;
    [HideInInspector]
    public bool PlayerNumSituation;
    [HideInInspector]
    public int WindowSituation;
    GameObject MainWindow;
    GameObject MenuItemAll;
    GameObject OperateItemAll;
    GameObject CurrentlyItem;
    GameObject OtherItem;
    GameObject SaveConfirmWindow;
    GameObject ExitWindow;
    GameObject SaveSucsess;
    GameObject Savefailure;
    GameObject PlayerNumWindow;
    GameObject TempMenuItem;
    bool DefaultInputNameUnsuitableCheck;
    bool DefaultInputNameExcludeDeviceCheck;
    [HideInInspector]
    public int
        PlayerSelectNum = 1;
    int CurrentInputJoystickNum;
    int JoystickNum;
    int OtherMingleJoystickNum;
    string OtherMingleCheckText;
    int PlayerMappingNumber;
    static public List<GameObject> MappingSetList = new List<GameObject>();
    [HideInInspector]
    public bool
        SetActiveToOpenClose;
    [HideInInspector]
    public bool
        jInputNonUGUICheck;
    [HideInInspector]
    public bool
        UseEscDefinitedBehavior = true;
    [HideInInspector]
    public bool
        EscPlayerNumWindowCheck;
    [HideInInspector]
    public bool
        InhibitMappingModeCheck;
    [HideInInspector]
    public Color
        SameKeyOutlineColor = new Color(0.75f, 0.45f, 0.65f, 0.7f);
    [HideInInspector]
    public UnityEvent
        ClosingEvent;
    bool MappingPointerUpPreventCheck;
    bool OnUp;
    bool OnDown;
    bool OnRight;
    bool OnLeft;

    void vhRenew()
    {
        if (Mapper.jInputOnUp || Mapper.jInputOnUp2p || Mapper.jInputOnUp3p || Mapper.jInputOnUp4p)
            OnUp = true;
        else
            OnUp = false;
        if (Mapper.jInputOnDown || Mapper.jInputOnDown2p || Mapper.jInputOnDown3p || Mapper.jInputOnDown4p)
            OnDown = true;
        else
            OnDown = false;
        if (Mapper.jInputOnRight || Mapper.jInputOnRight2p || Mapper.jInputOnRight3p || Mapper.jInputOnRight4p)
            OnRight = true;
        else
            OnRight = false;
        if (Mapper.jInputOnLeft || Mapper.jInputOnLeft2p || Mapper.jInputOnLeft3p || Mapper.jInputOnLeft4p)
            OnLeft = true;
        else
            OnLeft = false;
    }

    #if (UNITY_EDITOR)
    void OnValidate()
    {
        if (MenuItemHeadings.Length <= 0)
        {
            MenuItemHeadings = new string[1];
            MenuItemHeadings[0] = MinHeadingKeep;
        }
        HeadingGiveNumber();

        if (MenuItemHeadings[0] != null && MenuItemHeadings.Length >= 1)
        {
            MinHeadingKeep = MenuItemHeadings[0];
        }
        if (MenuItemHeadings.Length > 31)
        {
            Array.Resize(ref MenuItemHeadings, 31);
            //List<string> InterimList = new List<string> (MenuItemHeadings);
            //MenuItemHeadings = InterimList.GetRange (0, 31).ToArray ();
        }
        if (MenuItemHeadings.Length < 1)
        {
            MenuItemHeadings = new string[1];
            if (MinHeadingKeep != null)
            {
                MenuItemHeadings[0] = MinHeadingKeep;
            }
        }

        if (DefaultInputArray2p == null || DefaultInputArray2p.Length == 0)
            DefaultInputArray2p = new string[DefaultInputArray.Length];
        if (DefaultInputArray3p == null || DefaultInputArray3p.Length == 0)
            DefaultInputArray3p = new string[DefaultInputArray.Length];
        if (DefaultInputArray4p == null || DefaultInputArray4p.Length == 0)
            DefaultInputArray4p = new string[DefaultInputArray.Length];

        if (DefaultInputArrayCopy == null || DefaultInputArrayCopy.Count == 0)
            DefaultInputArrayCopy = new List<string>(DefaultInputArray);
        if (DefaultInputArray2pCopy == null || DefaultInputArray2pCopy.Count == 0)
            DefaultInputArray2pCopy = new List<string>(DefaultInputArray2p);
        if (DefaultInputArray3pCopy == null || DefaultInputArray3pCopy.Count == 0)
            DefaultInputArray3pCopy = new List<string>(DefaultInputArray3p);
        if (DefaultInputArray4pCopy == null || DefaultInputArray4pCopy.Count == 0)
            DefaultInputArray4pCopy = new List<string>(DefaultInputArray4p);

        HeadDiff = MenuItemHeadings.Length - DefaultInputArrayCopy.Count;
        if (HeadDiff > 0 && MenuItemHeadings.Length < 32)
        {
            for (int i = 1; i <= HeadDiff; i++)
            {
                DefaultInputArrayCopy.Add("");
            }
        }
        HeadDiff = MenuItemHeadings.Length - DefaultInputArray2pCopy.Count;
        if (HeadDiff > 0 && MenuItemHeadings.Length < 32)
        {
            for (int i = 1; i <= HeadDiff; i++)
            {
                DefaultInputArray2pCopy.Add("");
            }
        }
        HeadDiff = MenuItemHeadings.Length - DefaultInputArray3pCopy.Count;
        if (HeadDiff > 0 && MenuItemHeadings.Length < 32)
        {
            for (int i = 1; i <= HeadDiff; i++)
            {
                DefaultInputArray3pCopy.Add("");
            }
        }
        HeadDiff = MenuItemHeadings.Length - DefaultInputArray4pCopy.Count;
        if (HeadDiff > 0 && MenuItemHeadings.Length < 32)
        {
            for (int i = 1; i <= HeadDiff; i++)
            {
                DefaultInputArray4pCopy.Add("");
            }
        }

        if (DefaultInputArray.Length != MenuItemHeadings.Length)
        {
            DefferentLengthArrayRenew();
        }
        if (DefaultInputArray2p.Length != MenuItemHeadings.Length)
        {
            DefferentLengthArrayRenew2();
        }
        if (DefaultInputArray3p.Length != MenuItemHeadings.Length)
        {
            DefferentLengthArrayRenew3();
        }
        if (DefaultInputArray4p.Length != MenuItemHeadings.Length)
        {
            DefferentLengthArrayRenew4();
        }
        UGUICheckProcess();
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
            Awake();
    }

    public void DefferentLengthArrayRenew()
    {
        DefaultInputArrayCopy.RemoveRange(0, DefaultInputArray.Length);
        DefaultInputArrayCopy.InsertRange(0, DefaultInputArray);
        DefaultInputArray = DefaultInputArrayCopy.GetRange(0, MenuItemHeadings.Length).ToArray();
        ArrayCopyToSOData();
    }

    public void DefferentLengthArrayRenew2()
    {
        DefaultInputArray2pCopy.RemoveRange(0, DefaultInputArray2p.Length);
        DefaultInputArray2pCopy.InsertRange(0, DefaultInputArray2p);
        DefaultInputArray2p = DefaultInputArray2pCopy.GetRange(0, MenuItemHeadings.Length).ToArray();
        ArrayCopyToSOData2();
    }

    public void DefferentLengthArrayRenew3()
    {
        DefaultInputArray3pCopy.RemoveRange(0, DefaultInputArray3p.Length);
        DefaultInputArray3pCopy.InsertRange(0, DefaultInputArray3p);
        DefaultInputArray3p = DefaultInputArray3pCopy.GetRange(0, MenuItemHeadings.Length).ToArray();
        ArrayCopyToSOData3();
    }

    public void DefferentLengthArrayRenew4()
    {
        DefaultInputArray4pCopy.RemoveRange(0, DefaultInputArray4p.Length);
        DefaultInputArray4pCopy.InsertRange(0, DefaultInputArray4p);
        DefaultInputArray4p = DefaultInputArray4pCopy.GetRange(0, MenuItemHeadings.Length).ToArray();
        ArrayCopyToSOData4();
    }

    public void ArrayCopyToSOData()
    {
        jInputSOData.DefaultInputArray.Clear();
        jInputSOData.DefaultInputArray.AddRange(DefaultInputArray);
    }

    public void ArrayCopyToSOData2()
    {
        jInputSOData.DefaultInputArray2p.Clear();
        jInputSOData.DefaultInputArray2p.AddRange(DefaultInputArray2p);
    }

    public void ArrayCopyToSOData3()
    {
        jInputSOData.DefaultInputArray3p.Clear();
        jInputSOData.DefaultInputArray3p.AddRange(DefaultInputArray3p);
    }

    public void ArrayCopyToSOData4()
    {
        jInputSOData.DefaultInputArray4p.Clear();
        jInputSOData.DefaultInputArray4p.AddRange(DefaultInputArray4p);
    }

    public void PlayerNumToSOData()
    {
        jInputSOData.PlayerNum = PlayerNum;
    }

    public void ExcludeDropdownToList()
    {
        if (ExcludeNumList.Count != 0)
            ExcludeNumList.Clear();
        if (ExcludeDecisionFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            { //この方法は32を周期にループしてしまうので,例えば0がチェックされると32,64…も同じとみなされることに注意
                int DecisionNum = 1 << i; //ビットフラグをiぶん左にずらす
                if ((ExcludeDecisionFlags & DecisionNum) != 0)  //ビット演算子'&'は左右のビットを比べ,双方でフラグが立っている場合1/片方やどちらもたってないなら0とする
                { //Inspectorのドロップダウンで選択されている項目の番号だけリスト
                    ExcludeNumList.Add(i);
                }
            }
        }
        AddToExcludeFuncNum();
    }

    public void UIOperationDropdownToList()
    {
        if (UpMoveNumList.Count != 0)
            UpMoveNumList.Clear();
        if (UGUIUpFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUIUpFlags & TempNum) != 0)
                    UpMoveNumList.Add(i);
            }
        }
        if (DownMoveNumList.Count != 0)
            DownMoveNumList.Clear();
        if (UGUIDownFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUIDownFlags & TempNum) != 0)
                    DownMoveNumList.Add(i);
            }
        }
        if (RightMoveNumList.Count != 0)
            RightMoveNumList.Clear();
        if (UGUIRightFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUIRightFlags & TempNum) != 0)
                    RightMoveNumList.Add(i);
            }
        }
        if (LeftMoveNumList.Count != 0)
            LeftMoveNumList.Clear();
        if (UGUILeftFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUILeftFlags & TempNum) != 0)
                    LeftMoveNumList.Add(i);
            }
        }
        if (UGUISubmitNumList.Count != 0)
            UGUISubmitNumList.Clear();
        if (UGUISubmitFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUISubmitFlags & TempNum) != 0)
                    UGUISubmitNumList.Add(i);
            }
        }
        if (UGUICancelNumList.Count != 0)
            UGUICancelNumList.Clear();
        if (UGUICancelFlags != 0)
        {
            for (int i = 0; i < MenuItemHeadings.Length; i++)
            {
                int TempNum = 1 << i;
                if ((UGUICancelFlags & TempNum) != 0)
                    UGUICancelNumList.Add(i);
            }
        }
        AddToExcludeFuncNum();
    }

    void AddToExcludeFuncNum()
    { //以下で上下左右とCancelの役割に設定されているボタンは自動的に決定動作除外に追加
        if (UpMoveNumList.Count != 0)
        {
            for (int i = 0; i < UpMoveNumList.Count; i++)
            {
                if (ExcludeNumList.IndexOf(UpMoveNumList[i]) == -1)
                    ExcludeNumList.Add(UpMoveNumList[i]);
            }
        }
        if (DownMoveNumList.Count != 0)
        {
            for (int i = 0; i < DownMoveNumList.Count; i++)
            {
                if (ExcludeNumList.IndexOf(DownMoveNumList[i]) == -1)
                    ExcludeNumList.Add(DownMoveNumList[i]);
            }
        }
        if (RightMoveNumList.Count != 0)
        {
            for (int i = 0; i < RightMoveNumList.Count; i++)
            {
                if (ExcludeNumList.IndexOf(RightMoveNumList[i]) == -1)
                    ExcludeNumList.Add(RightMoveNumList[i]);
            }
        }
        if (LeftMoveNumList.Count != 0)
        {
            for (int i = 0; i < LeftMoveNumList.Count; i++)
            {
                if (ExcludeNumList.IndexOf(LeftMoveNumList[i]) == -1)
                    ExcludeNumList.Add(LeftMoveNumList[i]);
            }
        }
        if (UGUICancelNumList.Count != 0)
        {
            for (int i = 0; i < UGUICancelNumList.Count; i++)
            {
                if (ExcludeNumList.IndexOf(UGUICancelNumList[i]) == -1)
                    ExcludeNumList.Add(UGUICancelNumList[i]);
            }
        }
        ExcludeDecisionFlags = 0;
        for (int i = 0; i < ExcludeNumList.Count; i++)
        {
            int TempNum = 1 << ExcludeNumList[i]; //ビットフラグをExcludeNumList[i]に格納している数字ぶん左にずらす
            ExcludeDecisionFlags += TempNum;
        }
    }

    void HeadingGiveNumber()
    {
        MenuItemHeadingsCopy = new string[MenuItemHeadings.Length];
        for (int i = 0; i < MenuItemHeadingsCopy.Length; i++)
        {
            MenuItemHeadingsCopy[i] = "E" + i + ": " + MenuItemHeadings[i];
        }
    }

    public void DefaultArrayCopyReset()
    {
        DefaultInputArrayCopy = new List<string>(DefaultInputArray);
        DefaultInputArray2pCopy = new List<string>(DefaultInputArray2p);
        DefaultInputArray3pCopy = new List<string>(DefaultInputArray3p);
        DefaultInputArray4pCopy = new List<string>(DefaultInputArray4p);
        for (int i = PlayerNum + 1; i <= 4; i++)
        {
            ExcludeDeviceAry[i] = 0;
        }
    }

    public void UINumListsAdvanceToSOData()
    {
        jInputSOData.UpMoveNumList = UpMoveNumList;
        jInputSOData.DownMoveNumList = DownMoveNumList;
        jInputSOData.RightMoveNumList = RightMoveNumList;
        jInputSOData.LeftMoveNumList = LeftMoveNumList;
        jInputSOData.UGUISubmitNumList = UGUISubmitNumList;
        jInputSOData.UGUICancelNumList = UGUICancelNumList;
    }

    public void AxesAdvanceToSOData()
    {
        jInputSOData.DeadZone = DeadZone;
        jInputSOData.Gravity = Gravity;
        jInputSOData.Sensitivity = Sensitivity;
    }

    public void AxesReadFromSOData()
    {
        if (DeadZone != jInputSOData.DeadZone)
            DeadZone = jInputSOData.DeadZone;
        if (Gravity != jInputSOData.Gravity)
            Gravity = jInputSOData.Gravity;
        if (Sensitivity != jInputSOData.Sensitivity)
            Sensitivity = jInputSOData.Sensitivity;
    }

    public void SODataRenew()
    {
        //DefaultArrayCopyReset();
        jInputSOData.DefaKeyInconsistencyListText = "";
        PlayerNumToSOData();
        ArrayCopyToSOData();
        ArrayCopyToSOData2();
        ArrayCopyToSOData3();
        ArrayCopyToSOData4();
        UIOperationDropdownToList();
        UINumListsAdvanceToSOData();
        ExcludeDropdownToList();
        AxesAdvanceToSOData();
        jInputSOData.AxesSetApply();
    }

    public void UnusableIntsToList()
    {
        jInputSOData.UnusableKeyList.Clear();
        jInputSOData.UnusableKeyList.Clear();
        for (int i = 0; i < UnusableKeySize; i++)
        {
            if (UnusableKeyInts[i] != 0)
            {
                jInputSOData.UnusableKeyList.Add(jInput.AllInputNames[UnusableKeyInts[i]]);
            }
        }
    }

    public void SaveDefaInputMode()
    {
        if (PlayerSelectNum == 2)
        {
            TemporaryInputArray2p.CopyTo(DefaultInputArray2p, 0);
            ArrayCopyToSOData2();
        }
        else if (PlayerSelectNum == 3)
        {
            TemporaryInputArray3p.CopyTo(DefaultInputArray3p, 0);
            ArrayCopyToSOData3();
        }
        else if (PlayerSelectNum == 4)
        {
            TemporaryInputArray4p.CopyTo(DefaultInputArray4p, 0);
            ArrayCopyToSOData4();
        }
        else
        {
            TemporaryInputArray.CopyTo(DefaultInputArray, 0);
            ArrayCopyToSOData();
        }
        InputNameRegularlyCompare();
    }

    public void ApplyDefaInputMode()
    {
        jInputSOData.DefaultInputArray.CopyTo(DefaultInputArray, 0);
    }

    public void ApplyDefaInputMode2p()
    {
        jInputSOData.DefaultInputArray2p.CopyTo(DefaultInputArray2p, 0);
    }

    public void ApplyDefaInputMode3p()
    {
        jInputSOData.DefaultInputArray3p.CopyTo(DefaultInputArray3p, 0);
    }

    public void ApplyDefaInputMode4p()
    {
        jInputSOData.DefaultInputArray4p.CopyTo(DefaultInputArray4p, 0);
    }
    #endif

    void InputSetting(string MappingName)
    {
        if (MappingName != null)
        {
            string previousMappingName = null;
            int tempindex = -1;
            //同キーを重複してマッピングさせない場合に同じキーと現在の欄に元あったキーとを入れ替える
            if (PlayerSelectNum == 2)
            {
                if (PrecludeSameMappingCheck)
                {
                    tempindex = Array.IndexOf(TemporaryInputArray2p, MappingName);
                    if (tempindex != -1)
                    {
                        previousMappingName = TemporaryInputArray2p[SelectPosition];
                        TemporaryInputArray2p[tempindex] = previousMappingName;
                    }
                }
                TemporaryInputArray2p[SelectPosition] = MappingName;
            }
            else if (PlayerSelectNum == 3)
            {
                if (PrecludeSameMappingCheck)
                {
                    tempindex = Array.IndexOf(TemporaryInputArray3p, MappingName);
                    if (tempindex != -1)
                    {
                        previousMappingName = TemporaryInputArray3p[SelectPosition];
                        TemporaryInputArray3p[tempindex] = previousMappingName;
                    }
                }
                TemporaryInputArray3p[SelectPosition] = MappingName;
            }
            else if (PlayerSelectNum == 4)
            {
                if (PrecludeSameMappingCheck)
                {
                    tempindex = Array.IndexOf(TemporaryInputArray4p, MappingName);
                    if (tempindex != -1)
                    {
                        previousMappingName = TemporaryInputArray4p[SelectPosition];
                        TemporaryInputArray4p[tempindex] = previousMappingName;
                    }
                }
                TemporaryInputArray4p[SelectPosition] = MappingName;
            }
            else
            {
                if (PrecludeSameMappingCheck)
                {
                    tempindex = Array.IndexOf(TemporaryInputArray, MappingName);
                    if (tempindex != -1)
                    {
                        previousMappingName = TemporaryInputArray[SelectPosition];
                        TemporaryInputArray[tempindex] = previousMappingName;
                    }
                }
                TemporaryInputArray[SelectPosition] = MappingName;
            }
            if (PrecludeSameMappingCheck && tempindex != -1 && previousMappingName != null)
            { //同キーを重複してマッピングさせない場合に表示上の文字も入れ替える
                if (0 <= tempindex && tempindex <= 9)
                {
                    if (jInputNonUGUICheck)
                    {
                        if (MenuItemAll.transform.Find("MapperMenuItem0" + tempindex + "/TextPrefab") && MenuItemAll.transform.Find("MapperMenuItem0" + tempindex + "/TextPrefab").GetComponent<TextMesh>())
                            MenuItemAll.transform.Find("MapperMenuItem0" + tempindex + "/TextPrefab").GetComponent<TextMesh>().text = previousMappingName;
                    }
                    else
                    {
                        if (MenuItemAll.transform.Find("MapperMenuItem0" + tempindex) && MenuItemAll.transform.Find("MapperMenuItem0" + tempindex).GetComponent<UGUIMenuItem>())
                            MenuItemAll.transform.Find("MapperMenuItem0" + tempindex).GetComponent<UGUIMenuItem>().InputText.text = previousMappingName;
                    }
                }
                else if (10 <= tempindex && tempindex <= 30)
                {
                    if (jInputNonUGUICheck)
                    {
                        if (MenuItemAll.transform.Find("MapperMenuItem" + tempindex + "/TextPrefab") && MenuItemAll.transform.Find("MapperMenuItem" + tempindex + "/TextPrefab").GetComponent<TextMesh>())
                            MenuItemAll.transform.Find("MapperMenuItem" + tempindex + "/TextPrefab").GetComponent<TextMesh>().text = previousMappingName;
                    }
                    else
                    {
                        if (MenuItemAll.transform.Find("MapperMenuItem" + tempindex) && MenuItemAll.transform.Find("MapperMenuItem" + tempindex).GetComponent<UGUIMenuItem>())
                            MenuItemAll.transform.Find("MapperMenuItem" + tempindex).GetComponent<UGUIMenuItem>().InputText.text = previousMappingName;
                    }
                }
                else
                {
                    Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
                }
            }
        }
        else
        {
            Debug.LogError("[jInput] Error!! Mapping Name is Not Found!!");
        }
        CurrentryRestore = false;
        SameMappingCheck();
    }

    void SameMappingCheck()
    {
        if (PrecludeSameMappingCheck != true)
        {
            foreach (Transform Child in MenuItemAll.transform)
            {
                if (jInputNonUGUICheck)
                {
                    Child.GetComponent<SelectOrnament>().DuplicationTextColor = false;
                }
                else
                {
                    UGUISameKeyOutline TempSameKeyOutlineScript;
                    if (TempSameKeyOutlineScript = Child.GetComponent<UGUIMenuItem>().InputText.GetComponent<UGUISameKeyOutline>())
                    {
                    }
                    else
                    {
                        TempSameKeyOutlineScript = Child.GetComponent<UGUIMenuItem>().InputText.gameObject.AddComponent<UGUISameKeyOutline>();
                    }
                    TempSameKeyOutlineScript.UseOutline = false;
                }
            }
            for (int i = 0; i < MaxMenuSize - 1; i++)
            { //change text ornament when same setting in oneself
                CurrentlyItem = MenuItemAll.transform.GetChild(i).gameObject;
                for (int j = i + 1; j < MaxMenuSize; j++)
                {
                    OtherItem = MenuItemAll.transform.GetChild(j).gameObject;
                    if (jInputNonUGUICheck)
                    {
                        if (CurrentlyItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text == OtherItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text)
                        {
                            CurrentlyItem.GetComponent<SelectOrnament>().DuplicationTextColor = true;
                            OtherItem.GetComponent<SelectOrnament>().DuplicationTextColor = true;
                        }
                    }
                    else
                    {
                        if (CurrentlyItem.GetComponent<UGUIMenuItem>() != null && OtherItem.GetComponent<UGUIMenuItem>() != null)
                        {
                            if (CurrentlyItem.GetComponent<UGUIMenuItem>().InputText.text == OtherItem.GetComponent<UGUIMenuItem>().InputText.text)
                            {
                                UGUISameKeyOutline SameKeyOutlineScript;
                                SameKeyOutlineScript = CurrentlyItem.GetComponent<UGUIMenuItem>().InputText.GetComponent<UGUISameKeyOutline>();
                                {
                                    SameKeyOutlineScript.OutlineColor = SameKeyOutlineColor;
                                    SameKeyOutlineScript.UseOutline = true;
                                }
                                SameKeyOutlineScript = OtherItem.GetComponent<UGUIMenuItem>().InputText.GetComponent<UGUISameKeyOutline>();
                                {
                                    SameKeyOutlineScript.OutlineColor = SameKeyOutlineColor;
                                    SameKeyOutlineScript.UseOutline = true;
                                }
                            }
                        }
                        else
                        {
                            Debug.LogError("[jInput] UGUIMenuItem script is Not Found in " + CurrentlyItem.name + " gameObject or " + OtherItem.name + " gameObject!!");
                        }
                    }
                }
            }
        }
        if (PlayerNum != 1)
        { //put AlertMark when same setting as others
            for (int k = 1; k <= PlayerNum; k++)
            {
                if (PlayerSelectNum != k)
                {
                    ProvisionalArray = new string[Mapper.InputArray.Length];
                    switch (k)
                    {
                        case 1:
                            Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                            break;
                        case 2:
                            Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                            break;
                        case 3:
                            Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                            break;
                        case 4:
                            Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                            break;
                    }
                    for (int i = 0; i < MaxMenuSize; i++)
                    {
                        CurrentlyItem = MenuItemAll.transform.GetChild(i).gameObject;
                        for (int j = 0; j < ProvisionalArray.Length; j++)
                        {
                            if (jInputNonUGUICheck)
                            {
                                if (CurrentlyItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text != ProvisionalArray[j])
                                {
                                    CurrentlyItem.GetComponent<SelectOrnament>().AlertMarkCheck = false;
                                }
                                else
                                {
                                    CurrentlyItem.GetComponent<SelectOrnament>().AlertMarkCheck = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (CurrentlyItem.GetComponent<UGUIMenuItem>().InputText.text != ProvisionalArray[j])
                                {
                                    CurrentlyItem.GetComponent<UGUIMenuItem>().AlertMarkCheck = false;
                                }
                                else
                                {
                                    CurrentlyItem.GetComponent<UGUIMenuItem>().AlertMarkCheck = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        if (JoystickNum != 0)
        {
            OtherJoystickNumMingleConf();
        }
    }

    void OtherJoystickNumMingleConf()
    {
        for (int i = 0; i < MaxMenuSize; i++)
        {
            CurrentlyItem = MenuItemAll.transform.GetChild(i).gameObject;
            if (jInputNonUGUICheck)
                OtherMingleCheckText = CurrentlyItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text;
            else
                OtherMingleCheckText = CurrentlyItem.GetComponent<UGUIMenuItem>().InputText.text;
            if (OtherMingleCheckText.Length > 8
                && OtherMingleCheckText.IndexOf("Joystick") == 0)
            {
                OtherMingleJoystickNum = int.Parse(OtherMingleCheckText.Substring(8, 1));
                if (JoystickNum != OtherMingleJoystickNum && OtherMingleJoystickNum != 0)
                {
                    if (jInputNonUGUICheck)
                        CurrentlyItem.GetComponent<SelectOrnament>().AlertMarkCheck = true;
                    else
                        CurrentlyItem.GetComponent<UGUIMenuItem>().AlertMarkCheck = true;
                }
            }
        }
        OtherMingleCheckText = null;
        OtherMingleJoystickNum = 0;
    }

    void SaveConfirmWindowOpen()
    {
        if (CurrentryRestore != true)
        {
            SaveNoSelectPosition = false;
            SaveConfirmWindow.SetActive(true);
            WindowSituation = 1;
        }
    }

    void ExitWindowOpen()
    {
        ExitSelectPosition = 0;
        ExitWindow.SetActive(true);
        WindowSituation = 2;
    }

    public void SaveProcess()
    {
        if (MapperScript == null)
        {
            if (GameObject.Find("jInputMappingManager").GetComponent<Mapper>())
            {
                MapperScript = GameObject.Find("jInputMappingManager").GetComponent<Mapper>();
            }
            else
            {
                Debug.LogError("[jInput] Error!! jInputMappingManager is Not Found!!");
                Debug.LogError("[jInput] Input Mapping Data Save failure!");
                SavefailureIndicate();
                return;
            }
        }
        if (MapperScript != null)
        {
            if (PlayerSelectNum == 2)
            {
                TemporaryInputArray2p.CopyTo(Mapper.InputArray2p, 0);
                TemporaryInputArray2p.CopyTo(MapperScript.AllJoinInputArray, InputLength);
            }
            else if (PlayerSelectNum == 3)
            {
                TemporaryInputArray3p.CopyTo(Mapper.InputArray3p, 0);
                TemporaryInputArray3p.CopyTo(MapperScript.AllJoinInputArray, InputLength * 2);
            }
            else if (PlayerSelectNum == 4)
            {
                TemporaryInputArray4p.CopyTo(Mapper.InputArray4p, 0);
                TemporaryInputArray4p.CopyTo(MapperScript.AllJoinInputArray, InputLength * 3);
            }
            else
            {
                TemporaryInputArray.CopyTo(Mapper.InputArray, 0);
                TemporaryInputArray.CopyTo(MapperScript.AllJoinInputArray, 0);
            }
            MapperScript.SaveData();

            CurrentryRestore = true;
            SaveConfirmWindow.SetActive(false);
            WindowSituation = 0;
            OperateItemLine = true;
            if (jInputNonUGUICheck)
                OperateLineSelectPosition = 2;
        }
    }

    void RestorePrevious()
    {
        if (CurrentryRestore != true)
        {
            if (MapperScript == null)
            {
                if (GameObject.Find("jInputMappingManager").GetComponent<Mapper>())
                {
                    MapperScript = GameObject.Find("jInputMappingManager").GetComponent<Mapper>();
                }
                else
                {
                    Debug.LogError("[jInput] Error!! jInputMappingManager is Not Found!!");
                    Debug.LogError("[jInput] Input Mapping Restore Previous failure!");
                    return;
                }
            }

            MapperScript.LoadData();
            ProvisionalArray = new string[Mapper.InputArray.Length];
            switch (PlayerSelectNum)
            {
                case 1:
                    Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                    break;
                case 2:
                    Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                    break;
                case 3:
                    Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                    break;
                case 4:
                    Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                    break;
            }
            for (int i = 0; i < MaxMenuSize; i++)
            {
                TempMenuItem = null;
                if (0 <= i && i <= 9)
                {
                    if (MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject)
                        TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject;
                }
                else if (10 <= i && i <= 30)
                {
                    if (MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject)
                        TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject;
                }
                else
                {
                    Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
                }
                if (TempMenuItem != null)
                {
                    if (jInputNonUGUICheck)
                        TempMenuItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text = ProvisionalArray[i];
                    else
                        TempMenuItem.GetComponent<UGUIMenuItem>().InputText.text = ProvisionalArray[i];
                }
                else
                {
                    Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
                }
            }
            CurrentryRestore = true;
            OperateItemLine = true;
            if (jInputNonUGUICheck)
                OperateLineSelectPosition = 2;
        }
        SameMappingCheck();
    }

    void DefaultInputSet()
    {
        ProvisionalArray = new string[DefaultInputArray.Length];
        switch (PlayerSelectNum)
        {
            case 1:
                DefaultInputArray.CopyTo(ProvisionalArray, 0);
                DefaultInputArray.CopyTo(TemporaryInputArray, 0);
                break;
            case 2:
                DefaultInputArray2p.CopyTo(ProvisionalArray, 0);
                DefaultInputArray2p.CopyTo(TemporaryInputArray2p, 0);
                break;
            case 3:
                DefaultInputArray3p.CopyTo(ProvisionalArray, 0);
                DefaultInputArray3p.CopyTo(TemporaryInputArray3p, 0);
                break;
            case 4:
                DefaultInputArray4p.CopyTo(ProvisionalArray, 0);
                DefaultInputArray4p.CopyTo(TemporaryInputArray4p, 0);
                break;
        }
        for (int i = 0; i < MaxMenuSize; i++)
        {
            TempMenuItem = null;
            if (0 <= i && i <= 9)
            {
                if (MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject)
                    TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject;
            }
            else if (10 <= i && i <= 30)
            {
                if (MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject)
                    TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject;
            }
            else
            {
                Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
            }
            if (TempMenuItem != null)
            {
                if (jInputNonUGUICheck)
                    TempMenuItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text = ProvisionalArray[i];
                else
                    TempMenuItem.GetComponent<UGUIMenuItem>().InputText.text = ProvisionalArray[i];
            }
            else
            {
                Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
            }
        }
        CurrentryRestore = false;
        SameMappingCheck();
    }

    public void ResetSet()
    {
        if (SaveConfirmWindow != null)
            SaveConfirmWindow.SetActive(true);
        if (ExitWindow != null)
            ExitWindow.SetActive(true);
        if (SaveSucsess != null)
            SaveSucsess.SetActive(true);
        if (Savefailure != null)
            Savefailure.SetActive(true);
        if (PlayerNumWindow != null)
            PlayerNumWindow.SetActive(true);
        if (MainWindow != null)
            MainWindow.SetActive(true);

#if (UNITY_EDITOR)

#else
		Start ();
#endif
    }

    public void SaveSucsessIndicate()
    {
        if (SaveSucsess != null)
            SaveSucsess.SetActive(true);
    }

    public void SavefailureIndicate()
    {
        if (Savefailure != null)
            Savefailure.SetActive(true);
    }

    void TidyMenuItemText()
    {
        try
        {
            ProvisionalArray = new string[Mapper.InputArray.Length];
            switch (PlayerSelectNum)
            {
                case 1:
                    #if (UNITY_EDITOR)
                    if (DefaKeySetModeCheck)
                    {
                        DefaultInputArray.CopyTo(ProvisionalArray, 0);
                        DefaultInputArray.CopyTo(TemporaryInputArray, 0);
                    }
                    else
                    {
                        #endif
                        Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                        Mapper.InputArray.CopyTo(TemporaryInputArray, 0);
                        #if (UNITY_EDITOR)
                    }
                    #endif
                    break;
                case 2:
                    #if (UNITY_EDITOR)
                    if (DefaKeySetModeCheck)
                    {
                        DefaultInputArray2p.CopyTo(ProvisionalArray, 0);
                        DefaultInputArray2p.CopyTo(TemporaryInputArray, 0);
                    }
                    else
                    {
                        #endif
                        Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                        Mapper.InputArray2p.CopyTo(TemporaryInputArray2p, 0);
                        #if (UNITY_EDITOR)
                    }
                    #endif
                    break;
                case 3:
                    #if (UNITY_EDITOR)
                    if (DefaKeySetModeCheck)
                    {
                        DefaultInputArray3p.CopyTo(ProvisionalArray, 0);
                        DefaultInputArray3p.CopyTo(TemporaryInputArray, 0);
                    }
                    else
                    {
                        #endif
                        Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                        Mapper.InputArray3p.CopyTo(TemporaryInputArray3p, 0);
                        #if (UNITY_EDITOR)
                    }
                    #endif
                    break;
                case 4:
                    #if (UNITY_EDITOR)
                    if (DefaKeySetModeCheck)
                    {
                        DefaultInputArray4p.CopyTo(ProvisionalArray, 0);
                        DefaultInputArray4p.CopyTo(TemporaryInputArray, 0);
                    }
                    else
                    {
                        #endif
                        Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                        Mapper.InputArray4p.CopyTo(TemporaryInputArray4p, 0);
                        #if (UNITY_EDITOR)
                    }
                    #endif
                    break;
            }
            for (int i = 0; i < MaxMenuSize; i++)
            {
                TempMenuItem = null;
                if (0 <= i && i <= 9)
                {
                    if (MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject)
                        TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem0" + i).gameObject;
                }
                else if (10 <= i && i <= 30)
                {
                    if (MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject)
                        TempMenuItem = MenuItemAll.transform.Find("MapperMenuItem" + i).gameObject;
                }
                else
                {
                    Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
                }
                if (TempMenuItem != null)
                {
                    if (jInputNonUGUICheck)
                        TempMenuItem.transform.Find("TextPrefab").GetComponent<TextMesh>().text = ProvisionalArray[i];
                    else
                        TempMenuItem.GetComponent<UGUIMenuItem>().InputText.text = ProvisionalArray[i];
                }
                else
                {
                    Debug.LogError("[jInput] Error!! To be necessary MapperMenuItem Object naming 'MapperMenuItem'+ serial number of double figures 00 to 30.");
                }
            }
            CurrentryRestore = true;
            SameMappingCheck();
        }
        catch (ArgumentException)
        {
            Debug.LogError("[jInput] Error!! It failed to operate of open jInput Mapping window.");
        }
    }

    void Awake()
    {
        if (MappingSetList.Count > 0 && MappingSetList[0] == null)
        {
            MappingSetList.Clear();
            MappingSetList.TrimExcess();
        }
        if (MappingSetList.Contains(this.gameObject) != true)
            MappingSetList.Add(this.gameObject);

    }

    void OnEnable()
    {
        Awake();
    }

    #if (UNITY_EDITOR)
    void InputNameRegularlyCompare()
    {
        if (CompareThroughCheck)
            return;

        jInputSOData.DefaKeyInconsistencyListText = "";
        bool NotSetDefaKey2p = false;
        bool NotSetDefaKey3p = false;
        bool NotSetDefaKey4p = false;
        ProvisionalArray = new string[DefaultInputArray.Length];
        for (int k = 1; k <= PlayerNum; k++)
        {
            switch (k)
            {
                case 1:
                    DefaultInputArray.CopyTo(ProvisionalArray, 0);
                    break;
                case 2:
                    DefaultInputArray2p.CopyTo(ProvisionalArray, 0);
                    break;
                case 3:
                    DefaultInputArray3p.CopyTo(ProvisionalArray, 0);
                    break;
                case 4:
                    DefaultInputArray4p.CopyTo(ProvisionalArray, 0);
                    break;
            }
            for (int x = 0; x < ProvisionalArray.Length; x++)
            {
                if (ProvisionalArray[x] == "")
                {
                    switch (k)
                    {
                        case 1:
                            if (PlayerNum == 1)
                            {
                                Debug.LogError("[jInput] Default Input Mapping have not been set!! (Elements " + x + ")");
                                jInputSOData.DefaKeyInconsistencyListText += " Not Set (" + MenuItemHeadings[x] + ")\n";
                            }
                            else
                            {
                                Debug.LogError("[jInput] Default Input Mapping have not been set!! Player1P (Elements " + x + ")");
                                jInputSOData.DefaKeyInconsistencyListText += " -Player1P: Not Set (" + MenuItemHeadings[x] + ")\n";
                            }
                            jInputSOData.DefaKey1pFoldoutBool = true;
                            jInputSOData.DefaKeyFoldoutBool = true;
                            jInputSOData.SOValueDetermine();
                            Debug.Break();
                            jInputSOData.DefaultInputNameInconsistencyCheck = true;
                            Mapper.EditorPlaymodeStop = true;
                            DefaultInputNameUnsuitableCheck = true;
                            MapperScript.SaveFileDelete();
                            break;
                        case 2:
                            if (NotSetDefaKey2p != true)
                            {
                                Debug.LogWarning("[jInput] Default Input Mapping have not been set! Player2P");
                                NotSetDefaKey2p = true;
                            }
                            break;
                        case 3:
                            if (NotSetDefaKey3p != true)
                            {
                                Debug.LogWarning("[jInput] Default Input Mapping have not been set! Player3P");
                                NotSetDefaKey3p = true;
                            }
                            break;
                        case 4:
                            if (NotSetDefaKey4p != true)
                            {
                                Debug.LogWarning("[jInput] Default Input Mapping have not been set! Player4P");
                                NotSetDefaKey4p = true;
                            }
                            break;
                    }
                }
                else
                {
                    if (PrecludeSameMappingCheck)
                    {
                        bool overlapCheck = false;
                        for (int y = 0; y < ProvisionalArray.Length; y++)
                        {
                            if (x == y)
                                continue;
                            if (ProvisionalArray[y] == ProvisionalArray[x])
                            {
                                switch (k)
                                {
                                    case 1:
                                        jInputSOData.DefaKey1pFoldoutBool = true;
                                        break;
                                    case 2:
                                        jInputSOData.DefaKey2pFoldoutBool = true;
                                        break;
                                    case 3:
                                        jInputSOData.DefaKey3pFoldoutBool = true;
                                        break;
                                    case 4:
                                        jInputSOData.DefaKey4pFoldoutBool = true;
                                        break;
                                }
                                Debug.LogError("[jInput] Default Input Mapping have same key!!\n              Player " + k + "P: '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                                jInputSOData.DefaKeyInconsistencyListText += " -Player" + k + "P: '" + ProvisionalArray[x] + "' (" + MenuItemHeadings[x] + ")\n";
                                overlapCheck = true;
                                break;
                            }
                        }
                        if (overlapCheck)
                        {
                            jInputSOData.DefaKeyFoldoutBool = true;
                            jInputSOData.SameMappingFoldoutBool = true;
                            jInputSOData.SOValueDetermine();
                            Debug.Break();
                            jInputSOData.DefaultInputNameInconsistencyCheck = true;
                            Mapper.EditorPlaymodeStop = true;
                            DefaultInputNameUnsuitableCheck = true;
                        }
                    }
                }
                if (Array.IndexOf(jInput.AllInputNames, ProvisionalArray[x]) < 0 && ProvisionalArray[x] != "")
                {
                    if (PlayerNum == 1)
                    {
                        jInputSOData.DefaKey1pFoldoutBool = true;
                        Debug.LogError("[jInput] Default Input Mapping have Unsuitable Input Name!!\n              '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                        jInputSOData.DefaKeyInconsistencyListText += " '" + ProvisionalArray[x] + "' (" + MenuItemHeadings[x] + ")\n";
                    }
                    else
                    {
                        switch (k)
                        {
                            case 1:
                                jInputSOData.DefaKey1pFoldoutBool = true;
                                break;
                            case 2:
                                jInputSOData.DefaKey2pFoldoutBool = true;
                                break;
                            case 3:
                                jInputSOData.DefaKey3pFoldoutBool = true;
                                break;
                            case 4:
                                jInputSOData.DefaKey4pFoldoutBool = true;
                                break;
                        }
                        Debug.LogError("[jInput] Default Input Mapping have Unsuitable Input Name!!\n             Player " + k + "P: '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                        jInputSOData.DefaKeyInconsistencyListText += " -Player" + k + "P: '" + ProvisionalArray[x] + "' (" + MenuItemHeadings[x] + ")\n";
                    }
                    jInputSOData.DefaKeyFoldoutBool = true;
                    jInputSOData.SOValueDetermine();
                    Debug.Break();
                    jInputSOData.DefaultInputNameInconsistencyCheck = true;
                    Mapper.EditorPlaymodeStop = true;
                    DefaultInputNameUnsuitableCheck = true;
                }
                if (jInputSOData.UnusableKeyList.IndexOf(ProvisionalArray[x]) != -1)
                {
                    if (PlayerNum == 1)
                    {
                        jInputSOData.DefaKey1pFoldoutBool = true;
                        Debug.LogError("[jInput] Default Input Mapping have the unusable mapping key!\n              '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                    }
                    else
                    {
                        switch (k)
                        {
                            case 1:
                                jInputSOData.DefaKey1pFoldoutBool = true;
                                break;
                            case 2:
                                jInputSOData.DefaKey2pFoldoutBool = true;
                                break;
                            case 3:
                                jInputSOData.DefaKey3pFoldoutBool = true;
                                break;
                            case 4:
                                jInputSOData.DefaKey4pFoldoutBool = true;
                                break;
                        }
                        Debug.LogError("[jInput] Default Input Mapping have the unusable mapping key!\n             Player " + k + "P: '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                    }
                    jInputSOData.DefaKeyFoldoutBool = true;
                    jInputSOData.SOValueDetermine();
                    Debug.Break();
                    jInputSOData.DefaultInputNameInconsistencyCheck = true;
                    Mapper.EditorPlaymodeStop = true;
                    DefaultInputNameUnsuitableCheck = true;
                }

                DefaultInputNameExcludeDeviceCheck = false;
                if (ExcludeDeviceAry[k] == 1)
                {
                    if (ProvisionalArray[x].Length > 8 && ProvisionalArray[x].Substring(0, 8) == "Joystick"
                        || ProvisionalArray[x].Length > 5 && ProvisionalArray[x].Substring(0, 5) == "Mouse")
                    {
                    }
                    else
                    {
                        DefaultInputNameExcludeDeviceCheck = true;
                    }
                }
                else if (ExcludeDeviceAry[k] == 2)
                {
                    if (ProvisionalArray[x].Length > 8 && ProvisionalArray[x].Substring(0, 8) == "Joystick")
                        DefaultInputNameExcludeDeviceCheck = true;
                }
                if (DefaultInputNameExcludeDeviceCheck)
                {
                    if (PlayerNum == 1)
                    {
                        jInputSOData.DefaKey1pFoldoutBool = true;
                        Debug.LogError("[jInput] Default Input Mapping have the Key of Exclude Device!\n              '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                    }
                    else
                    {
                        switch (k)
                        {
                            case 1:
                                jInputSOData.DefaKey1pFoldoutBool = true;
                                break;
                            case 2:
                                jInputSOData.DefaKey2pFoldoutBool = true;
                                break;
                            case 3:
                                jInputSOData.DefaKey3pFoldoutBool = true;
                                break;
                            case 4:
                                jInputSOData.DefaKey4pFoldoutBool = true;
                                break;
                        }
                        Debug.LogError("[jInput] Default Input Mapping have the Key of Exclude Device!\n             Player " + k + "P: '" + ProvisionalArray[x] + "'  (Elements " + x + ")");
                    }
                    jInputSOData.DefaKeyFoldoutBool = true;
                    jInputSOData.ExcludeDeviceFoldoutBool = true;
                    jInputSOData.SOValueDetermine();
                    Debug.Break();
                    Mapper.EditorPlaymodeStop = true;
                    DefaultInputNameUnsuitableCheck = true;
                }
            }
        }
        if (DefaultInputNameUnsuitableCheck)
        {
            //MapperScript.SaveFileDelete (); //1P未設定以外の不適合名はjInput.csのほうからこの処理を行っている
        }
        else
        {
            jInputSOData.PlayerNum = PlayerNum;
            jInputSOData.DefaultInputArray.Clear();
            jInputSOData.DefaultInputArray2p.Clear();
            jInputSOData.DefaultInputArray3p.Clear();
            jInputSOData.DefaultInputArray4p.Clear();
            jInputSOData.DefaultInputArray.InsertRange(0, DefaultInputArray);
            if (PlayerNum >= 2)
            {
                jInputSOData.DefaultInputArray2p.InsertRange(0, DefaultInputArray2p);
            }
            if (PlayerNum >= 3)
            {
                jInputSOData.DefaultInputArray3p.InsertRange(0, DefaultInputArray3p);
            }
            if (PlayerNum >= 4)
            {
                jInputSOData.DefaultInputArray4p.InsertRange(0, DefaultInputArray4p);
            }
            jInputSOData.DefaultInputNameInconsistencyCheck = false;
        }
    }
    #endif

    void UGUICheckProcess()
    {
        if (GetComponent<Canvas>())
            jInputNonUGUICheck = false;
        else
            jInputNonUGUICheck = true;
    }

    void Start()
    {
        if (MapperScript == null)
            MapperScript = GameObject.Find("jInputMappingManager").GetComponent<Mapper>();
        if (MapperScript == null)
            Debug.LogError("[jInput] Error!! jInputMappingManager is Not Found!!");
        UGUICheckProcess();
        WindowSituation = 0;
        CurrentryRestore = true;
        CloseButtonSelect = false;
        SelectPosition = 0;
        OperateItemLine = false;
        JoystickNum = 0;
        MappingMode = false;
        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck != true)
        #endif
            PlayerSelectNum = 1;
        CloseButtonSelect = false;
        if (PlayerNum < 1 || 4 < PlayerNum)
        {
            PlayerNum = 4;
        }
        DefaultInputNameUnsuitableCheck = false;

#if (UNITY_EDITOR)
        InputNameRegularlyCompare();
        UIOperationDropdownToList();
        UINumListsAdvanceToSOData();
        AxesAdvanceToSOData();
        jInputSOData.AxesSetApply();
#endif
        if (DefaultInputNameUnsuitableCheck)
        {
            return;
        }
        InputLength = DefaultInputArray.Length;
        if (InputLength <= 0)
        {
            Debug.LogError("[jInput] Error!! To verify jInput Settings and re-create the input mapping data!!");
            Debug.Break();
#if (UNITY_EDITOR)
            Mapper.EditorPlaymodeStop = true;
            return;
#else
			return;
#endif
        }
        TemporaryInputArray = new string[InputLength];
        DefaultInputArray.CopyTo(TemporaryInputArray, 0);
        if (PlayerNum >= 2)
        {
            TemporaryInputArray2p = new string[InputLength];
            DefaultInputArray2p.CopyTo(TemporaryInputArray2p, 0);
        }
        if (PlayerNum >= 3)
        {
            TemporaryInputArray3p = new string[InputLength];
            DefaultInputArray3p.CopyTo(TemporaryInputArray3p, 0);
        }
        if (PlayerNum >= 4)
        {
            TemporaryInputArray4p = new string[InputLength];
            DefaultInputArray4p.CopyTo(TemporaryInputArray4p, 0);
        }

        if (transform.Find("ConfirmWindow"))
        {
            SaveConfirmWindow = transform.Find("ConfirmWindow").gameObject;
            SaveConfirmWindow.SetActive(false);
        }
        else
        {
            Debug.LogError("[jInput] Error!! ConfirmWindow is Not Found!!");
        }
        if (transform.Find("ExitWindow"))
        {
            ExitWindow = transform.Find("ExitWindow").gameObject;
            ExitWindow.SetActive(false);
        }
        else
        {
            Debug.LogError("[jInput] Error!! ExitWindow is Not Found!!");
        }
        if (transform.Find("SaveResultTexts/SaveSucsessText"))
        {
            SaveSucsess = transform.Find("SaveResultTexts/SaveSucsessText").gameObject;
            SaveSucsess.SetActive(false);
        }
        if (transform.Find("SaveResultTexts/SavefailureText"))
        {
            Savefailure = transform.Find("SaveResultTexts/SavefailureText").gameObject;
            Savefailure.SetActive(false);
        }
        if (transform.Find("MainWindow"))
            MainWindow = transform.Find("MainWindow").gameObject;
        else
            Debug.LogError("[jInput] Error!! MainWindow is Not Found!!");
        if (MenuItemAll == null && MainWindow != null)
        {
            if (MainWindow.transform.Find("InMapperMenuItems"))
                MenuItemAll = MainWindow.transform.Find("InMapperMenuItems").gameObject;
            else
                Debug.LogError("[jInput] Error!! InMapperMenuItems is Not Found!!");
        }
        if (MenuItemAll != null)
        {
            if (InputLength != MenuItemAll.transform.childCount)
            {
                Debug.LogError("[jInput] Error!! There is an unnecessary thing in InMapperMenuItems!");
            }
            else
            {
                MaxMenuSize = InputLength;
            }
        }
        if (transform.Find("PlayerNumWindow"))
            PlayerNumWindow = transform.Find("PlayerNumWindow").gameObject;
        if (PlayerNumWindow == null && PlayerNum != 1)
        {
            Debug.LogError("[jInput] Error!! PlayerNumWindow is Not Found!!");
        }
        else
        {
            #if (UNITY_EDITOR)
            if (DefaKeySetModeCheck)
            {
                NothingCloseButtonBool = true;
                PlayerNumSituation = false;
            }
            else
            {
                #endif
                if (PlayerNumWindow.transform.Find("CloseButton") == null || PlayerNumWindow.transform.Find("CloseButton").gameObject.activeSelf == false)
                {
                    NothingCloseButtonBool = true;
                }
                if (PlayerNum == 1)
                {
                    PlayerNumSituation = false;
                }
                else
                {
                    PlayerNumSituation = true;
                }
                #if (UNITY_EDITOR)
            }
            #endif
            PlayerNumWindow.SetActive(false);
        }
        if (OperateItemAll == null && MainWindow != null)
        {
            if (MainWindow.transform.Find("InMapperOperateItems"))
                OperateItemAll = MainWindow.transform.Find("InMapperOperateItems").gameObject;
            else
                Debug.LogError("[jInput] Error!! InMapperOperateItems is Not Found!!");
        }
        if (OperateItemAll != null)
        {
            #if (UNITY_EDITOR)
            if (DefaKeySetModeCheck)
            {
                OperateItemAll.SetActive(false);
            }
            else
            {
                #endif
                if (OperateItemAll.transform.Find("MapperOperateItem03") == null)
                {
                    MaxOperateMenuSize = OperateItemAll.transform.childCount - 1;
                }
                else if (OperateItemAll.transform.Find("MapperOperateItem03").gameObject.activeSelf == false)
                { //Because transform.Find can find the object with SetActive(false);
                    MaxOperateMenuSize = OperateItemAll.transform.childCount - 1;
                }
                else
                {
                    MaxOperateMenuSize = OperateItemAll.transform.childCount;
                }
                #if (UNITY_EDITOR)
            }
            #endif
        }
        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck)
        {
            MappingWindowPrepare();
        }
        else
        {
            #endif
            if (MainWindow != null)
                MainWindow.SetActive(false);
            if (SetActiveToOpenClose != true)
                MappingWindowPrepare();
            else
                this.gameObject.SetActive(false);
            #if (UNITY_EDITOR)
        }
        #endif
    }
    //Start() End

    void Update()
    {
        if (AxisDelay)
        {
            AxisDelayTimer += Time.deltaTime;
            if (AxisDelayTimer > 0.15f)
            {
                AxisDelay = false;
                AxisDelayTimer = 0;
            }
        }
        if (KeyDelay)
        {
            KeyDelayTimer += Time.deltaTime;
            if (KeyDelayTimer > 0.1f)
            {
                KeyDelay = false;
                KeyDelayTimer = 0;
            }
        }

        if (WindowSituation < 0 || 2 < WindowSituation)
            WindowSituation = 0;
#if (UNITY_EDITOR)
        if (jInputSOData == null)
            Debug.LogError("[jInput] Error! jInputSOData is not set in jInputSettings!!");
#endif

        if (jInputNonUGUICheck)
        {
            vhRenew();

            //Axes&SelectedControll
            if (AxisDelay != true)
            {
                if (WindowSituation == 1)
                {
                    if (OnRight | OnLeft)
                    {
                        SaveNoSelectPosition = !SaveNoSelectPosition;
                        AxisDelay = true;
                    }
                }
                else if (WindowSituation == 2)
                {
                    if (OnLeft)
                    {
                        ExitSelectPosition--;
                        AxisDelay = true;
                    }
                    if (OnRight)
                    {
                        ExitSelectPosition++;
                        AxisDelay = true;
                    }
                }
                else if (PlayerNumSituation && gameObject.activeSelf != false)
                {
                    if (OnUp | OnDown)
                    {
                        if (NothingCloseButtonBool != true)
                        {
                            if (CloseButtonSelect != true)
                            {
                                CloseButtonSelect = true;
                            }
                            else
                            {
                                CloseButtonSelect = false;
                            }
                            AxisDelay = true;
                        }
                    }
                    if (CloseButtonSelect != true)
                    {
                        if (OnLeft)
                        {
                            PlayerSelectNum--;
                            AxisDelay = true;
                        }
                        if (OnRight)
                        {
                            PlayerSelectNum++;
                            AxisDelay = true;
                        }
                    }
                }
                else
                {
                    if (MappingMode != true)
                    {
                        if (OnRight | OnLeft)
                        {
                            if (FirstSet != true)
                            {
                                OperateItemLine = !OperateItemLine;
                                if (CurrentryRestore)
                                {
                                    OperateLineSelectPosition = 2;
                                }
                                else
                                {
                                    OperateLineSelectPosition = 0;
                                }
                            }
                            AxisDelay = true;
                        }
                        if (OperateItemLine != true)
                        {
                            if (OnDown)
                            {
                                if (FirstSet != true)
                                {
                                    SelectPosition++;
                                }
                                AxisDelay = true;
                            }
                            else if (OnUp)
                            {
                                if (FirstSet != true)
                                {
                                    SelectPosition--;
                                }
                                AxisDelay = true;
                            }
                        }
                        else
                        {
                            if (CurrentryRestore != true)
                            {
                                if (OnDown)
                                {
                                    OperateLineSelectPosition++;
                                    AxisDelay = true;
                                }
                                else if (OnUp)
                                {
                                    OperateLineSelectPosition--;
                                    AxisDelay = true;
                                }
                            }
                            else
                            {
                                if (OnUp | OnDown)
                                {
                                    if (MaxOperateMenuSize >= 4)
                                    {
                                        if (OperateLineSelectPosition == 2)
                                        {
                                            OperateLineSelectPosition = 3;
                                            AxisDelay = true;
                                        }
                                        else
                                        {
                                            OperateLineSelectPosition = 2;
                                            AxisDelay = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } //Axes&SelectedControll End
        }

        //over and under select
        if (SelectPosition < 0)
            SelectPosition = MaxMenuSize - 1;
        if (SelectPosition > MaxMenuSize - 1)
            SelectPosition = 0;
        if (OperateLineSelectPosition < 0)
            OperateLineSelectPosition = MaxOperateMenuSize - 1;
        if (OperateLineSelectPosition > MaxOperateMenuSize - 1)
            OperateLineSelectPosition = 0;
        if (ExitSelectPosition < 0)
            ExitSelectPosition = 2;
        if (ExitSelectPosition > 2)
            ExitSelectPosition = 0;
        if (PlayerSelectNum < 1)
            PlayerSelectNum = PlayerNum;
        if (PlayerSelectNum > PlayerNum)
            PlayerSelectNum = 1;

        if (DefaultInputNameUnsuitableCheck != true)
        {
            if (PlayerNumSituation != true && gameObject.activeSelf != false)
            {
                MainWindow.SetActive(true);
                PlayerNumWindow.SetActive(false);
            }
            else
            {
                if (PlayerNum != 1)
                {
                    MainWindow.SetActive(false);
                    PlayerNumWindow.SetActive(true);
                }
                else
                {
                    PlayerNumSituation = false;
                    PlayerNumWindow.SetActive(false);
                    MainWindow.SetActive(true);
                }
            }
        }

        if (KeyDelay != true)
        {
            for (int x = 0; x < jInput.KeyValueList.Count; x++)
            {
                if (Input.GetKeyDown(jInput.KeyValueList[x]))
                {
                    if (MappingPointerUpPreventCheck)
                        MappingPointerUpPreventCheck = false;
                    for (int y = 0; y < jInputSOData.UnusableKeyList.Count; y++)
                    {
                        if (jInput.KeyValueList[x].ToString() == jInputSOData.UnusableKeyList[y])
                            return;
                    }
                    CurrentInput = jInput.KeyValueList[x].ToString();
                    if (PlayerSelectNum != 0 && ExcludeDeviceAry[PlayerSelectNum] != 0)
                    {
                        if (CurrentInput.Length > 8 && CurrentInput.Substring(0, 8) == "Joystick")
                        {
                            if (ExcludeDeviceAry[PlayerSelectNum] == 2)
                                return;
                        }
                        else
                        {
                            if (ExcludeDeviceAry[PlayerSelectNum] == 1)
                            {
                                if (CurrentInput.Length > 5 && CurrentInput.Substring(0, 5) == "Mouse")
                                {
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    CurrentInputJoystickNum = 0;
                    if (CurrentInput.IndexOf("Joystick") == 0 && CurrentInput.IndexOf("Button") == 9)
                        CurrentInputJoystickNum = Convert.ToInt32(CurrentInput.Substring(8, 1));
                    if (JoystickNum != 0 && CurrentInputJoystickNum != 0 && PlayerNum != 1 && PlayerNumSituation != true)
                    {
                        if (JoystickNum != CurrentInputJoystickNum)
                            return;
                    }
                    if (MappingMode != true)
                    {
                        ProvisionalArray = new string[Mapper.InputArray.Length];
                        for (int j = 1; j <= PlayerNum; j++)
                        {
                            switch (j)
                            {
                                case 1:
                                    Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                                    break;
                                case 2:
                                    Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                                    break;
                                case 3:
                                    Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                                    break;
                                case 4:
                                    Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                                    break;
                            }
                            for (int i = 0; i < ExcludeNumList.Count; i++)
                            {
                                if (CurrentInput == ProvisionalArray[ExcludeNumList[i]].ToString())
                                {
                                    if (FirstSet && PlayerNumSituation != true)
                                        ReleaseFirstSet();
                                    return;
                                }
                            }
                        }
                    }
                    if (CurrentInputJoystickNum != 0)
                    {
                        if (JoystickNum == 0 && PlayerNum != 1)
                        {
                            JoystickNum = CurrentInputJoystickNum;
                            OtherJoystickNumMingleConf();
                        }
                    }
                    if (FirstSet && PlayerNumSituation != true)
                    {
                        ReleaseFirstSet();
                        return;
                    }
                    AnyPressProsess();
                }
            }
            //Axisの処理(アサインされているもののみ)
            for (int i = 0; i < MaxMenuSize; i++)
            {
                ProvisionalArray = new string[Mapper.InputArray.Length];
                for (int j = 1; j <= PlayerNum; j++)
                {
                    switch (j)
                    {
                        case 1:
                            Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                            break;
                        case 2:
                            Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                            break;
                        case 3:
                            Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                            break;
                        case 4:
                            Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                            break;
                    }
                    if (jInput.GetKeyDown(ProvisionalArray[i]))
                    {
                        CurrentInput = ProvisionalArray[i].ToString();
                        if (PlayerSelectNum != 0 && ExcludeDeviceAry[PlayerSelectNum] != 0)
                        {
                            if (CurrentInput.Length > 8 && CurrentInput.Substring(0, 8) == "Joystick")
                            {
                                if (ExcludeDeviceAry[PlayerSelectNum] == 2)
                                    return;
                            }
                            else
                            {
                                if (ExcludeDeviceAry[PlayerSelectNum] == 1)
                                {
                                    if (CurrentInput.Length > 5 && CurrentInput.Substring(0, 5) == "Mouse")
                                    {
                                    }
                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                        CurrentInputJoystickNum = 0;
                        if (CurrentInput.IndexOf("Joystick") == 0 && CurrentInput.IndexOf("Axis") == 9)
                            CurrentInputJoystickNum = Convert.ToInt32(CurrentInput.Substring(8, 1));
                        if (JoystickNum != 0 && CurrentInputJoystickNum != 0 && PlayerNum != 1 && PlayerNumSituation != true)
                        {
                            if (JoystickNum != CurrentInputJoystickNum)
                                return;
                        }
                        for (int k = 0; k < ExcludeNumList.Count; k++)
                        {
                            if (i == ExcludeNumList[k] && MappingMode != true)
                            {
                                if (FirstSet && PlayerNumSituation != true)
                                    ReleaseFirstSet();
                                return;
                            }
                        }
                        if (CurrentInputJoystickNum != 0)
                        {
                            if (JoystickNum == 0 && PlayerNum != 1)
                            {
                                JoystickNum = CurrentInputJoystickNum;
                                OtherJoystickNumMingleConf();
                            }
                            if (FirstSet && PlayerNumSituation != true)
                            {
                                ReleaseFirstSet();
                                return;
                            }
                            AnyPressProsess();
                        }
                    }
                }
            }

            if (MappingMode && AxisDelay != true && PlayerNumSituation != true)
            {
                if (Input.GetAxis("MouseWheel") > 0.2)
                {
                    CurrentInput = "MouseWheel+";
                    if (jInputSOData.UnusableKeyList.IndexOf(CurrentInput) != -1)
                        return;
                    if (CullentTextCpnt != null)
                        CullentTextCpnt.text = CurrentInput;
                    if (CullentTextMesh != null)
                        CullentTextMesh.text = CurrentInput;
                    InputSetting(CurrentInput);
                    MappingMode = false;
                    KeyDelay = true;
                    AxisDelay = true;
                    PreviousText = null;
                    CurrentryRestore = false;
                }
                else if (Input.GetAxis("MouseWheel") < -0.2)
                {
                    CurrentInput = "MouseWheel-";
                    if (jInputSOData.UnusableKeyList.IndexOf(CurrentInput) != -1)
                        return;
                    if (CullentTextCpnt != null)
                        CullentTextCpnt.text = CurrentInput;
                    if (CullentTextMesh != null)
                        CullentTextMesh.text = CurrentInput;
                    InputSetting(CurrentInput);
                    MappingMode = false;
                    KeyDelay = true;
                    AxisDelay = true;
                    PreviousText = null;
                    CurrentryRestore = false;
                }
            }

            if (PlayerSelectNum != 0 && ExcludeDeviceAry[PlayerSelectNum] == 2)
            {
                return;
            }
            else
            {
                for (int i = 1; i <= 9; i++)
                {
                    for (int j = 1; j <= 20; j++)
                    {
                        AxisName = "Joystick" + (i) + "Axis" + (j);
                        if (Input.GetAxis(AxisName) > 0.17)
                        {
                            CurrentInput = AxisName + "+";
                            if (jInputSOData.UnusableKeyList.IndexOf(CurrentInput) != -1)
                                return;
                            if (JoystickNum != 0 && JoystickNum != i && PlayerNum != 1)
                                return;
                            if (JoystickNum == 0 && PlayerNum != 1)
                            {
                                JoystickNum = i;
                                OtherJoystickNumMingleConf();
                            }
                            if (MappingMode && AxisDelay != true)
                            {
                                ProvisionalArray = new string[Mapper.InputArray.Length];
                                switch (PlayerSelectNum)
                                {
                                    case 1:
                                        Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 2:
                                        Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 3:
                                        Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 4:
                                        Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                                        break;
                                }
                                for (int k = 0; k < ProvisionalArray.Length; k++)
                                {
                                    if (CurrentInput == ProvisionalArray[k].ToString())
                                        return; //Exclude already Mapping key, prosess of already Mapping key is written at different line.(AnyPressProsess())
                                }
                                if (CullentTextCpnt != null)
                                    CullentTextCpnt.text = CurrentInput;
                                if (CullentTextMesh != null)
                                    CullentTextMesh.text = CurrentInput;
                                InputSetting(CurrentInput);
                                MappingMode = false;
                                KeyDelay = true;
                                AxisDelay = true;
                                PreviousText = null;
                                CurrentryRestore = false;
                                return;
                            }
                        }
                        else if (Input.GetAxis(AxisName) < -0.17)
                        {
                            CurrentInput = AxisName + "-";
                            if (jInputSOData.UnusableKeyList.IndexOf(CurrentInput) != -1)
                                return;
                            if (JoystickNum != 0 && JoystickNum != i && PlayerNum != 1)
                                return;
                            if (JoystickNum == 0 && PlayerNum != 1)
                            {
                                JoystickNum = i;
                                OtherJoystickNumMingleConf();
                            }
                            if (MappingMode && AxisDelay != true)
                            {
                                ProvisionalArray = new string[Mapper.InputArray.Length];
                                switch (PlayerSelectNum)
                                {
                                    case 1:
                                        Mapper.InputArray.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 2:
                                        Mapper.InputArray2p.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 3:
                                        Mapper.InputArray3p.CopyTo(ProvisionalArray, 0);
                                        break;
                                    case 4:
                                        Mapper.InputArray4p.CopyTo(ProvisionalArray, 0);
                                        break;
                                }
                                for (int k = 0; k < ProvisionalArray.Length; k++)
                                {
                                    if (CurrentInput == ProvisionalArray[k].ToString())
                                    {
                                        return; //Exclude already Mapping key, prosess of already Mapping key is written at different line.(AnyPressProsess())
                                    }
                                }
                                if (CullentTextCpnt != null)
                                    CullentTextCpnt.text = CurrentInput;
                                if (CullentTextMesh != null)
                                    CullentTextMesh.text = CurrentInput;
                                InputSetting(CurrentInput);
                                MappingMode = false;
                                KeyDelay = true;
                                AxisDelay = true;
                                PreviousText = null;
                                CurrentryRestore = false;
                                return;
                            }
                        }
                    }
                }
            }
        }

    }
    //Update() End

    public void EscBehavior()
    {
        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck)
        {
            if (MappingMode)
            { //when waiting to be pressed the mapping key
                MappingMode = false;
                if (PreviousText != null)
                {
                    if (CullentTextCpnt != null)
                        CullentTextCpnt.text = PreviousText;
                    if (CullentTextMesh != null)
                        CullentTextMesh.text = PreviousText;
                }
                SameMappingCheck();
                PreviousText = null;
                KeyDelay = true;
                AxisDelay = true;
            }
            return;
        }
        #endif
        if (MappingMode)
        { //when waiting to be pressed the mapping key
            MappingMode = false;
            if (PreviousText != null)
            {
                if (CullentTextCpnt != null)
                    CullentTextCpnt.text = PreviousText;
                if (CullentTextMesh != null)
                    CullentTextMesh.text = PreviousText;
            }
            SameMappingCheck();
            PreviousText = null;
        }
        else if (WindowSituation == 1 || WindowSituation == 2)
        { //when ConfirmWindow or ExitWindow is open
            gameObject.SetActive(true);
            SaveConfirmWindow.SetActive(false);
            WindowSituation = 0;
            ExitWindow.SetActive(false);
        }
        else if (PlayerNumSituation && gameObject.activeSelf != false)
        { //when player select
            if (SetActiveToOpenClose)
            {
                gameObject.SetActive(false);
                PlayerSelectNum = 1;
                CloseButtonSelect = false;
                if (jInputNonUGUICheck != true)
                    EscPlayerNumWindowCheck = true;
            }
            if (ClosingEvent != null)
                ClosingEvent.Invoke();
        }
        else if (CurrentryRestore && gameObject.activeSelf != false)
        { //when current mapping is same as save data in normal state
            FirstSet = true;
            if (PlayerNum != 1)
            {
                PlayerNumSituation = true;
                JoystickNum = 0;
            }
            else
            {
                if (SetActiveToOpenClose)
                    gameObject.SetActive(false);
                if (ClosingEvent != null)
                    ClosingEvent.Invoke();
            }
        }
        else if (CurrentryRestore != true && gameObject.activeSelf != false)
        { //when current mapping may be different from save data in normal state
            ExitWindowOpen();
        }
        else
        {
            if (gameObject.activeSelf != true)
            {
                MappingWindowPrepare();
            }
        }
        KeyDelay = true;
        AxisDelay = true;
    }

    public void BackPlayerSelect()
    {
        if (PlayerNum != 1)
        {
            if (MappingMode)
            { //when waiting to be pressed the mapping key
                MappingMode = false;
                if (PreviousText != null)
                {
                    if (CullentTextCpnt != null)
                        CullentTextCpnt.text = PreviousText;
                    if (CullentTextMesh != null)
                        CullentTextMesh.text = PreviousText;
                }
                PreviousText = null;
            }
            if (WindowSituation == 1)
            { //when ConfirmWindow is open
                SaveConfirmWindow.SetActive(false);
                WindowSituation = 2;
                ExitWindow.SetActive(true);
            }
            if (WindowSituation == 2)
            { //when ExitWindow is open
                return;
            }
            else if (PlayerNumSituation && gameObject.activeSelf != false)
            { //when player select
                return;
            }
            else if (CurrentryRestore && gameObject.activeSelf != false)
            { //when current mapping is same as save data
                PlayerNumSituation = true;
                JoystickNum = 0;
            }
            else if (CurrentryRestore != true && gameObject.activeSelf != false)
            { //when current mapping may be different from save data
                ExitWindowOpen();
            }
            else
            { //when mapping window closed
                Debug.LogWarning("[jInput] The input mapping window have closed.");
            }
            KeyDelay = true;
            AxisDelay = true;
        }
        else
        {
            Debug.LogWarning("[jInput] To execute BackPlayerSelect() Method is needed two and more of Max Players in Same Place.");
        }
    }

    public void MappingWindowOpen()
    {
        if (gameObject.activeSelf != true)
            MappingWindowPrepare();
        else
            Debug.LogWarning("[jInput] Mapping window have already opened.");
    }

    void MappingWindowPrepare()
    {
        gameObject.SetActive(true);
        FirstSet = true;

        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck)
        {
            TidyMenuItemText();
        }
        else
        {
            #endif
            if (PlayerNum == 1)
                TidyMenuItemText();
            #if (UNITY_EDITOR)
        }
        #endif
        SelectPosition = 0;
        OperateItemLine = false;
        JoystickNum = 0;
        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck != true)
        #endif
            PlayerSelectNum = 1;
        CloseButtonSelect = false;
        CurrentryRestore = true;
        SaveConfirmWindow.SetActive(false);
        WindowSituation = 0;
        ExitWindow.SetActive(false);
        if (SaveSucsess != null)
        {
            SaveSucsess.SetActive(false);
            Savefailure.SetActive(false);
        }
        KeyDelay = true;
        AxisDelay = true;
        #if (UNITY_EDITOR)
        if (DefaKeySetModeCheck)
        {
            PlayerNumSituation = false;
        }
        else
        {
            #endif
            if (PlayerNum == 1)
                PlayerNumSituation = false;
            else
                PlayerNumSituation = true;
            #if (UNITY_EDITOR)
        }
        #endif
    }

    public void PlayerSelectingProcess()
    {
        TidyMenuItemText();
        FirstSet = true;
        KeyDelay = true;
        AxisDelay = true;
        CloseButtonSelect = false;
        CurrentryRestore = true;
        SelectPosition = 0;
        OperateItemLine = false;
        PlayerNumSituation = false;
    }

    public void ExitWindowProcess()
    {
        ExitWindow.SetActive(false);
        WindowSituation = 0;
        if (PlayerNum != 1)
        {
            PlayerNumSituation = true;
            JoystickNum = 0;
        }
        else
        {
            if (SetActiveToOpenClose)
                gameObject.SetActive(false);
            if (ClosingEvent != null)
                ClosingEvent.Invoke();
        }
        KeyDelay = true;
        AxisDelay = true;
    }

    public void ExitWindowReturnProcess()
    {
        ExitWindow.SetActive(false);
        WindowSituation = 0;
        KeyDelay = true;
        AxisDelay = true;
    }

    public void MenuItemsClickProcess()
    {
        if (FirstSet)
        {
            FirstSet = false;
            AxisDelay = true;
            KeyDelay = true;
        }
        if (KeyDelay != true)
        {
            OperateItemLine = false;
            if (MappingMode != true)
            {
                SelectPosition = PreparedSelectPosition;
                if (InhibitMappingModeCheck != true)
                {
                    MappingMode = true;
                    KeyDelay = true;
                    AxisDelay = true;
                }
            }
        }
        else
        {
            PreparedSelectPosition = SelectPosition;
        }
    }

    public void OperateItemsProcess()
    {
        if (FirstSet)
        {
            FirstSet = false;
            AxisDelay = true;
            KeyDelay = true;
        }
        if (KeyDelay != true && MappingMode != true)
        {
            OperateItemLine = true;
            OperateLineSelectPosition = PreparedOperateSelectPos;
            KeyDelay = true;
            AxisDelay = true;
            switch (OperateLineSelectPosition)
            {
                case 0:
                    SaveConfirmWindowOpen();
                    break;
                case 1:
                    RestorePrevious();
                    break;
                case 2:
                    DefaultInputSet();
                    break;
                case 3:
                    ExitWindowOpen();
                    break;
            }
        }
        else
        {
            PreparedOperateSelectPos = OperateLineSelectPosition;
        }
    }

    public void CurrentryRestoreOperateProcess()
    {
        if (FirstSet)
        {
            FirstSet = false;
            AxisDelay = true;
            KeyDelay = true;
        }
        if (KeyDelay != true && MappingMode != true)
        {
            OperateLineSelectPosition = PreparedOperateSelectPos;
            if (OperateLineSelectPosition == 2)
            {
                OperateItemLine = true;
                DefaultInputSet();
            }
            else if (OperateLineSelectPosition == 3)
            {
                if (PlayerNum != 1)
                {
                    PlayerNumSituation = true;
                    JoystickNum = 0;
                }
                else
                {
                    if (SetActiveToOpenClose)
                        gameObject.SetActive(false);
                    if (ClosingEvent != null)
                        ClosingEvent.Invoke();
                }
            }
            KeyDelay = true;
            AxisDelay = true;
        }
        else
        {
            PreparedOperateSelectPos = OperateLineSelectPosition;
        }
    }

    public void MappingWindowClose()
    {
        if (MappingMode)
        { //when waiting to be pressed the mapping key
            MappingMode = false;
            if (PreviousText != null)
            {
                if (CullentTextCpnt != null)
                    CullentTextCpnt.text = PreviousText;
                if (CullentTextMesh != null)
                    CullentTextMesh.text = PreviousText;
            }
            PreviousText = null;
        }
        if (WindowSituation == 1)
        { //when ConfirmWindow is open
            SaveConfirmWindow.SetActive(false);
            ExitWindowOpen();
        }
        if (WindowSituation == 2)
        { //when ExitWindow is open
            return;
        }
        else if (PlayerNumSituation && gameObject.activeSelf != false)
        { //when player select
            if (SetActiveToOpenClose)
            {
                gameObject.SetActive(false);
                PlayerSelectNum = 1;
                CloseButtonSelect = false;
            }
            if (ClosingEvent != null)
                ClosingEvent.Invoke();
        }
        else if (CurrentryRestore && gameObject.activeSelf != false)
        { //when current mapping is same as save data
            if (PlayerNum != 1)
            {
                PlayerNumSituation = true;
                JoystickNum = 0;
            }
            else
            {
                if (SetActiveToOpenClose)
                    gameObject.SetActive(false);
                if (ClosingEvent != null)
                    ClosingEvent.Invoke();
            }
        }
        else if (CurrentryRestore != true && gameObject.activeSelf != false)
        { //when current mapping may be different from save data
            ExitWindowOpen();
        }
        else
        { //when mapping window closed
            if (ClosingEvent != null)
                ClosingEvent.Invoke();
        }
        KeyDelay = true;
        AxisDelay = true;
    }

    public void KeyDelayCall()
    {
        KeyDelay = true;
    }

    public bool KeyDelayNotify()
    {
        return KeyDelay;
    }

    public int JoystickNumNotify()
    {
        return JoystickNum;
    }

    public bool MappingPointerUpPreventNotify()
    {
        return MappingPointerUpPreventCheck;
    }

    void ReleaseFirstSet()
    {
        SelectPosition = 0;
        FirstSet = false;
        AxisDelay = true;
        KeyDelay = true;
    }

    void AnyPressProsess()
    {
        if (KeyDelay != true)
        {
            if (WindowSituation == 1)
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (CurrentInput == "Mouse" + (i))
                        return;
                }
                KeyDelay = true;
                AxisDelay = true;
                if (SaveNoSelectPosition)
                {
                    SaveConfirmWindow.SetActive(false);
                    WindowSituation = 0;
                }
                else
                {
                    SaveProcess();
                }
            }
            else if (WindowSituation == 2)
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (CurrentInput == "Mouse" + (i))
                        return;
                }
                KeyDelay = true;
                AxisDelay = true;
                if (ExitSelectPosition == 0)
                {
                    SaveProcess();
                }
                if (ExitSelectPosition == 0 || ExitSelectPosition == 1)
                {
                    ExitWindow.SetActive(false);
                    WindowSituation = 0;
                    FirstSet = true;
                    if (PlayerNum != 1)
                    {
                        PlayerNumSituation = true;
                        JoystickNum = 0;
                    }
                    else
                    {
                        if (SetActiveToOpenClose)
                            gameObject.SetActive(false);
                        if (ClosingEvent != null)
                            ClosingEvent.Invoke();
                    }
                }
                else
                {
                    ExitWindow.SetActive(false);
                    WindowSituation = 0;
                }
            }
            else if (PlayerNumSituation && gameObject.activeSelf != false)
            {
                for (int i = 0; i <= 9; i++)
                {
                    if (CurrentInput == "Mouse" + (i))
                        return;
                }
                if (CloseButtonSelect)
                {
                    if (SetActiveToOpenClose)
                    {
                        gameObject.SetActive(false);
                        PlayerSelectNum = 1;
                        CloseButtonSelect = false;
                    }
                    if (ClosingEvent != null)
                        ClosingEvent.Invoke();
                    return;
                }
                TidyMenuItemText();
                KeyDelay = true;
                AxisDelay = true;
                FirstSet = true;
                SelectPosition = 0;
                OperateItemLine = false;
                CurrentryRestore = true;
                CloseButtonSelect = false;
                PlayerNumSituation = false;
            }
            else
            {
                if (OperateItemLine != true)
                {
                    if (MappingMode != true)
                    {
                        for (int i = 0; i <= 9; i++)
                        {
                            if (CurrentInput == "Mouse" + (i))
                                return;
                        }
                        if (InhibitMappingModeCheck != true)
                        {
                            MappingMode = true;
                            KeyDelay = true;
                            AxisDelay = true;
                        }
                    }
                    else
                    {
                        if (CullentTextCpnt != null)
                            CullentTextCpnt.text = CurrentInput;
                        if (CullentTextMesh != null)
                            CullentTextMesh.text = CurrentInput;
                        InputSetting(CurrentInput);
                        MappingMode = false;
                        MappingPointerUpPreventCheck = true;
                        KeyDelay = true;
                        AxisDelay = true;
                        PreviousText = null;
                        CurrentryRestore = false;
                    }
                }
                else
                {
                    for (int i = 0; i <= 9; i++)
                    {
                        if (CurrentInput == "Mouse" + (i))
                            return;
                    }
                    KeyDelay = true;
                    AxisDelay = true;
                    if (CurrentryRestore)
                    {
                        if (OperateLineSelectPosition == 2)
                        {
                            DefaultInputSet();
                        }
                        else if (OperateLineSelectPosition == 3)
                        {
                            FirstSet = true;
                            if (PlayerNum != 1)
                            {
                                PlayerNumSituation = true;
                                JoystickNum = 0;
                            }
                            else
                            {
                                if (SetActiveToOpenClose)
                                    gameObject.SetActive(false);
                                if (ClosingEvent != null)
                                    ClosingEvent.Invoke();
                            }
                        }
                    }
                    else
                    {
                        switch (OperateLineSelectPosition)
                        {
                            case 0:
                                SaveConfirmWindowOpen();
                                break;
                            case 1:
                                RestorePrevious();
                                break;
                            case 2:
                                DefaultInputSet();
                                break;
                            case 3:
                                ExitWindowOpen();
                                break;
                        }
                    }
                }
            }

        }

    }
}
