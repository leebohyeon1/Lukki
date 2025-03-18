using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class UpdatableData : ScriptableObject {

	public event System.Action OnValuesUpdated;
	[LabelText("자동 업데이트")]
    public bool autoUpdate;

	#if UNITY_EDITOR

	protected virtual void OnValidate() {
		if (autoUpdate) {
			UnityEditor.EditorApplication.update += NotifyOfUpdatedValues;
		}
	}

	public void NotifyOfUpdatedValues() {
		UnityEditor.EditorApplication.update -= NotifyOfUpdatedValues;
		if (OnValuesUpdated != null) {
			OnValuesUpdated ();
		}
	}

	#endif

}
