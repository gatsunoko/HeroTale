using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

public class jInput : ScriptableObject
{
    [HideInInspector]
    public float
        DeadZone = 0.15f;
    [HideInInspector]
    public float
        Gravity = 5.0f;
    [HideInInspector]
    public float
        Sensitivity = 3.0f;
    static float AxisDeadZone;
    static float AxisGravity;
    static float AxisSensitivity;
    #if (UNITY_EDITOR)
    [HideInInspector]
    public bool
        AxesFoldoutBool;
    [HideInInspector]
    public bool
        OpenCloseSetFoldoutBool;
    [HideInInspector]
    public bool
        ExcludeDeviceFoldoutBool;
    [HideInInspector]
    public bool
        SameMappingFoldoutBool;
    [HideInInspector]
    public bool
        UnusableKeyFoldoutBool;
    [HideInInspector]
    public bool
        UGUINumListsFoldoutBool;
    [HideInInspector]
    public bool
        DefaKeyFoldoutBool;
    [HideInInspector]
    public bool
        DefaKey1pFoldoutBool;
    [HideInInspector]
    public bool
        DefaKey2pFoldoutBool;
    [HideInInspector]
    public bool
        DefaKey3pFoldoutBool;
    [HideInInspector]
    public bool
        DefaKey4pFoldoutBool;
    [HideInInspector]
    public bool
        DefaultInputNameInconsistencyCheck;
    [HideInInspector]
    public bool
        DefaKeyButtonOffCheck;
    [HideInInspector]
    public string
        DefaKeyInconsistencyListText;
    //SyncValuesData SyncValuesSOData;
    #endif
    [HideInInspector]
    public int
        PlayerNum = 1;
    [HideInInspector]
    public List<string>
        DefaultInputArray;
    [HideInInspector]
    public List<string>
        DefaultInputArray2p;
    [HideInInspector]
    public List<string>
        DefaultInputArray3p;
    [HideInInspector]
    public List<string>
        DefaultInputArray4p;
    [HideInInspector]
    public GameObject
        MappingManagerPrefab;
    [HideInInspector]
    public GameObject
        Ist;
    public static string[] AllInputNames;
    public static List<KeyCode> KeyValueList;
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
    [HideInInspector]
    public List<string>
        UnusableKeyList = new List<string>();

    void OnEnable()
    {
#if (UNITY_EDITOR)
        if (MappingManagerPrefab == null)
        {
            //Resources.Loadを使っても良い
            string[] Pathes = AssetDatabase.FindAssets("jInputMappingManager t:GameObject");
            if (Pathes.Length >= 1)
            {
                string MappingManagerPath = AssetDatabase.GUIDToAssetPath(Pathes[0]);
                MappingManagerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(MappingManagerPath);
            }
            if (Pathes.Length > 1)
            {
                Debug.LogError("[jInput] There are some GameObjects included in the name 'jInputMappingManager' in Project window!! There is a possibility that it may not operate normally.");
            }
            else if (Pathes.Length < 1)
            {
                Debug.LogError("[jInput] Error! jInputMappingManager prefab is Not Found in Project window!!");
            }
        }
#else
        if (GameObject.Find("jInputMappingManager") == null)
        {
            if (MappingManagerPrefab != null)
            {
                GameObject Ist = Instantiate(MappingManagerPrefab) as GameObject;
                Ist.name = "jInputMappingManager";
            }
            else
            {
                Debug.LogError("[jInput] Error! jInputMappingManager prefab is Not Found in Project window!!");
                return;
            }
        }
        else
        {
				
        }
#endif
        AxesSetApply();
        KeyListCreate();
        //DontDestroyOnLoad(this);
    }

    public void AxesSetApply()
    {
        AxisDeadZone = DeadZone;
        AxisGravity = Gravity;
        AxisSensitivity = Sensitivity;
#if (UNITY_EDITOR)
        EditorUtility.SetDirty(this);
#endif
    }

