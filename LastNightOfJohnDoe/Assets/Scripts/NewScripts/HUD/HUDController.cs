using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour {


	public static HUDController instance { get; set; }


	[SerializeField]
	private GameObject pnActions;	//Panel que tiene como hijos a PnLook y PnOpen
	[SerializeField]
	private GameObject pnLook;
	[SerializeField]
	private GameObject pnOpen;


	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
	
	}
	
	void Update () {
	
	}


	public void EnablePnLook(KeyCode keyCode)
	{
		pnLook.SetActive(true);
		pnLook.GetComponent<PanelAction>().Init(keyCode.ToString());
	}

	public void DisablePnLook()
	{
		pnLook.SetActive(false);
	}

	public void EnablePnOpen(KeyCode keyCode)
	{
		pnOpen.SetActive(true);
		pnOpen.GetComponent<PanelAction>().Init(keyCode.ToString());
	}

	public void DisablePnOpen()
	{
		pnOpen.SetActive(false);
	}


	/// <summary>
	/// Desactiva el panel contenedor de las acciones.
	/// </summary>
	public void DisablePnActions()
	{
		pnActions.SetActive(false);
	}

	/// <summary>
	/// Activa el panel contenedor de las acciones.
	/// </summary>
	public void EnablePnActions()
	{
		pnActions.SetActive(true);
	}

}
