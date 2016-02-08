using UnityEngine;
using System.Collections;

public class InteractiveFurniture : MonoBehaviour {

	public Drawer[] drawers;

	private Drawer currentOpenedDrawer;
	private int indexDrawer = 0;

	void Update () {
		CheckKeyboardInput();
	}

	private void CheckKeyboardInput()
	{
		if (Input.GetKeyDown(KeyCode.F5))
		{
			
			if (currentOpenedDrawer == null)
			{
				drawers[indexDrawer].OpenDrawer();
			}
			else
			{
				currentOpenedDrawer.CloseDrawer();
				indexDrawer++;
				if(indexDrawer >= drawers.Length)
				{
					indexDrawer = 0;
				}
				drawers[indexDrawer].OpenDrawer();
				
			}

			currentOpenedDrawer = drawers[indexDrawer];

		}
	}

}
