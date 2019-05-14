using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

[ExecuteInEditMode]
public class PlayerNumText : MonoBehaviour
{

    jInputSettings SetScript;
    TextMesh ThisTextMesh;
    Text ThisTextCpnt;
    public string Signage1p = "Player1";
    public string Signage2p = "Player2";
    public string Signage3p = "Player3";
    public string Signage4p = "Player4";

    void Start()
    {
        if (SetScript == null)
            SetScript = GetComponentInParent<jInputSettings>();
        if (SetScript == null)
            Debug.LogError("[jInput] Error!! jInputSettings script is Not Found!!");
        if (transform.Find("TextPrefab") && transform.Find("TextPrefab").GetComponent<TextMesh>())
            ThisTextMesh = transform.Find("TextPrefab").GetComponent<TextMesh>();
        if (GetComponent<Text>())
            ThisTextCpnt = GetComponent<Text>();
        if (ThisTextMesh == null && ThisTextCpnt == null)
            Debug.LogError("[jInput] Error!! Text Display component on PlayerSelectNumText is Not Found!!");
        if (ThisTextMesh != null)
            ThisTextMesh.text = "";
        if (ThisTextCpnt != null)
            ThisTextCpnt.text = "";
    }

    void Update()
    {
#if (UNITY_EDITOR)
        if (!EditorApplication.isPlaying && !EditorApplication.isPaused)
        {
            if (ThisTextMesh == null && transform.Find("TextPrefab") && transform.Find("TextPrefab").GetComponent<TextMesh>())
                ThisTextMesh = transform.Find("TextPrefab").GetComponent<TextMesh>();
            if (ThisTextCpnt == null && GetComponent<Text>())
                ThisTextCpnt = GetComponent<Text>();
            if (ThisTextMesh != null)
                ThisTextMesh.text = "PlayerXX";
            if (ThisTextCpnt != null)
                ThisTextCpnt.text = "PlayerXX";
            return;
        }

        if (SetScript.DefaKeySetModeCheck)
        {
            switch (SetScript.PlayerSelectNum)
            {
                case 1:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = "1P Default Input Setting Mode";
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = "1P Default Input Setting Mode";
                    break;
                case 2:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = "2P Default Input Setting Mode";
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = "2P Default Input Setting Mode";
                    break;
                case 3:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = "3P Default Input Setting Mode";
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = "3P Default Input Setting Mode";
                    break;
                case 4:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = "4P Default Input Setting Mode";
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = "4P Default Input Setting Mode";
                    break;
            }
            return;
        }
#endif

        if (SetScript.PlayerNumSituation != true && SetScript.PlayerNum != 1)
        {
            switch (SetScript.PlayerSelectNum)
            {
                case 1:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = Signage1p;
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = Signage1p;
                    break;
                case 2:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = Signage2p;
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = Signage2p;
                    break;
                case 3:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = Signage3p;
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = Signage3p;
                    break;
                case 4:
                    if (ThisTextMesh != null)
                        ThisTextMesh.text = Signage4p;
                    if (ThisTextCpnt != null)
                        ThisTextCpnt.text = Signage4p;
                    break;
            }
        }
        else
        {
            if (ThisTextMesh != null)
                ThisTextMesh.text = "";
            if (ThisTextCpnt != null)
                ThisTextCpnt.text = "";
        }

    }

}

