using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class Mapper : MonoBehaviour
{
    public static string[] InputArray;
    public static string[] InputArray2p;
    public static string[] InputArray3p;
    public static string[] InputArray4p;
    [SerializeField]
    jInput
        jInputSOData;
    [HideInInspector]
    public string[]
        AllJoinInputArray;
    [HideInInspector]
    public string
        MapperSaveData;
    [HideInInspector]
    public bool
        IstCheck;
    List<int> UpMoveNumList = new List<int>();
    List<int> DownMoveNumList = new List<int>();
    List<int> RightMoveNumList = new List<int>();
    List<int> LeftMoveNumList = new List<int>();
    List<int> UGUISubmitNumList = new List<int>();
    List<int> UGUICancelNumList = new List<int>();
    public static bool jInputOnUp;
    public static bool jInputOnUp2p;
    public static bool jInputOnUp3p;
    public static bool jInputOnUp4p;
    public static bool jInputOnDown;
    public static bool jInputOnDown2p;
    public static bool jInputOnDown3p;
    public static bool jInputOnDown4p;
    public static bool jInputOnRight;
    public static bool jInputOnRight2p;
    public static bool jInputOnRight3p;
    public static bool jInputOnRight4p;
    public static bool jInputOnLeft;
    public static bool jInputOnLeft2p;
    public static bool jInputOnLeft3p;
    public static bool jInputOnLeft4p;
    public static bool UGUIOnSubmit;
    public static bool UGUIOnSubmit2p;
    public static bool UGUIOnSubmit3p;
    public static bool UGUIOnSubmit4p;
    public static bool UGUIOnCancel;
    public static bool UGUIOnCancel2p;
    public static bool UGUIOnCancel3p;
    public static bool UGUIOnCancel4p;
    static bool MoveDelayCallCheck;
    bool LevelLoadedCheck;
    bool BreakCheck;
    bool[] AllowMoveCheckAry = new bool[5];
    //index[0] is not use
    bool[] AllowRepeatCheckAry = new bool[5];
    int[] PrevAxisIndexAry = new int[5];
    int[] ConsecutiveMoveCountAry = new int[5];
    float[] PrevActionTimeAry = new float[5];
    float[] RepeatActionTimeAry = new float[5];
    string TempInputName;
    int CollateJoystickNum;
    static int PlayerNum;
    static int InputLength;
    SaveLoadScript SaveLoad;
    public static jInputSettings SetScript;
    StandaloneInputModule UGUIInputModule;
    static jInputUGUIOperationSender UGUIOperateScript;
    float m_InputActionsPerSecond;
    float m_RepeatDelay;
    #if (UNITY_EDITOR)
    public static bool
        EditorPlaymodeStop;
    #endif

    public void LoadfailureDeal()
    {

        //write to deal with when have failed to operate load data of input mapping

    }

    public void SaveFileDelete()
    {
#if (UNITY_STANDALONE || UNITY_EDITOR)
        if (SaveLoad != null)
        {
            SaveLoad = gameObject.GetComponent<SaveLoadScript>();
        }
        SaveLoad.DeleteInputSetting();
#else
			PlayerPrefs.DeleteKey("PrefsMappingData");
#endif
        Debug.LogWarning("[jInput] jInput have deleted the save data file. It is wrong Dafault Input Mapping name or there is a suspicion that the save data file was broken.");
        Debug.Break();

#if (UNITY_EDITOR)
        EditorPlaymodeStop = true;
        return;
#else
			SetScriptSearch ();
			if (SetScript != null){
				SetScript.ResetSet ();
			}
			Start ();
#endif
    }

    public void SaveSucsessIndicate()
    {
        if (SetScript != null)
            SetScript.SaveSucsessIndicate();
    }

    public void SavefailureIndicate()
    {
        if (SetScript != null)
            SetScript.SavefailureIndicate();
    }

    public void LoadData()
    {
#if (UNITY_STANDALONE || UNITY_EDITOR)
        SaveLoad.LoadInputSetting();
#else
				MapperSaveData = PlayerPrefs.GetString("PrefsMappingData");
#endif
        AllJoinInputArray = MapperSaveData.Split(',');
        Array.Copy(AllJoinInputArray, 0, Mapper.InputArray, 0, InputLength);
        if (PlayerNum >= 2)
        {
            Array.Copy(AllJoinInputArray, InputLength, Mapper.InputArray2p, 0, InputLength);
        }
        if (PlayerNum >= 3)
        {
            Array.Copy(AllJoinInputArray, InputLength * 2, Mapper.InputArray3p, 0, InputLength);
        }
        if (PlayerNum >= 4)
        {
            Array.Copy(AllJoinInputArray, InputLength * 3, Mapper.InputArray4p, 0, InputLength);
        }
    }

    public void SaveData()
    {
        MapperSaveData = string.Join(",", AllJoinInputArray);
#if (UNITY_STANDALONE || UNITY_EDITOR)
        SaveLoad.SaveInputSetting();
#else
				PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
				PlayerPrefs.Save();
#endif
    }

    void Start()
    {
#if (UNITY_EDITOR)
        EditorPlaymodeStop = false;
#endif
        SetScriptSearch();
        SODataConfirm();
        UGUIOperateScript = GetComponent<jInputUGUIOperationSender>();

        if (SetScript != null)
        {
            PlayerNum = SetScript.PlayerNum;
            InputLength = SetScript.DefaultInputArray.Length;
            UpMoveNumList = SetScript.UpMoveNumList;
            DownMoveNumList = SetScript.DownMoveNumList;
            RightMoveNumList = SetScript.RightMoveNumList;
            LeftMoveNumList = SetScript.LeftMoveNumList;
            UGUISubmitNumList = SetScript.UGUISubmitNumList;
            UGUICancelNumList = SetScript.UGUICancelNumList;
        }
        else
        {
            if (jInputSOData != null)
            {
                PlayerNum = jInputSOData.PlayerNum;
                InputLength = jInputSOData.DefaultInputArray.Count;
                UpMoveNumList = jInputSOData.UpMoveNumList;
                DownMoveNumList = jInputSOData.DownMoveNumList;
                RightMoveNumList = jInputSOData.RightMoveNumList;
                LeftMoveNumList = jInputSOData.LeftMoveNumList;
                UGUISubmitNumList = jInputSOData.UGUISubmitNumList;
                UGUICancelNumList = jInputSOData.UGUICancelNumList;
            }
        }
        if (PlayerNum <= 0 || InputLength <= 0)
        {
            ErrorStopEditor();
        }
#if (UNITY_STANDALONE || UNITY_EDITOR)
        if (SaveLoad == null)
            SaveLoad = gameObject.GetComponent<SaveLoadScript>();
#endif
        if (AllJoinInputArray.Length <= 0)
        {
            AllJoinInputArray = new string[InputLength * PlayerNum + 1];
        }
        if (InputArray == null)
        {
            InputArray = new string[InputLength];
            if (SetScript != null)
            {
                SetScript.DefaultInputArray.CopyTo(AllJoinInputArray, 0);
            }
            else
            {
                if (jInputSOData.DefaultInputArray.Count == 0 || jInputSOData.DefaultInputArray[0] == null)
                {
                    ErrorStopEditor();
                }
                else
                {
                    jInputSOData.DefaultInputArray.CopyTo(AllJoinInputArray, 0);
                }
            }
            LevelLoadedCheck = true;
        }
        if (PlayerNum >= 2)
        {
            if (InputArray2p == null)
            {
                InputArray2p = new string[InputLength];
                if (SetScript != null)
                {
                    SetScript.DefaultInputArray2p.CopyTo(AllJoinInputArray, InputLength);
                }
                else
                {
                    if (jInputSOData.DefaultInputArray2p.Count == 0 || jInputSOData.DefaultInputArray2p[0] == null || jInputSOData.DefaultInputArray2p.Count != InputLength)
                    {
                        ErrorStopEditor();
                    }
                    else
                    {
                        jInputSOData.DefaultInputArray2p.CopyTo(AllJoinInputArray, InputLength);
                    }
                }
                LevelLoadedCheck = true;
            }
        }
        if (PlayerNum >= 3)
        {
            if (InputArray3p == null)
            {
                InputArray3p = new string[InputLength];
                if (SetScript != null)
                {
                    SetScript.DefaultInputArray3p.CopyTo(AllJoinInputArray, InputLength * 2);
                }
                else
                {
                    if (jInputSOData.DefaultInputArray3p.Count == 0 || jInputSOData.DefaultInputArray3p[0] == null || jInputSOData.DefaultInputArray3p.Count != InputLength)
                    {
                        ErrorStopEditor();
                    }
                    else
                    {
                        jInputSOData.DefaultInputArray3p.CopyTo(AllJoinInputArray, InputLength * 2);
                    }
                }
                LevelLoadedCheck = true;
            }
        }
        if (PlayerNum >= 4)
        {
            if (InputArray4p == null)
            {
                InputArray4p = new string[InputLength];
                if (SetScript != null)
                {
                    SetScript.DefaultInputArray4p.CopyTo(AllJoinInputArray, InputLength * 3);
                }
                else
                {
                    if (jInputSOData.DefaultInputArray4p.Count == 0 || jInputSOData.DefaultInputArray4p[0] == null || jInputSOData.DefaultInputArray4p.Count != InputLength)
                    {
                        ErrorStopEditor();
                    }
                    else
                    {
                        jInputSOData.DefaultInputArray4p.CopyTo(AllJoinInputArray, InputLength * 3);
                    }
                }
                LevelLoadedCheck = true;
            }
        }

        if (LevelLoadedCheck != false)
        {
            MapperSaveData = string.Join(",", AllJoinInputArray);

#if (UNITY_STANDALONE || UNITY_EDITOR)
            SaveLoad.LoadInputSetting();
#else
				if(PlayerPrefs.HasKey("PrefsMappingData")) {
					MapperSaveData = PlayerPrefs.GetString("PrefsMappingData");
				}else {
					PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
				}
#endif

            string[] LoadArray = MapperSaveData.Split(',');
            if (AllJoinInputArray.Length != LoadArray.Length)
            {
                Debug.LogWarning("[jInput] SaveData items are different number of current input items.");
                MapperSaveData = string.Join(",", AllJoinInputArray);

#if (UNITY_STANDALONE || UNITY_EDITOR)
                SaveLoad.DeleteInputSetting();
                SaveLoad.SaveInputSetting();
#else
								PlayerPrefs.DeleteKey("PrefsMappingData");
								PlayerPrefs.SetString("PrefsMappingData", MapperSaveData);
#endif
                Debug.LogWarning("[jInput] jInputMapping was created new SaveData with Default Input Mapping.");
            }
            else
            {
                AllJoinInputArray = MapperSaveData.Split(',');
            }
            Array.Copy(AllJoinInputArray, 0, InputArray, 0, InputLength);
            if (PlayerNum >= 2)
            {
                Array.Copy(AllJoinInputArray, InputLength, InputArray2p, 0, InputLength);
            }
            if (PlayerNum >= 3)
            {
                Array.Copy(AllJoinInputArray, InputLength * 2, InputArray3p, 0, InputLength);
            }
            if (PlayerNum >= 4)
            {
                Array.Copy(AllJoinInputArray, InputLength * 3, InputArray4p, 0, InputLength);
            }
        }
        LevelLoadedCheck = false;
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < 5; i++)
        {
            AllowMoveCheckAry[i] = false;
            AllowRepeatCheckAry[i] = false;
            PrevAxisIndexAry[i] = 0;
            PrevActionTimeAry[i] = 0;
            RepeatActionTimeAry[i] = 0;
            ConsecutiveMoveCountAry[i] = 0;
        }
    }
    //Start() End

    #if (UNITY_5_4_OR_NEWER)
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode m)
    {
        Start();
    }

