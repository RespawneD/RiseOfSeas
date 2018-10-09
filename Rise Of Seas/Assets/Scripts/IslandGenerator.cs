using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IslandSize
{
    Small = 1,
    Medium = 4,
    Big = 8
}

public class IslandGenerator : MonoBehaviour {





    public GameObject island;
    public List<GameObject> trees;
    public List<GameObject> rocks;
    public List<GameObject> boulders;
    public List<GameObject> envs;

    public List<GameObject> wrecks;

    public float beachBorder = 1f;

    [Range(0f, 1f)]  public float propSpawnInterval = .5f;
    [Range(0f, 1f)]  public float treeDensity;
    [Range(0f, 1f)]  public float rockDensity;
    [Range(0f, 1f)]  public float envDensity;
    [Range(0f, 1f)]  public float boulderDensity;
    [Range(0f, 1f)]  public float wreckChance;
    public IslandSize size;

    GameObject t_island;
    private int scale;

    private float[,] propsArray;

    private List<Vector3> waterSpawns;

    Vector3 accurateSize;
    int width;
    int height;

    [SerializeField]
    private bool debug;

    Vector3 GetIslandScale(IslandSize size)
    {
        scale = (int)Random.Range((float)size, (float)size * 2);
        return Vector3.one * scale;
    }

    void DebugPropsArray()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                GameObject g  = GameObject.CreatePrimitive(PrimitiveType.Cube);
                g.transform.position = new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, propsArray[x, y], transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y);
            }
        }
    }

    void GetPropsArray()
    {
        accurateSize = t_island.GetComponent<MeshFilter>().sharedMesh.bounds.extents * 2 * (scale * beachBorder);

        width = (int)(accurateSize.x / propSpawnInterval);
        height = (int)(accurateSize.z / propSpawnInterval);

        propsArray = new float[width, height];

        for(int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                RaycastHit hit;
                Vector3 rayPos = new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 30, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y);

                if(Physics.Raycast(rayPos, Vector3.down, out hit))
                {
                    if (hit.collider.CompareTag("Island"))
                        propsArray[x, y] = 30 - hit.distance;
                    else
                        propsArray[x, y] = -20;

                    if(hit.collider.CompareTag("Water"))
                    {
                        waterSpawns.Add(new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 30 - hit.distance, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y));
                    }

                }

            }
        }
        

    }


    void SpawnWreck()
    {
        if(Random.Range(0f, 1f) < wreckChance)
        {
            GameObject g = Instantiate(wrecks[Random.Range(0, wrecks.Count - 1)], waterSpawns[Random.Range(0, waterSpawns.Count - 1)] + Vector3.down * Random.Range(-1f, 1f), Quaternion.Euler(0, Random.Range(0, 360), 0), transform);

        }
    }

    void SpawnTrees()
    {


        for(int k = 0; k < width * height * treeDensity/20; k++)
        {
            int x;
            int y;
            RaycastHit hit;
            do
            {
                x = Random.Range(0, width);
                y = Random.Range(0, height);

                Physics.Raycast(new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 50, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Vector3.down, out hit);
            } while (propsArray[x, y] < 0 && !hit.collider.CompareTag("Island"));

            GameObject g = Instantiate(trees[Random.Range(0, trees.Count - 1)], new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, propsArray[x, y], transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Quaternion.Euler(0, Random.Range(0f, 360f), 0), transform);
            g.transform.localScale *= Random.Range(.4f, 1.2f);
            propsArray[x, y] = -20;





        }
    }


    void SpawnRocks()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0f, 1f) < rockDensity && propsArray[x, y] >= 0)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 50, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Vector3.down, out hit))
                    {
                        if (!hit.collider.CompareTag("Island"))
                            continue;
                    }

                    GameObject g = Instantiate(rocks[Random.Range(0, rocks.Count - 1)], new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, propsArray[x, y] - .7f, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Quaternion.Euler(0, Random.Range(0f, 360f), 0), transform);
                    g.transform.localScale *= Random.Range(.7f, 1.2f);
                    propsArray[x, y] = -20;
                }


            }
        }
    }

    void SpawnBoulders()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0f, 1f) < boulderDensity * propSpawnInterval && propsArray[x, y] >= 0)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 50, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Vector3.down, out hit))
                    {
                        if (!hit.collider.CompareTag("Island"))
                            continue;
                    }

                    GameObject g = Instantiate(boulders[Random.Range(0, boulders.Count - 1)], new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, propsArray[x, y] - .7f, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Quaternion.Euler(0, Random.Range(0f, 360f), 0), transform);
                    g.transform.localScale *= Random.Range(.4f, 1.2f);
                    propsArray[x, y] = -20;
                }


            }
        }
    }

    void SpawnEnvs()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Random.Range(0f, 1f) < envDensity * propSpawnInterval && propsArray[x, y] >= 0)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, 50, transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Vector3.down, out hit))
                    {
                        if (!hit.collider.CompareTag("Island"))
                            continue;
                    }

                    GameObject g = Instantiate(envs[Random.Range(0, envs.Count - 1)], new Vector3(transform.position.x + -accurateSize.x / 2 + propSpawnInterval * x, propsArray[x, y], transform.position.z + -accurateSize.z / 2 + propSpawnInterval * y), Quaternion.Euler(0, Random.Range(0f, 360f), 0), transform);
                    g.transform.localScale *= Random.Range(.5f, 1.2f);
                    propsArray[x, y] = -20;
                }


            }
        }
    }

    public void Generate()
    {
        waterSpawns = new List<Vector3>();
        t_island = Instantiate(island, transform);
        t_island.tag = "Island";
        t_island.transform.localScale = GetIslandScale(size);
        t_island.transform.position += Vector3.down * t_island.transform.localScale.y / 3;
        GetPropsArray();
        SpawnWreck();
        SpawnBoulders();
        SpawnRocks();
        SpawnTrees();
        SpawnEnvs();
    }

    private void Start()
    {
        if (debug)
            Generate();
    }
}
