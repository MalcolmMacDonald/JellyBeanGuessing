using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanSpawnerController : MonoBehaviour
{

    public GameObject beanPrefab;


    public int beanCount;
    public float timeBetweenSpawns;

    public Color[] beanColors;
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(SpawnBeans());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnBeans()
    {
        for (int i = 0; i < beanCount; i++)
        {
            GameObject spawnedBean = Instantiate(beanPrefab, transform.position, Random.rotation, transform);
            spawnedBean.GetComponent<MeshRenderer>().materials[0].color = beanColors[Random.Range(0, beanColors.Length)];
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

    }
}