    void KeyListCreate()
    {
        Array KeyVals = Enum.GetValues(typeof(KeyCode));
        KeyValueList = new List<KeyCode>();
        for (int i = 0; i < KeyVals.Length; i++)
        {
            KeyCode key = (KeyCode)KeyVals.GetValue(i);
            if (key == KeyCode.Escape)
                continue;
            KeyValueList.Add(key);
        }
        for (int i = 0; i < 20; i++)
        {
            for (int x = 0; x < KeyValueList.Count; x++)
            {
                if (KeyValueList[x].ToString() == "JoystickButton" + i)
                {
                    KeyValueList.RemoveAt(x);
                    break;
                }
            }
        }
        List<string> InputNameList = new List<string>();
        InputNameList = KeyValueList.ConvertAll(e => e.ToString());
        //別の型のListから配列[]にする場合
        //string[] xxx = KeyValueList.ConvertAll(e => e.ToString()).ToArray();
        int tempInsertNum;
        if (InputNameList.IndexOf("Mouse6") >= 0)
            tempInsertNum = InputNameList.IndexOf("Mouse6") + 1;
        else
            tempInsertNum = InputNameList.Count;
        InputNameList.Insert(tempInsertNum, "MouseWheel-");
        InputNameList.Insert(tempInsertNum, "MouseWheel+");
        for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 20; j++)
            {
                InputNameList.Add("Joystick" + (i) + "Axis" + (j) + "+");
                InputNameList.Add("Joystick" + (i) + "Axis" + (j) + "-");
            }
        }
        AllInputNames = InputNameList.ToArray();
        AllInputNames[0] = " ";
    }

    /*SyncValuesData.csのScriptableObjectを取得し変数SyncValuesSODataに取る(ScriptableObjectが無ければ作成)
		#if (UNITY_EDITOR)
		void SyncValuesSOSetting ()
		{
				string[] Pathes = AssetDatabase.FindAssets ("SyncValuesData t:SyncValuesData");
				if (Pathes.Length >= 1) {
						string Path = AssetDatabase.GUIDToAssetPath (Pathes [0]);
						SyncValuesData SyncValuesSOData = AssetDatabase.LoadAssetAtPath<SyncValuesData> (Path);
				} else if (Pathes.Length < 1) {
						string Path = "Assets/jInputMapping/FunctionalFile/SyncValuesData.asset";
						AssetDatabase.CreateAsset (ScriptableObject.CreateInstance<SyncValuesData> (), Path);
						AssetDatabase.Refresh ();
                        #if UNITY_5_5_OR_NEWER
                            AssetDatabase.SaveAssets ();
                        #else
						    EditorApplication.SaveAssets ();
                        #endif
						SyncValuesData SyncValuesSOData = AssetDatabase.LoadAssetAtPath<SyncValuesData> (Path);
				}
				if (Pathes.Length > 1) {
						Debug.LogWarning ("[jInput] There are some ScriptableObjects included 'SyncValuesData' in the name in Project window!");
				}
		}
		#endif
		*/

    public void SOValueDetermine()
    {
#if (UNITY_EDITOR)
        EditorUtility.SetDirty(this);
#endif
    }

    public static float GetAxis(string IName)
    {
        string InputName = IName;
        string InputNameMend;
        float ValueGap;
        float SustainAxisValue = 0;
        float AxisValue = 0;
        float Computation = 0;

        try
        {
            if (AxisDic.ContainsKey(InputName))
            {
                SustainAxisValue = AxisDic[InputName];
            }
            else
            {
                AxisDic[InputName] = 0;
            }
            if (InputName.EndsWith("+"))
            {
                InputNameMend = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxis(InputNameMend);
                if (AxisValue > AxisDeadZone)
                {
                    ValueGap = AxisValue - SustainAxisValue;
                    if (-0.05f <= ValueGap && ValueGap <= 0.05f)
                    {
                        AxisValue = SustainAxisValue + (AxisValue - SustainAxisValue);
                    }
                    else if (0.05f < ValueGap)
                    {
                        AxisValue = SustainAxisValue + (0.01f * AxisSensitivity);
                    }
                    else if (ValueGap < -0.05f)
                    {
                        AxisValue = SustainAxisValue - (0.01f * AxisGravity);
                    }
                }
                else
                {
                    AxisValue = SustainAxisValue - (0.01f * AxisGravity);
                }
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
            }
            else if (InputName.EndsWith("-"))
            {
                InputNameMend = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxis(InputNameMend);
                if (AxisValue < -AxisDeadZone)
                {
                    AxisValue = Mathf.Abs(AxisValue);
                    ValueGap = AxisValue - SustainAxisValue;
                    if (-0.05f <= ValueGap && ValueGap <= 0.05f)
                    {
                        AxisValue = SustainAxisValue + (AxisValue - SustainAxisValue);
                    }
                    else if (0.05f < ValueGap)
                    {
                        AxisValue = SustainAxisValue + (0.01f * AxisSensitivity);
                    }
                    else if (ValueGap < -0.05f)
                    {
                        AxisValue = SustainAxisValue - (0.01f * AxisGravity);
                    }
                }
                else
                {
                    AxisValue = SustainAxisValue - (0.01f * AxisGravity);
                }
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                if (Input.GetKey(InputKeyCode))
                {
                    AxisValue = SustainAxisValue + (0.01f * AxisSensitivity);
                    if (AxisValue >= 1)
                    {
                        AxisValue = 1;
                    }
                }
                else
                {
                    AxisValue = SustainAxisValue - (0.01f * AxisGravity);
                    if (AxisValue <= 0)
                    {
                        AxisValue = 0;
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = AxisValue;
        AxisDic[InputName] = AxisValue;
        if (Computation > 0)
        {
            Computation = Computation + 0.07f;//0.07f is Margin of maxim
            Computation = Mathf.Clamp(Computation, 0, 1);
        }
        return Computation;
    }

    public static float GetAxisRaw(string IName)
    {
        string InputName = IName;
        float AxisValue = 0;
        float Computation = 0;

        try
        {
            if (InputName.EndsWith("+"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                if (AxisValue > 0.4f)
                {
                    AxisValue = 1;
                }
                else
                {
                    AxisValue = 0;
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                if (AxisValue < -0.4f)
                {
                    AxisValue = 1;
                }
                else
                {
                    AxisValue = 0;
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                if (Input.GetKey(InputKeyCode))
                {
                    AxisValue = 1;
                }
                else
                {
                    AxisValue = 0;
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = AxisValue;
        return Computation;
    }


    public static bool GetButton(string IName)
    {
        string InputName = IName;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;

        try
        {
            if (InputName.EndsWith("+"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.4f)
                {
                    ButtonBool = true;
                }
                else
                {
                    ButtonBool = false;
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.4f)
                {
                    ButtonBool = true;
                }
                else
                {
                    ButtonBool = false;
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKey(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        return Computation;
    }

    public static bool GetKey(string IName)
    {
        string InputName = IName;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;

        try
        {
            if (InputName.EndsWith("+"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.4f)
                {
                    ButtonBool = true;
                }
                else
                {
                    ButtonBool = false;
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputName = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputName);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.4f)
                {
                    ButtonBool = true;
                }
                else
                {
                    ButtonBool = false;
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKey(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        return Computation;
    }


    public static bool GetButtonDown(string IName)
    {
        string InputName = IName;
        string InputNameEW;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;
        bool UpDownDetection = false;

        if (BtnDownDetectionDic.ContainsKey(InputName))
        {
            if (BtnDownDetectionDic[InputName])
            {
                UpDownDetection = true;
            }
            else
            {
                UpDownDetection = false;
            }
        }
        else
        {
            BtnDownDetectionDic[InputName] = false;
            UpDownDetection = false;
        }
        try
        {
            if (InputName.EndsWith("+"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.35f)
                {
                    if (UpDownDetection != true)
                    {
                        UpDownDetection = true;
                        ButtonBool = true;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
                else
                {
                    ButtonBool = false;
                    UpDownDetection = false;
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.35f)
                {
                    if (UpDownDetection != true)
                    {
                        UpDownDetection = true;
                        ButtonBool = true;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
                else
                {
                    ButtonBool = false;
                    UpDownDetection = false;
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKeyDown(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        if (UpDownDetection)
        {
            BtnDownDetectionDic[InputName] = true;
        }
        else
        {
            BtnDownDetectionDic[InputName] = false;
        }
        return Computation;
    }

    public static bool GetKeyDown(string IName)
    {
        string InputName = IName;
        string InputNameEW;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;
        bool UpDownDetection;

        if (KeyDownDetectionDic.ContainsKey(InputName))
        {
            if (KeyDownDetectionDic[InputName])
            {
                UpDownDetection = true;
            }
            else
            {
                UpDownDetection = false;
            }
        }
        else
        {
            KeyDownDetectionDic[InputName] = false;
            UpDownDetection = false;
        }
        try
        {
            if (InputName.EndsWith("+"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.35f)
                {
                    if (UpDownDetection != true)
                    {
                        UpDownDetection = true;
                        ButtonBool = true;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
                else
                {
                    ButtonBool = false;
                    UpDownDetection = false;
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.35f)
                {
                    if (UpDownDetection != true)
                    {
                        UpDownDetection = true;
                        ButtonBool = true;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
                else
                {
                    ButtonBool = false;
                    UpDownDetection = false;
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKeyDown(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        if (UpDownDetection)
        {
            KeyDownDetectionDic[InputName] = true;
        }
        else
        {
            KeyDownDetectionDic[InputName] = false;
        }
        return Computation;
    }

    public static bool GetButtonUp(string IName)
    {
        string InputName = IName;
        string InputNameEW;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;
        bool UpDownDetection = false;

        if (BtnUpDetectionDic.ContainsKey(InputName))
        {
            if (BtnUpDetectionDic[InputName])
            {
                UpDownDetection = true;
            }
            else
            {
                UpDownDetection = false;
            }
        }
        else
        {
            BtnUpDetectionDic[InputName] = false;
            UpDownDetection = false;
        }
        try
        {
            if (InputName.EndsWith("+"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.35f)
                {
                    UpDownDetection = true;
                }
                else
                {
                    if (UpDownDetection)
                    {
                        ButtonBool = true;
                        UpDownDetection = false;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.35f)
                {
                    UpDownDetection = true;
                }
                else
                {
                    if (UpDownDetection)
                    {
                        ButtonBool = true;
                        UpDownDetection = false;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKeyUp(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        if (UpDownDetection)
        {
            BtnUpDetectionDic[InputName] = true;
        }
        else
        {
            BtnUpDetectionDic[InputName] = false;
        }
        return Computation;
    }

    public static bool GetKeyUp(string IName)
    {
        string InputName = IName;
        string InputNameEW;
        float AxisValue = 0;
        bool Computation = false;
        bool ButtonBool = false;
        bool UpDownDetection = false;

        if (KeyUpDetectionDic.ContainsKey(InputName))
        {
            if (KeyUpDetectionDic[InputName])
            {
                UpDownDetection = true;
            }
            else
            {
                UpDownDetection = false;
            }
        }
        else
        {
            KeyUpDetectionDic[InputName] = false;
            UpDownDetection = false;
        }
        try
        {
            if (InputName.EndsWith("+"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, 0, 1);
                if (AxisValue > 0.35f)
                {
                    UpDownDetection = true;
                }
                else
                {
                    if (UpDownDetection)
                    {
                        ButtonBool = true;
                        UpDownDetection = false;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
            }
            else if (InputName.EndsWith("-"))
            {
                InputNameEW = InputName.Substring(0, InputName.Length - 1);
                AxisValue = Input.GetAxisRaw(InputNameEW);
                AxisValue = Mathf.Clamp(AxisValue, -1, 0);
                if (AxisValue < -0.35f)
                {
                    UpDownDetection = true;
                }
                else
                {
                    if (UpDownDetection)
                    {
                        ButtonBool = true;
                        UpDownDetection = false;
                    }
                    else
                    {
                        ButtonBool = false;
                    }
                }
            }
            else if (InputName == "")
            {

            }
            else
            {
                KeyCode InputKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), InputName);
                ButtonBool = Input.GetKeyUp(InputKeyCode);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            InputNameUnsuitable();
        }
        Computation = ButtonBool;
        if (UpDownDetection)
        {
            KeyUpDetectionDic[InputName] = true;
        }
        else
        {
            KeyUpDetectionDic[InputName] = false;
        }
        return Computation;
    }

    static void InputNameUnsuitable()
    {
        Debug.LogError("[jInput] Being used Unsuitable Input Mapping Name!!\n             Or Input Mapping Array Number in source code do not exist in the actual array!!");
    }

    static Dictionary<string, float> AxisDic = new Dictionary<string, float>();
    static Dictionary<string, bool> BtnDownDetectionDic = new Dictionary<string, bool>();
    static Dictionary<string, bool> KeyDownDetectionDic = new Dictionary<string, bool>();
    static Dictionary<string, bool> BtnUpDetectionDic = new Dictionary<string, bool>();
    static Dictionary<string, bool> KeyUpDetectionDic = new Dictionary<string, bool>();

}
