
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [SerializeField] int islandCount;

    [SerializeField] int widthArea;

    [SerializeField] int heightArea;

    [SerializeField] GameObject proceduralIsland;

    // Use this for initialization
	void Start () {
        for (int k = 0; k < islandCount; k++)
        {
            GameObject g = Instantiate(proceduralIsland, new Vector3(Random.Range(-widthArea, widthArea), 0, Random.Range(-heightArea, heightArea)), Quaternion.Euler(0, Random.Range(0f, 360f), 0), transform);
            IslandGenerator ig = g.GetComponent<IslandGenerator>();

            ig.size = System.Enum.GetValues(typeof(IslandSize)).Cast<IslandSize>().ToList()[Random.Range(0, 2)];
            ig.treeDensity = Random.Range(0.02f, 0.4f);
            ig.rockDensity = Random.Range(0.01f, 0.03f);
            ig.Generate();
        }
	}
	
}
