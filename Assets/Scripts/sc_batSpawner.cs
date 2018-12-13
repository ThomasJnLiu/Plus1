using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sc_batSpawner : MonoBehaviour
{

    public GameObject bat2;

    public GameObject target;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("SpawnBat");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnBat()
    {
        Instantiate(bat2, transform.position, transform.rotation);
        //bat2.GetComponent<sc_batControllerFollow2>().getTarget(target);
        yield return new WaitForSeconds(2f);
    }
}
