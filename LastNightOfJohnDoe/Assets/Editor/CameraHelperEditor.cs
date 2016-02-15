using UnityEditor;
using UnityEngine;

public class CameraHelperEditor : EditorWindow {

	private Camera currentCamera;
	private CameraData currentCameraData;
	private static int numInstances = 0;

	[MenuItem ("Tools/Camera Data Helper")]
	public static void ShowWindow()
	{
		CameraHelperEditor window =  (CameraHelperEditor)EditorWindow.GetWindow(typeof(CameraHelperEditor));
		window.Show();
	}

	void OnGUI()
	{
		EditorGUILayout.LabelField("Camera data transform: ");
		currentCameraData = (CameraData) EditorGUILayout.ObjectField(currentCameraData, typeof(CameraData), true);

		EditorGUILayout.LabelField("Current camera: ");
		currentCamera = (Camera)EditorGUILayout.ObjectField(currentCamera, typeof(Camera), true);

		if(GUILayout.Button("Create new camera data"))
		{
			InstantiateNewCameraDataTransform();
		} 

		if(GUILayout.Button("Change current camera data"))
		{
			ChangeCurrentCameraData();
		}

		if(GUILayout.Button("Set Camera to current cameraData"))
		{
			SetCameraToCurrentCameraData();
		}
		
	}

	private void InstantiateNewCameraDataTransform()
	{
		if (currentCamera == null)
		{
			Debug.LogError("Debes asignar una camara primero!");
		}
		else
		{
			GameObject go = new GameObject();

			//go.name = "Transform Camera Data " + numInstances;
			go.name = "Camera Data";

			CameraData cameraData = go.AddComponent<CameraData>();
			cameraData.fov = currentCamera.fieldOfView;
			cameraData.transform.position = currentCamera.transform.position;
			cameraData.transform.rotation = currentCamera.transform.rotation;

			currentCameraData = cameraData;

			numInstances++;

			Selection.activeGameObject = go;
		}
	}

	private void ChangeCurrentCameraData()
	{
		if(currentCameraData == null)
		{
			Debug.LogError("Debes asignar un Camera Data en antes de poder cambiarlo!");
		}
		else
		{
			currentCameraData.transform.position = currentCamera.transform.position;
			currentCameraData.transform.rotation = currentCamera.transform.rotation;
			currentCameraData.fov = currentCamera.fieldOfView;

			Selection.activeGameObject = currentCameraData.gameObject;
		}
	}

	private void SetCameraToCurrentCameraData()
	{
		if (currentCamera == null || currentCameraData == null)
		{
			if(currentCameraData == null)
			{
				Debug.LogError("No has asignado ningun CameraData!");
			}

			if (currentCamera)
			{
				Debug.LogError("No has asignado ninguna Camara!");
			}
			
		}
		else
		{
			currentCamera.transform.position = currentCameraData.transform.position;
			currentCamera.transform.rotation = currentCameraData.transform.rotation;
			currentCamera.fieldOfView = currentCameraData.fov;

			Selection.activeGameObject = currentCamera.gameObject;
		}
	}
}
