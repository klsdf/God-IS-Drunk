using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DistortableRawImage))]
public class DistortableRawImageEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 让 Unity 画 RawImage 的默认内容
        DrawDefaultInspector();

        DistortableRawImage img = (DistortableRawImage)target;

        GUILayout.Label("🌟 顶点偏移设置", EditorStyles.boldLabel);

        img.topLeftOffset = EditorGUILayout.Vector2Field("Top Left", img.topLeftOffset);
        img.topRightOffset = EditorGUILayout.Vector2Field("Top Right", img.topRightOffset);
        img.bottomLeftOffset = EditorGUILayout.Vector2Field("Bottom Left", img.bottomLeftOffset);
        img.bottomRightOffset = EditorGUILayout.Vector2Field("Bottom Right", img.bottomRightOffset);

        if (GUI.changed)
        {
            img.SetVerticesDirty(); // 通知刷新
        }
    }
}
