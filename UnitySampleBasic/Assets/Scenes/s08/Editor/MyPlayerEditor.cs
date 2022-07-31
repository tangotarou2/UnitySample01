using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



[CustomEditor(typeof(MyPlayer))]
class MyPlayerEditor : Editor
{

	Matrix4x4 matrix = new Matrix4x4();

	float determinant3x3;
	float determinant4x4;

	Vector4 rhs;
	Vector4 result;

	public override void OnInspectorGUI()
	{
		// base.OnInspectorGUI ();

		// Hide Script property
		serializedObject.Update();
		DrawPropertiesExcluding(serializedObject, new string[] { "m_Script" });
		serializedObject.ApplyModifiedProperties();


		EditorGUI.BeginChangeCheck();
		EditorGUILayout.BeginVertical(GUI.skin.box);


		EditorGUILayout.LabelField(new GUIContent("Matrix4x4"));
		matrix.SetRow(0, Vector4.one);
		matrix.SetRow(1, Vector4.one);
		matrix.SetRow(2, Vector4.one);
		matrix.SetRow(3, Vector4.one);

		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUI.skin.box);

		determinant3x3 = EditorGUILayout.FloatField("Determinant (3x3)", determinant3x3);
		determinant4x4 = EditorGUILayout.FloatField("Determinant (4x4)", determinant4x4);

		EditorGUILayout.EndVertical();

		EditorGUILayout.Space();

		EditorGUILayout.BeginVertical(GUI.skin.box);

		rhs = EditorGUILayout.Vector4Field("RHS", rhs);

		EditorGUILayout.EndVertical();

		//EditorGUILayout.Space();

		if (GUILayout.Button("operator *")) {
			result = matrix * rhs;
		}




		if (EditorGUI.EndChangeCheck()) {
		//	determinant3x3 = getDeterminant3x3(matrix);
		//	determinant4x4 = getDeterminant4x4(matrix);

			Undo.RecordObject(target, "Chapter04EditorUndo");
			EditorUtility.SetDirty(target);
		}
	}

}
