using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator4 : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject target;
    void Start()
    {
        StartCoroutine("MakeZako");


    }
    IEnumerator MakeZako()
    {

        for (int i = 0; i < 8*2; i++) {
            var obj = GameObject.Instantiate(prefab);
            //  obj.GetComponent<Bane>().target = target.transform;

            //   yield return new WaitForSeconds(Time.deltaTime*10f);
            yield return new WaitForSeconds(0.1f);

        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
