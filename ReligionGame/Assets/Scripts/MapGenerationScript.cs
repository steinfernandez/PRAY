using UnityEngine;
using System.Collections;

public class MapGenerationScript : MonoBehaviour {

    private int i, j;
    [SerializeField]
    GameObject mapCube;
    [SerializeField]
    GameObject gridStart;

    [SerializeField]
    private int mapLength;
    [SerializeField]
    private int mapWidth;

    public GameObject[] mapGrid; 

	// Use this for initialization
	void Start ()
    {
        mapGrid = new GameObject[10000];
        GenerateMap();
        //RoughenEdges();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void GenerateMap()
    {
        for(i=0;i<mapLength;i++)
        {
            for(j=0;j<mapWidth;j++)
            {
                GameObject tempcube = (GameObject)Instantiate(mapCube, gridStart.transform.position+new Vector3(j*mapCube.transform.localScale.x,-1*i*mapCube.transform.localScale.y,0), gridStart.transform.rotation);
                mapGrid[i * mapWidth + j] = tempcube;
            }
        }
    }

    void RoughenEdges()
    {
        for(i=0;i<mapLength;i++)
        {
            int rem1 = Random.Range(0, 2);
            int rem2 = Random.Range(0, 2);
            for(j=0; j<rem1; j++)
            {
                mapGrid[i * mapWidth + j].gameObject.SetActive(false);
            }
            for(j=mapWidth-1; j>mapWidth - rem2 -1; j--)
            {
                mapGrid[i * mapWidth + j].gameObject.SetActive(false);
            }
        }
    }

    void SpawnCities()
    {
        for(i=0;i<(mapLength*mapWidth);i++)
        {
        }
    }
}
