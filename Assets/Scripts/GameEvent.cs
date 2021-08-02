using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{

    public enum Type {
    startTurn,
    playCard,
    diceRoll,
    endTurn
    }
    
    public Type eventType;
    public int playerIndex;
    public int diceRoll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
