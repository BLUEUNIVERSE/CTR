using UnityEngine;
using UnityEditor;
using System.Collections;
/// <summary>
/// June utilities.
/// Aniket Kayande
/// 28-09-2012
/// </summary>
public class JuneUtilities : EditorWindow 
{

	[MenuItem("InfernoUtilities/AttachChild #n")]
	static void AttachChild()
	{
		GameObject go = new GameObject("Child" + Time.timeSinceLevelLoad);
		go.transform.parent = Selection.activeGameObject.transform;
		go.transform.localPosition = Vector3.zero;
		
	}

	void OnGUI()
	{
	}
}
