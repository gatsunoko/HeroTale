using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UGUIButtonTextColor : MonoBehaviour
{
    [Space(5)]
    public Color HighlightedTextColor = new Color(0.95f, 0.95f, 0.95f, 1.0f);
    public Color MappingModeTextColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color UnInteractableTextColor = new Color(0.5f, 0.5f, 0.5f, 0.7f);
    float TextAlphaRatio;
    Color NormalTextColor;
    Text ThisText;
    Button ThisButton;
    jInputSettings SetScript;
    GameObject ParentGO;
    string HoldingTextString;

    void Awake()
    {
        if (ThisText == null)
            ThisText = GetComponent<Text>();
        if (ThisText != null)
            NormalTextColor = ThisText.color;
        else
            Debug.LogError("[jInput] Error!! Text component is Not Found!!");
    }

    void Start()
    {
        if (SetScript == null)
            SetScript = GetComponentInParent<jInputSettings>();
        if (SetScript == null)
            Debug.LogError("[jInput] jInputSettings script is Not Found!!");
        ParentGO = transform.parent.gameObject;
        if (ParentGO == null)
            Debug.LogError("[jInput] Error!! ParentGO is Not Found!!");
        if (ParentGO.GetComponent<Button>())
            ThisButton = ParentGO.GetComponent<Button>();
        else if (GetComponent<Button>())
            ThisButton = GetComponent<Button>();
    }

    void Update()
    {
        if (ThisText == null)
            return;
        if (SetScript != null && SetScript.MappingMode != true)
            HoldingTextString = ThisText.text;
        if (EventSystem.current.currentSelectedGameObject == this.gameObject
            || EventSystem.current.currentSelectedGameObject == ParentGO)
        {
            if (SetScript != null && SetScript.MappingMode)
            {
                ThisText.color = MappingModeTextColor;
                SetScript.PreviousText = HoldingTextString;
                ThisText.text = "Input...";
                if (GetComponent<UGUISameKeyOutline>() != null)
                    GetComponent<UGUISameKeyOutline>().UseOutline = false;
            }
            else
            {
                ThisText.color = HighlightedTextColor;
            }
        }
        else
        {
            ThisText.color = NormalTextColor;
        }

        if (ThisButton != null && ThisButton.interactable != true)
            ThisText.color = UnInteractableTextColor;

    }

    public void SetTextAlphaRatio(float Ratio)
    {
        TextAlphaRatio = Ratio;
    }

    void LateUpdate()
    {
        ThisText.color = new Color(ThisText.color.r, ThisText.color.g, ThisText.color.b, ThisText.color.a * (1.0f - TextAlphaRatio));
    }

}
