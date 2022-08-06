using UnityEngine;

[ExecuteInEditMode]
public class DrawBones : MonoBehaviour
{
#if UNITY_EDITOR
    public bool  ShowHierarchyAlwyas  = true;
    public bool  ShowSelectedBoneName = true;
    public Color BoneColor            = Color.white;
    public Color SelectedBoneColor    = Color.red;
    public Color BoneNameColor        = Color.cyan;
    public float JointSize            = 0.0066f;

    SkinnedMeshRenderer[] m_Renderers;
    GUIStyle              m_BoneNameStyle;

    void Start()
    {
        m_Renderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        if (m_Renderers == null || m_Renderers.Length == 0)
        {
            Debug.LogWarning("No SkinnedMeshRenderer found, script removed");
        }
    }

    void OnDrawGizmos()
    {
        if (ShowHierarchyAlwyas == false && UnityEditor.Selection.activeGameObject.GetComponentInParent<DrawBones>() != this)
            return;

        if (m_Renderers == null)
            return;

        if (m_BoneNameStyle == null) 
        {
            m_BoneNameStyle = new GUIStyle (GUI.skin.GetStyle ("Label"));
        }

        foreach (var render in m_Renderers) 
        {
            var bones = render.bones;
            foreach (var B in bones)
            {
                if (B.parent == null)
                    continue;

                bool selfSelected   = (UnityEditor.Selection.activeGameObject != null && B.name == UnityEditor.Selection.activeGameObject.name);
                bool parentSelected = (UnityEditor.Selection.activeGameObject != null && B.parent.name == UnityEditor.Selection.activeGameObject.name);

                if (!ShowSelectedBoneName || selfSelected || parentSelected) 
                {
                    m_BoneNameStyle.normal.textColor = BoneNameColor;
                    UnityEditor.Handles.Label(selfSelected ? B.position : B.parent.position, selfSelected ? B.name : B.parent.name, m_BoneNameStyle);
                }

                var color = Gizmos.color;
                {
                    if (parentSelected) 
                    {
                        Gizmos.color = SelectedBoneColor;
                        Gizmos.DrawWireSphere (B.parent.position, JointSize);
                        Gizmos.color = BoneColor;
                    }

                    Gizmos.color = selfSelected ? SelectedBoneColor : BoneColor;
                    Gizmos.DrawWireSphere (B.position, JointSize);
                    Gizmos.DrawLine(B.position, B.parent.position);
                    Gizmos.color = color;
                }
            }
        }
    }
#endif
}