using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsGenerator : MonoBehaviour {

    [System.Serializable]
    public class Props
    {
        public Vector2 heightSpawn;
        public Vector2 scaleSpawn;
        public List<GameObject> objs;
        public float intensity;

        public string groupName;
        public bool followNormal;
        public float downOffset;
    }


    public List<Props> props;


	public void GenerateProps(float width, float height, float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve)
    {
        foreach(Props p in props)
        {
            GameObject g = new GameObject(p.groupName);
            Transform t = g.transform;
            t.SetParent(transform);
            Debug.Log(p.groupName);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {

                    if (heightMap[x, y] > p.heightSpawn.x && heightMap[x, y] < p.heightSpawn.y)
                        if (Random.Range(0f, 1f) < p.intensity)
                        {
                            Vector3 position = new Vector3(x - width / 2, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, height / 2 - y);
                            Vector3 rotation = Vector3.zero;
                            if(p.followNormal)
                            {
                                RaycastHit hit;
                                if(Physics.Raycast(position + Vector3.up * 200, Vector3.down, out hit))
                                {
                                    rotation = Quaternion.FromToRotation(Vector3.up, hit.normal).eulerAngles;
                                }
                                
                            }
                            else
                            {
                                rotation = Vector3.up * Random.Range(0f, 360f);
                            }
                            
                            Transform prop =Instantiate(p.objs[Random.Range(0, p.objs.Count - 1)], position, Quaternion.Euler(rotation), t).transform;
                            prop.position -= prop.up * p.downOffset;
                            prop.localScale = Vector3.one * Random.Range(p.scaleSpawn.x, p.scaleSpawn.y);
                        }

                }
            }
        }
    }


    public void RemoveAllProps()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            if(Application.isEditor)
                DestroyImmediate(transform.GetChild(i).gameObject);
            else
                Destroy(transform.GetChild(i).gameObject);
        }
    }
}
