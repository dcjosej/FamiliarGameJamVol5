using UnityEngine;
using UnityEngine.UI;

public class PanelAction : MonoBehaviour {

	[SerializeField]
	private Text keyActionText;

	void Start()
	{
		if (!keyActionText)
		{
			Debug.LogError("No se encuentra la referencia al texto con la tecla la acción!", this);
		}
	}

	public void Init(string key)
	{
		keyActionText.text = key;
	}
}