#else
    void OnLevelWasLoaded()
    {
        Start();
    }
    #endif

    void ErrorStopEditor()
    {
        Debug.LogError("[jInput] Error!! To verify jInput Settings and re-create the input mapping data!!");
        Debug.Break();
#if (UNITY_EDITOR)
        EditorPlaymodeStop = true;
        return;
#else
				return;
#endif
    }

    void Update()
    {

        SetScriptSearch();
        SODataConfirm();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SetScript != null && SetScript.UseEscDefinitedBehavior)
                SetScript.EscBehavior();
        }

        jInputOnUp = jInputOnUp2p = jInputOnUp3p = jInputOnUp4p = false;
        jInputOnDown = jInputOnDown2p = jInputOnDown3p = jInputOnDown4p = false;
        jInputOnRight = jInputOnRight2p = jInputOnRight3p = jInputOnRight4p = false;
        jInputOnLeft = jInputOnLeft2p = jInputOnLeft3p = jInputOnLeft4p = false;
        UGUIOnSubmit = UGUIOnSubmit2p = UGUIOnSubmit3p = UGUIOnSubmit4p = false;
        UGUIOnCancel = UGUIOnCancel2p = UGUIOnCancel3p = UGUIOnCancel4p = false;
        float[,] PlayerAxesArys = new float[5, 4]; //[(int)player,(float)AxesValue] / player[0] is not used / AxesValue : 0=UpAxis, 1=DownAxis, 2=RightAxis, 3=LeftAxis
        if (SetScript != null && SetScript.transform.gameObject.activeSelf
            && PlayerNum > 1 && SetScript.PlayerNumSituation != true)
            CollateJoystickNum = SetScript.JoystickNumNotify();
        else
            CollateJoystickNum = 0;

        if (UpMoveNumList.Count > 0)
        {
            for (int j = 1; j <= PlayerNum; j++)
            { //各PlayerのUpのAxisの最大値を配列に取る
                for (int i = 0; i < UpMoveNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            TempInputName = InputArray[UpMoveNumList[i]];
                            break;
                        case 2:
                            TempInputName = InputArray2p[UpMoveNumList[i]];
                            break;
                        case 3:
                            TempInputName = InputArray3p[UpMoveNumList[i]];
                            break;
                        case 4:
                            TempInputName = InputArray4p[UpMoveNumList[i]];
                            break;
                    }
                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                    {
                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                            continue;
                    }
                    if (jInput.GetAxis(TempInputName) > PlayerAxesArys[j, 0])
                        PlayerAxesArys[j, 0] = jInput.GetAxis(TempInputName);
                }
            }
        }
        if (DownMoveNumList.Count > 0)
        {
            for (int j = 1; j <= PlayerNum; j++)
            { //各PlayerのDownのAxisの最大値を配列に取る
                for (int i = 0; i < DownMoveNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            TempInputName = InputArray[DownMoveNumList[i]];
                            break;
                        case 2:
                            TempInputName = InputArray2p[DownMoveNumList[i]];
                            break;
                        case 3:
                            TempInputName = InputArray3p[DownMoveNumList[i]];
                            break;
                        case 4:
                            TempInputName = InputArray4p[DownMoveNumList[i]];
                            break;
                    }
                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                    {
                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                            continue;
                    }
                    if (jInput.GetAxis(TempInputName) > PlayerAxesArys[j, 1])
                        PlayerAxesArys[j, 1] = jInput.GetAxis(TempInputName);
                }
            }
        }
        if (RightMoveNumList.Count > 0)
        {
            for (int j = 1; j <= PlayerNum; j++)
            { //各PlayerのRightのAxisの最大値を配列に取る
                for (int i = 0; i < RightMoveNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            TempInputName = InputArray[RightMoveNumList[i]];
                            break;
                        case 2:
                            TempInputName = InputArray2p[RightMoveNumList[i]];
                            break;
                        case 3:
                            TempInputName = InputArray3p[RightMoveNumList[i]];
                            break;
                        case 4:
                            TempInputName = InputArray4p[RightMoveNumList[i]];
                            break;
                    }
                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                    {
                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                            continue;
                    }
                    if (jInput.GetAxis(TempInputName) > PlayerAxesArys[j, 2])
                        PlayerAxesArys[j, 2] = jInput.GetAxis(TempInputName);
                }
            }
        }
        if (LeftMoveNumList.Count > 0)
        {
            for (int j = 1; j <= PlayerNum; j++)
            { //各PlayerのLeftのAxisの最大値を配列に取る
                for (int i = 0; i < LeftMoveNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            TempInputName = InputArray[LeftMoveNumList[i]];
                            break;
                        case 2:
                            TempInputName = InputArray2p[LeftMoveNumList[i]];
                            break;
                        case 3:
                            TempInputName = InputArray3p[LeftMoveNumList[i]];
                            break;
                        case 4:
                            TempInputName = InputArray4p[LeftMoveNumList[i]];
                            break;
                    }
                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                    {
                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                            continue;
                    }
                    if (jInput.GetAxis(TempInputName) > PlayerAxesArys[j, 3])
                        PlayerAxesArys[j, 3] = jInput.GetAxis(TempInputName);
                }
            }
        }
        if (UGUICancelNumList.Count > 0)
        {
            BreakCheck = false;
            for (int j = 1; j <= PlayerNum; j++)
            {
                for (int i = 0; i < UGUICancelNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            TempInputName = InputArray[UGUICancelNumList[i]];
                            if (jInput.GetKeyDown(TempInputName))
                            {
                                if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                {
                                    if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                        continue;
                                }
                                UGUIOnCancel = true;
                                BreakCheck = true;
                            }
                            break;
                        case 2:
                            TempInputName = InputArray2p[UGUICancelNumList[i]];
                            if (jInput.GetKeyDown(TempInputName))
                            {
                                if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                {
                                    if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                        continue;
                                }
                                UGUIOnCancel2p = true;
                                BreakCheck = true;
                            }
                            break;
                        case 3:
                            TempInputName = InputArray3p[UGUICancelNumList[i]];
                            if (jInput.GetKeyDown(TempInputName))
                            {
                                if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                {
                                    if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                        continue;
                                }
                                UGUIOnCancel3p = true;
                                BreakCheck = true;
                            }
                            break;
                        case 4:
                            TempInputName = InputArray4p[UGUICancelNumList[i]];
                            if (jInput.GetKeyDown(TempInputName))
                            {
                                if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                {
                                    if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                        continue;
                                }
                                UGUIOnCancel4p = true;
                                BreakCheck = true;
                            }
                            break;
                    }
                    if (BreakCheck)
                    {
                        BreakCheck = false;
                        break;
                    }
                }
            }
        }
        if (UGUISubmitNumList.Count > 0)
        {
            BreakCheck = false;
            for (int j = 1; j <= PlayerNum; j++)
            {
                for (int i = 0; i < UGUISubmitNumList.Count; i++)
                {
                    switch (j)
                    {
                        case 1:
                            if (UGUIOnCancel)
                            {
                                BreakCheck = true;
                            }
                            else
                            {
                                TempInputName = InputArray[UGUISubmitNumList[i]];
                                if (jInput.GetKeyDown(TempInputName))
                                {
                                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                    {
                                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                            continue;
                                    }
                                    UGUIOnSubmit = true;
                                    BreakCheck = true;
                                }
                            }
                            break;
                        case 2:
                            if (UGUIOnCancel2p)
                            {
                                BreakCheck = true;
                            }
                            else
                            {
                                TempInputName = InputArray2p[UGUISubmitNumList[i]];
                                if (jInput.GetKeyDown(TempInputName))
                                {
                                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                    {
                                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                            continue;
                                    }
                                    UGUIOnSubmit2p = true;
                                    BreakCheck = true;
                                }
                            }
                            break;
                        case 3:
                            if (UGUIOnCancel3p)
                            {
                                BreakCheck = true;
                            }
                            else
                            {
                                TempInputName = InputArray3p[UGUISubmitNumList[i]];
                                if (jInput.GetKeyDown(TempInputName))
                                {
                                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                    {
                                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                            continue;
                                    }
                                    UGUIOnSubmit3p = true;
                                    BreakCheck = true;
                                }
                            }
                            break;
                        case 4:
                            if (UGUIOnCancel4p)
                            {
                                BreakCheck = true;
                            }
                            else
                            {
                                TempInputName = InputArray4p[UGUISubmitNumList[i]];
                                if (jInput.GetKeyDown(TempInputName))
                                {
                                    if (CollateJoystickNum != 0 && TempInputName.IndexOf("Joystick") == 0)
                                    {
                                        if (CollateJoystickNum != Convert.ToInt32(TempInputName.Substring(8, 1)))
                                            continue;
                                    }
                                    UGUIOnSubmit4p = true;
                                    BreakCheck = true;
                                }
                            }
                            break;
                    }
                    if (BreakCheck)
                    {
                        BreakCheck = false;
                        break;
                    }
                }
            }
        }

        if (EventSystem.current != null && EventSystem.current.currentInputModule != null)
        {
            UGUIInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
            m_InputActionsPerSecond = UGUIInputModule.inputActionsPerSecond;
            m_RepeatDelay = UGUIInputModule.repeatDelay;
        }
        else
        {
            m_InputActionsPerSecond = 7;
            m_RepeatDelay = 0.7f;
        }
        float ScaleTime = Time.unscaledTime;
        for (int i = 1; i <= PlayerNum; i++)
        {
            if (MoveDelayCallCheck)
                PrevActionTimeAry[i] = ScaleTime;
            if (ScaleTime < PrevActionTimeAry[i] + (1.0f / m_InputActionsPerSecond))
                AllowMoveCheckAry[i] = false;
            else
                AllowMoveCheckAry[i] = true;
        }
        MoveDelayCallCheck = false;
        for (int k = 1; k <= PlayerNum; k++)
        {
            if (PlayerAxesArys[k, 0] < 0.35f && PlayerAxesArys[k, 1] < 0.35f && PlayerAxesArys[k, 2] < 0.35f && PlayerAxesArys[k, 3] < 0.35f)
            {
                PrevAxisIndexAry[k] = -1;
                continue;
            }
            else
            {
                int MaxAxisIndex = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (PlayerAxesArys[k, MaxAxisIndex] < PlayerAxesArys[k, i])
                        MaxAxisIndex = i;
                }
                if (PrevAxisIndexAry[k] != MaxAxisIndex)
                {
                    AllowRepeatCheckAry[k] = true;
                    RepeatActionTimeAry[k] = ScaleTime;
                }
                else
                {
                    if (ScaleTime < RepeatActionTimeAry[k] + m_RepeatDelay)
                        AllowRepeatCheckAry[k] = false;
                    else
                        AllowRepeatCheckAry[k] = true;
                }
                PrevAxisIndexAry[k] = MaxAxisIndex;
                if (AllowMoveCheckAry[k] && AllowRepeatCheckAry[k])
                {
                    PrevActionTimeAry[k] = ScaleTime;
                    switch (MaxAxisIndex)
                    {
                        case 0:
                            if (k == 1)
                                jInputOnUp = true;
                            else if (k == 2)
                                jInputOnUp2p = true;
                            else if (k == 3)
                                jInputOnUp3p = true;
                            else if (k == 4)
                                jInputOnUp4p = true;
                            break;
                        case 1:
                            if (k == 1)
                                jInputOnDown = true;
                            else if (k == 2)
                                jInputOnDown2p = true;
                            else if (k == 3)
                                jInputOnDown3p = true;
                            else if (k == 4)
                                jInputOnDown4p = true;
                            break;
                        case 2:
                            if (k == 1)
                                jInputOnRight = true;
                            else if (k == 2)
                                jInputOnRight2p = true;
                            else if (k == 3)
                                jInputOnRight3p = true;
                            else if (k == 4)
                                jInputOnRight4p = true;
                            break;
                        case 3:
                            if (k == 1)
                                jInputOnLeft = true;
                            else if (k == 2)
                                jInputOnLeft2p = true;
                            else if (k == 3)
                                jInputOnLeft3p = true;
                            else if (k == 4)
                                jInputOnLeft4p = true;
                            break;
                    }
                }
            }
        }

    }
    //Update() End

    void SODataConfirm()
    {
        if (jInputSOData != null)
        {
            if (SetScript != null && SetScript.jInputSOData != null)
            {
                if (jInputSOData != SetScript.jInputSOData)
                {
                    SetScript.jInputSOData = jInputSOData;
                    Debug.LogWarning("[jInput] It was Different jInputSOData in jInputMappingManager and jInputSettings!!");
                }
            }
        }
        else
        {
            if (SetScript != null && SetScript.jInputSOData != null)
            {
                jInputSOData = SetScript.jInputSOData;
#if (UNITY_EDITOR)
            }
            else
            {
                jInputSOGetback();
#endif
            }
        }
    }

    #if (UNITY_EDITOR)
    void jInputSOGetback()
    {
        //Resources.Loadを使っても良い
        string[] Pathes = AssetDatabase.FindAssets("jInputData t:jInput");
        if (Pathes.Length >= 1)
        {
            string MappingManagerPath = AssetDatabase.GUIDToAssetPath(Pathes[0]);
            jInputSOData = AssetDatabase.LoadAssetAtPath<jInput>(MappingManagerPath);
            Debug.LogWarning("[jInput] jInputSOData is assigned in jInputMappingManager!!");
        }
        if (jInputSOData == null)
            Debug.LogError("[jInput] Error!! jInputSOData is not set in jInputMappingManager!!");
    }
    #endif

    public static void MoveDelayCall()
    {
        MoveDelayCallCheck = true;
    }

    public static void BackPlayerSelect()
    {
        SetScriptSearch();
        if (SetScript != null)
            SetScript.BackPlayerSelect();
        else
            Debug.LogError("[jInput] jInputSettings script is Not Found.");
    }

    public static void MappingWindowOpen()
    {
        SetScriptSearch();
        if (SetScript != null)
            SetScript.MappingWindowOpen();
        else
            Debug.LogError("[jInput] jInputSettings script is Not Found.");
    }

    public static void MappingWindowClose()
    {
        SetScriptSearch();
        if (SetScript != null)
            SetScript.MappingWindowClose();
        else
            Debug.LogError("[jInput] jInputSettings script is Not Found.");
    }

    static void SetScriptSearch()
    {
        if (jInputSettings.MappingSetList != null && jInputSettings.MappingSetList.Count > 0 && jInputSettings.MappingSetList[0] != null)
        {
            if (jInputSettings.MappingSetList[0].GetComponent<jInputSettings>())
                SetScript = jInputSettings.MappingSetList[0].GetComponent<jInputSettings>();
        }
    }
    //under from here, invalidete UGUI Operation methods
    public static void UGUIOperateValidate()
    {
        if (UGUIOperateScript != null)
        {
            UGUIOperateScript.UGUIOperationInvalid = true;
            UGUIOperateScript.UGUIOperationInvalid2p = true;
            UGUIOperateScript.UGUIOperationInvalid3p = true;
            UGUIOperateScript.UGUIOperationInvalid4p = true;
        }
    }

    public static void UGUIOperateValidate(int Num)
    {
        if (UGUIOperateScript != null)
        {
            switch (Num)
            {
                case 1:
                    UGUIOperateScript.UGUIOperationInvalid = true;
                    break;
                case 2:
                    UGUIOperateScript.UGUIOperationInvalid2p = true;
                    break;
                case 3:
                    UGUIOperateScript.UGUIOperationInvalid3p = true;
                    break;
                case 4:
                    UGUIOperateScript.UGUIOperationInvalid4p = true;
                    break;
            }
        }
    }

    public static void UGUIOperateValidate(int Num, bool OnlyCheck)
    {
        if (OnlyCheck != true)
        {
            UGUIOperateValidate(Num);
        }
        else
        {
            UGUIOperateScript.UGUIOperationInvalid = false;
            UGUIOperateScript.UGUIOperationInvalid2p = false;
            UGUIOperateScript.UGUIOperationInvalid3p = false;
            UGUIOperateScript.UGUIOperationInvalid4p = false;
            switch (Num)
            {
                case 1:
                    UGUIOperateScript.UGUIOperationInvalid = true;
                    break;
                case 2:
                    UGUIOperateScript.UGUIOperationInvalid2p = true;
                    break;
                case 3:
                    UGUIOperateScript.UGUIOperationInvalid3p = true;
                    break;
                case 4:
                    UGUIOperateScript.UGUIOperationInvalid4p = true;
                    break;
            }
        }
    }

    public static void UGUIOperateInvalidate()
    {
        if (UGUIOperateScript != null)
        {
            UGUIOperateScript.UGUIOperationInvalid = false;
            UGUIOperateScript.UGUIOperationInvalid2p = false;
            UGUIOperateScript.UGUIOperationInvalid3p = false;
            UGUIOperateScript.UGUIOperationInvalid4p = false;
        }
    }

    public static void UGUIOperateInvalidate(int Num)
    {
        if (UGUIOperateScript != null)
        {
            switch (Num)
            {
                case 1:
                    UGUIOperateScript.UGUIOperationInvalid = false;
                    break;
                case 2:
                    UGUIOperateScript.UGUIOperationInvalid2p = false;
                    break;
                case 3:
                    UGUIOperateScript.UGUIOperationInvalid3p = false;
                    break;
                case 4:
                    UGUIOperateScript.UGUIOperationInvalid4p = false;
                    break;
            }
        }
    }

    public static void UGUIOperateInvalidate(int Num, bool OnlyCheck)
    {
        if (OnlyCheck != true)
        {
            UGUIOperateInvalidate(Num);
        }
        else
        {
            UGUIOperateScript.UGUIOperationInvalid = true;
            UGUIOperateScript.UGUIOperationInvalid2p = true;
            UGUIOperateScript.UGUIOperationInvalid3p = true;
            UGUIOperateScript.UGUIOperationInvalid4p = true;
            switch (Num)
            {
                case 1:
                    UGUIOperateScript.UGUIOperationInvalid = false;
                    break;
                case 2:
                    UGUIOperateScript.UGUIOperationInvalid2p = false;
                    break;
                case 3:
                    UGUIOperateScript.UGUIOperationInvalid3p = false;
                    break;
                case 4:
                    UGUIOperateScript.UGUIOperationInvalid4p = false;
                    break;
            }
        }
    }

}
