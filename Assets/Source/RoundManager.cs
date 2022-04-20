using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SignalManager))]
public class RoundManager : MonoBehaviour
{
    // MARK: - Editor Properties
    public Text reactTimerText;

    // MARK: - Private Properties
    private SignalManager signalManager;
    private InputManager inputManager;
    private UIManager uiManager;

    private RoundState roundState = RoundState.OPEN;

    private float _reactTimer = 0;
    private float reactTimer {
        get { return _reactTimer; }
        set {
            _reactTimer = value;
            if (reactTimerText != null) {
                reactTimerText.text = "" + (value * 100);
            }
        }
    }

    // MARK: - Public API

    // MARK: - Input

    private void SpacebarPressed() {
        if (roundState == RoundState.READY) {
            StartRandomSignalTimer();
        } else if (roundState == RoundState.REACT) {
            // TODO: Finish recording reaction time here
            MoveToNextState();
        } else if (roundState == RoundState.FINISH) {
            ResetForNewRound();
        }
    }

    // MARK: - Lifecycle

    void Awake() {
        signalManager = GetComponent<SignalManager>();
        inputManager = GetComponent<InputManager>();
        uiManager = GetComponent<UIManager>();
    }

    void Start() {
        inputManager.subscribeTo(InputType.SPACEBAR, new SpacebarAction(SpacebarPressed));
        reactTimerText.enabled = false;
    }

    void Update()
    {
        switch(roundState) {
            case RoundState.OPEN:
                handleOPENStateUpdate();
                break;
            case RoundState.READY:
                break;
            case RoundState.WAIT:
                break;
            case RoundState.REACT:
                handleREACTStateUpdate();
                break;
            case RoundState.DECIDE:
                break;
            case RoundState.FINISH:
                break;
        }
    }

    private void handleOPENStateUpdate() {
        // TODO: Manage opening animations
        MoveToNextState();
    }

    private void handleREACTStateUpdate() {
        reactTimer += Time.deltaTime;
    }

    // MARK: - State Management

    private enum RoundState {
        OPEN, READY, WAIT, REACT, DECIDE, FINISH
    }

    private void ResetForNewRound() {
        uiManager.SetOpeningStateUI();
        signalManager.ResetForNewRound();
        roundState = RoundState.OPEN;
        reactTimerText.enabled = false;
        reactTimer = 0;
    }

    private void MoveToNextState() {
        switch(roundState) {
            case RoundState.OPEN:
                roundState = RoundState.READY;
                break;
            case RoundState.READY:
                roundState = RoundState.WAIT;
                break;
            case RoundState.WAIT:
                roundState = RoundState.REACT;
                break;
            case RoundState.REACT:
                roundState = RoundState.DECIDE;
                break;
            case RoundState.DECIDE:
                roundState = RoundState.FINISH;
                break;
            case RoundState.FINISH:
                break;
        }

        OnStateChanged(roundState);
    }

    private void OnStateChanged(RoundState newState) {
        print("State: " + newState);
        switch(newState) {
            case RoundState.OPEN:
                break;
            case RoundState.READY:
                break;
            case RoundState.WAIT:
                break;
            case RoundState.REACT:
                reactTimerText.enabled = true;
                break;
            case RoundState.DECIDE:
                uiManager.SetDecisionStateUI();
                MoveToNextState();
                break;
            case RoundState.FINISH:
                break;
        }
    }

    // MARK: - Signal Logic
    
    public void StartRandomSignalTimer() {
        var randomTime = Random.Range(2, 5);
        signalManager.StartSignalTimer(randomTime, new OnSignalFired(SignalFired));
        MoveToNextState();
    }

    private void SignalFired() {
        // TODO: Set reaction period start time here
        MoveToNextState();
    }
}
