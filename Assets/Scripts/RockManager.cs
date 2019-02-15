using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour 
{
  [SerializeField]
  private GameObject[] rocks; 

  [SerializeField]
  private int number;

	// Use this for initialization
	void Start () 
  {
		for (int i = 0; i < number; i++)
    {
      float fieldSize = 1000.0f;
      Vector3 pos = new Vector3 ( Random.Range (-fieldSize, fieldSize),
                                  Random.Range (-fieldSize, fieldSize),
                                  Random.Range (-fieldSize, fieldSize));
      int rnd = Random.Range (0, rocks.Length);

      Instantiate (rocks[rnd], pos, Quaternion.identity);
    }
	}
	
	// Update is called once per frame
	void Update () 
  {
		
	}
}
