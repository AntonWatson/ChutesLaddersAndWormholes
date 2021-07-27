using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladders : MonoBehaviour
{
    [SerializeField]
    public  GameObject otherLadder;
    [SerializeField]
    public int position;
    public int otherPosition;
    // Start is called before the first frame update
    void Start()
    {
        otherPosition = otherLadder.GetComponent<OtherLadder>().otherPosition;
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
