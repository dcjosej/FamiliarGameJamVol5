using UnityEngine;

public class InteractiveFurniture : MonoBehaviour {

	private Drawer[] drawers;

	private Drawer currentOpenedDrawer;
	private int indexDrawer = 0;


	[Header("Content")]
	public GameObject pillPrefab;
	public int numPills;
	public GameObject cartridgePrefab;
	public int numCartridges;
	public GameObject[] dummyObjectsArrayPrefabs;
	public int numDummyObjects;

	void Start()
	{
		drawers = GetComponentsInChildren<Drawer>();

		PlaceRandomMainObjects();

	}

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

			/*FOR TEXT*/
			//randomIndex = 0;
			/**/

            drawers[randomIndex].PlaceRandomObject(pillPrefab);
		}


		//Colocamos los cartuchos
		for (int i = 0; i < numCartridges; i++)
		{
			randomIndex = Random.Range(0, drawers.Length);


			
			Drawer selectedDrawer = drawers[randomIndex];
			selectedDrawer.PlaceRandomObject(cartridgePrefab);
		}

		//Colocamos los objetos dummies
		for (int i = 0; i < numDummyObjects; i++)
		{
			randomIndex = Random.Range(0, drawers.Length);



			Drawer selectedDrawer = drawers[randomIndex];
			selectedDrawer.PlaceRandomObject(dummyObjectsArrayPrefabs[Random.Range(0, dummyObjectsArrayPrefabs.Length)]);
		}
	}
}