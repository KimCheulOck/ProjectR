using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(Transform))]
public class TransformEditor : Editor {

    private const float FIELD_WIDTH = 250; //controls the width of the input fields
    //private const bool WIDE_MODE = true; //makes our controls draw inline

    private const float POSITION_MAX = 100000.0f;

    //private static GUIContent positionGUIContent = new GUIContent(LocalString("Position"), LocalString("The local position of this Game Object relative to the parent."));
    private static GUIContent rotationGUIContent = new GUIContent(LocalString("Rotation"), LocalString("The local rotation of this Game Object relative to the parent."));
    //private static GUIContent scaleGUIContent = new GUIContent(LocalString("Scale"), LocalString("The local scaling of this Game Object relative to the parent."));

    private SerializedProperty positionProperty; //The position of this transform
    private SerializedProperty rotationProperty; //The rotation of this transform
    private SerializedProperty scaleProperty; //The scale of this transform

    //References to some images for our GUI
    private static bool UniformScaling = false; //Are we using uniform scaling mode?

    private static Quaternion saveQuaternion;
    private static Vector3 saveScale;
    private static Vector3 savePosition;

    #region INITIALISATION

    public void OnEnable()
    {
        this.positionProperty = this.serializedObject.FindProperty("m_LocalPosition");
        this.rotationProperty = this.serializedObject.FindProperty("m_LocalRotation");
        this.scaleProperty = this.serializedObject.FindProperty("m_LocalScale");
        EditorApplication.update += EditorUpdate;
    }

    private void OnDisable()
    {
        EditorApplication.update -= EditorUpdate;
    }

    private void EditorUpdate()
    {
        Repaint();
    }
    #endregion

    /// <summary>
    /// Draws the inspector
    /// </summary>
    public override void OnInspectorGUI()
    {
        base.serializedObject.Update();
        //Draw the inputs
        DrawPositionElement();
        DrawRotationElement();
        DrawScaleElement();

        //Apply the settings to the object
        this.serializedObject.ApplyModifiedProperties();
    }

    /// <summary>
    /// Draws the input for the position
    /// </summary>
    private void DrawPositionElement()
    {
        if (ThinInspectorMode)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Position");
            DrawPositionSkill();
            GUILayout.EndHorizontal();
        }

        string label = ThinInspectorMode ? "" : "Position";

        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - FIELD_WIDTH - 64; // align field to right of inspector
        this.positionProperty.vector3Value = TransformUtilEditor.Vector3InputField(label, this.positionProperty.vector3Value);
        if (!ThinInspectorMode)
            DrawPositionSkill();
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = 0;
    }
    private void DrawPositionSkill()
    {
        if (GUILayout.Button("R", GUILayout.Width(20f)))
        {
            this.positionProperty.vector3Value = Vector3.zero;
        }

        if (GUILayout.Button("C", GUILayout.Width(20f)))
        {
            savePosition = this.positionProperty.vector3Value;
        }

        if (GUILayout.Button("V", GUILayout.Width(20f)))
        {
            this.positionProperty.vector3Value = savePosition;
        }
    }

    /// <summary>
    /// Draws the input for the rotation
    /// </summary>
    private void DrawRotationElement()
    {
        if (ThinInspectorMode)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Rotation");
            DrawRotationSkill();
            GUILayout.EndHorizontal();
        }

        //Rotation layout
        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - FIELD_WIDTH - 64; // align field to right of inspector
        this.RotationPropertyField(this.rotationProperty, ThinInspectorMode ? GUIContent.none : rotationGUIContent);
        if (!ThinInspectorMode)
            DrawRotationSkill();
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = 0;
    }
    private void DrawRotationSkill()
    {
        if (GUILayout.Button("R", GUILayout.Width(20f)))
        {
            this.rotationProperty.quaternionValue = Quaternion.identity;
        }

        if (GUILayout.Button("C", GUILayout.Width(20f)))
        {
            saveQuaternion = this.rotationProperty.quaternionValue;
        }

        if (GUILayout.Button("V", GUILayout.Width(20f)))
        {
            this.rotationProperty.quaternionValue = saveQuaternion;
        }
    }

    /// <summary>
    /// Draws the input for the scale
    /// </summary>
    private void DrawScaleElement()
    {
        if (ThinInspectorMode)
        {
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Scale");
            DrawScaleSkill();
            GUILayout.EndHorizontal();
        }
        string label = ThinInspectorMode ? "" : "Scale";

        //Scale Layout
        GUILayout.BeginHorizontal();
        EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - FIELD_WIDTH - 64; // align field to right of inspector
        this.scaleProperty.vector3Value = TransformUtilEditor.Vector3InputField(label, this.scaleProperty.vector3Value, false, UniformScaling, UniformScaling);
        if (!ThinInspectorMode)
            DrawScaleSkill();
        GUILayout.EndHorizontal();
        EditorGUIUtility.labelWidth = 0;
    }
    private void DrawScaleSkill()
    {
        if (GUILayout.Button("R", GUILayout.Width(20f)))
        {
            this.scaleProperty.vector3Value = Vector3.one;
        }

        if (GUILayout.Button("C", GUILayout.Width(20f)))
        {
            saveScale = this.scaleProperty.vector3Value;
        }

        if (GUILayout.Button("V", GUILayout.Width(20f)))
        {
            this.scaleProperty.vector3Value = saveScale;
        }
    }

    /// <summary>
    /// Returns the localised version of a string
    /// </summary>
    private static string LocalString(string text)
    {
        return text;
        //return LocalizationDatabase.GetLocalizedString(text);
    }

    private static bool ThinInspectorMode
    {

        get
        {
            return EditorGUIUtility.currentViewWidth <= 300;
        }

    }

    private bool ValidatePosition(Vector3 position)
    {
        if (Mathf.Abs(position.x) > POSITION_MAX) return false;
        if (Mathf.Abs(position.y) > POSITION_MAX) return false;
        if (Mathf.Abs(position.z) > POSITION_MAX) return false;
        return true;
    }

    private void RotationPropertyField(SerializedProperty rotationProperty, GUIContent content)
    {
        Transform transform = (Transform)this.targets[0];
        Quaternion localRotation = transform.localRotation;
        foreach (UnityEngine.Object t in (UnityEngine.Object[])this.targets)
        {
            if (!SameRotation(localRotation, ((Transform)t).localRotation))
            {
                EditorGUI.showMixedValue = true;
                break;
            }
        }

        EditorGUI.BeginChangeCheck();

        Vector3 eulerAngles = TransformUtilEditor.Vector3InputField(content.text, localRotation.eulerAngles);
        //Vector3 eulerAngles = EditorGUILayout.Vector3Field(content, localRotation.eulerAngles);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObjects(this.targets, "Rotation Changed");
            foreach (UnityEngine.Object obj in this.targets)
            {
                Transform t = (Transform)obj;
                t.localEulerAngles = eulerAngles;
            }
            rotationProperty.serializedObject.SetIsDifferentCacheDirty();
        }

        EditorGUI.showMixedValue = false;
    }

    private bool SameRotation(Quaternion rot1, Quaternion rot2)
    {
        if (rot1.x != rot2.x) return false;
        if (rot1.y != rot2.y) return false;
        if (rot1.z != rot2.z) return false;
        if (rot1.w != rot2.w) return false;
        return true;
    }
}
