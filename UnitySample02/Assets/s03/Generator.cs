using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MakeZako");


    }
    IEnumerator MakeZako(){

        for (int i = 0; i < 8; i++) {
            var obj = GameObject.Instantiate(prefab);
            obj.GetComponent<Bane>().target = target.transform;

            yield return new WaitForSeconds(0.2f);

        }

        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
