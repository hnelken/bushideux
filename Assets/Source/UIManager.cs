using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public SpriteRenderer player1;
    public SpriteRenderer player2;

    private Vector2 player1Start;
    private Vector2 player2Start;

    void Awake() {
        player1Start = player1.transform.position;
        player2Start = player2.transform.position;
    }

    // Start is called before the first frame update
    void Start() {
        SetOpeningStateUI();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void SetOpeningStateUI() {
        player1.transform.position = player1Start;
        player2.transform.position = player2Start;
    }

    public void SetReadyStateUI() {
        
    }

    public void SetWaitStateUI() {
        
    }

    public void SetReactStateUI() {

    }

    public void SetDecisionStateUI() {
        player1.transform.position = player2Start;
        player2.transform.position = player1Start;
    }

    public void SetFinishStateUI() {
        
    }
}
