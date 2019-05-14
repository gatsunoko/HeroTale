using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

#if (UNITY_EDITOR)
using UnityEditor;
#endif

[DisallowMultipleComponent]
public class UGUISameKeyOutline : Shadow
{

    //[HideInInspector]
    public Color OutlineColor = new Color(0.75f, 0.45f, 0.65f, 0.7f);
    //[HideInInspector]
    public bool UseOutline;
    Vector2 OutlineDistance = new Vector2(2, 2);

    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;
        if (UseOutline != true)
            return;

        //inspector欄の表示をなくす,空欄ではなく存在しないように見える
        //this.hideFlags = HideFlags.HideInInspector;
        //this.hideFlags = HideFlags.None;

        List<UIVertex> verts = new List<UIVertex>();
        vh.GetUIVertexStream(verts);

        int start = 0;
        int end = verts.Count;
        ApplyShadowZeroAlloc(verts, OutlineColor, start, verts.Count, OutlineDistance.x, OutlineDistance.y);

        start = end;
        end = verts.Count;
        ApplyShadowZeroAlloc(verts, OutlineColor, start, verts.Count, OutlineDistance.x, -OutlineDistance.y);

        start = end;
        end = verts.Count;
        ApplyShadowZeroAlloc(verts, OutlineColor, start, verts.Count, -OutlineDistance.x, OutlineDistance.y);

        start = end;
        end = verts.Count;
        ApplyShadowZeroAlloc(verts, OutlineColor, start, verts.Count, -OutlineDistance.x, -OutlineDistance.y);

        vh.Clear();
        vh.AddUIVertexTriangleStream(verts);
    }

}

#if (UNITY_EDITOR)
[CustomEditor(typeof(UGUISameKeyOutline))]
public class UGUISameKeyOutlineInspector : Editor
{
    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector ();
    }
}
#endif
