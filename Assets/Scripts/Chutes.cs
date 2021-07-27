using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chutes : MonoBehaviour
{

    [SerializeField]
    public GameObject otherChute;
    public int position = 0;
    public int otherPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        otherPosition = otherChute.GetComponent<OtherChutes>().otherPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
