#if (UNITY_EDITOR)
using UnityEngine;
using System.Collections;

public class DemoNote : MonoBehaviour
{
    [SerializeField]
    GameObject
        MappingSet;
    jInputSettings jInputSetScript;


    void Update()
    {
        if (MappingSet != null)
        {
            if (jInputSetScript == null)
            {
                jInputSetScript = MappingSet.GetComponent<jInputSettings>();
            }
            if (jInputSetScript != null)
            {
                if (jInputSetScript.DefaKeySetModeCheck)
                    gameObject.SetActive(false);
            }
        }
    }

}
#endif
