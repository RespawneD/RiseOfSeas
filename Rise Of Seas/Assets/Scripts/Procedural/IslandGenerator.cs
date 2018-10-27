using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class IslandGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, ColourMap, Mesh};
	public DrawMode drawMode;

	const int mapChunkSize = 95;
	[Range(0,6)]
	public int levelOfDetail;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public float meshHeightMultiplier;
	public AnimationCurve meshHeightCurve;

	public bool autoUpdate;
    public bool autoGenerate;


	public TerrainType[] regions;



	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
		for (int y = 0; y < mapChunkSize; y++) {
			for (int x = 0; x < mapChunkSize; x++) {
				float currentHeight = noiseMap [x, y];
				for (int i = 0; i < regions.Length; i++) {
					if (currentHeight <= regions [i].height) {
						colourMap [y * mapChunkSize + x] = regions [i].colour;
						break;
					}
				}
			}
		}

        PropsGenerator propsGenerator = GetComponentInChildren<PropsGenerator>();
        MeshData meshData = MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve, levelOfDetail);
        Mesh mesh = meshData.CreateMesh();
        Texture texture = TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize);



        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer> ().sharedMaterial.mainTexture = texture;
        //GetComponent<MeshRenderer>().transform.localScale = new Vector3(texture.width, 1, texture.height);

        GetComponent<MeshCollider>().sharedMesh = mesh;


        propsGenerator.RemoveAllProps();

        propsGenerator.GenerateProps(mapChunkSize, mapChunkSize, noiseMap, meshHeightMultiplier, meshHeightCurve);

	}

    public void StartAutoGeneration()
    {
        StartCoroutine(AutoGeneration());
    }

    public void StopAutoGeneration()
    {
        StopCoroutine(AutoGeneration());
    }

    IEnumerator AutoGeneration()
    {
        while(autoGenerate)
        {
            offset.x += Random.Range(-5f, 5f);
            offset.y += Random.Range(-5f, 5f);
            GenerateMap();
            yield return new WaitForSeconds(.25f);
        }

        yield return null;
    }

    private void Start()
    {

        GenerateMap();

        if (autoGenerate)
            StartAutoGeneration();
    }
    void OnValidate() {
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}