using UnityEngine;
using System.Collections;

public class InteractiveFurniture : MonoBehaviour {

	public Drawer[] drawers;

	private Drawer currentOpenedDrawer;
	private int indexDrawer = 0;


	[Header("Content")]
	public GameObject pillPrefab;
	public int numPills;
	public GameObject cartridgePrefab;
	public int numCartridges;
	public GameObject[] dummyObjectsArrayPrefabs;
	public int numDummyObjects;



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



	private void PlaceRandomMainObjects()
	{
		int randomIndex = -1;

		//Colocamos las pildoras
		for(int i = 0; i < numPills; i++)
		{
			randomIndex = Random.Range(0, drawers.Length);
            drawers[randomIndex].PlaceRandomObject(pillPrefab);
		}


		//Colocamos los cartuchos
		for (int i = 0; i < numCartridges; i++)
		{
			randomIndex = Random.Range(0, drawers.Length);
			Drawer selectedDrawer = drawers[randomIndex];



		}
	}
}