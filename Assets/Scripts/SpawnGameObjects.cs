using UnityEngine;
using System.Collections;

public class SpawnGameObjects : MonoBehaviour {

	public GameObject[] spawnPrefab;

	private int spawnPrefabLength;

	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	public Transform chaseTarget;
	
	private float savedTime;
	private float secondsBetweenSpawning;

	private GameController gc;

	// Use this for initialization
	void Start () {
		gc = GameObject.FindWithTag("GameController").GetComponent<GameController>();

		spawnPrefabLength = spawnPrefab.Length;

		savedTime = Time.time;
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - savedTime >= secondsBetweenSpawning) // is it time to spawn again?
		{
			MakeThingToSpawn();
			savedTime = Time.time; // store for next spawn
			secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
		}	
	}

	void MakeThingToSpawn()
	{
		int from = 0, to = 25; // max range

		setRange(ref from, ref to); // change range according to current alphabet

        //randomize objeect to be created
        int index = Random.Range(from, to);

		// create a new gameObject
		GameObject clone = Instantiate(spawnPrefab[index], transform.position, transform.rotation) as GameObject;

		// set chaseTarget if specified
		if ((chaseTarget != null) && (clone.gameObject.GetComponent<Chaser> () != null))
		{
			clone.gameObject.GetComponent<Chaser>().SetTarget(chaseTarget);
		}
	}

	private void setRange(ref int from, ref int to){
		char alphabet = char.Parse( gc.getCurrentAlpha() ); // get first alphabet in current word

		int alpha_ascii = alphabet;

		int alpha_index = alpha_ascii % 97; // get the index of alphabet ranging from 0-25. alphabet IS lower case

		// 5 alphabets before and after
		from = alpha_index - 5;
		to = alpha_index + 5;

		if(from < 0){
			from = 0;
		}

		if(to > 25){
			to = 25;
		}

	}

    public string GetGameObjectName(int index)
    {
        return spawnPrefab[index].name;
    }
}
