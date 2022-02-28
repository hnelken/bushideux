using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(InputManager))]
public class RoundManager : MonoBehaviour
{
    // MARK: - Editor Properties
    public Text reactTimerText;
    public SignalManager signalManager;

    // MARK: - Private Properties
    private InputManager inputManager;

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
            float signalTimer = 3;
            StartSignalTimer(signalTimer);
        } else if (roundState == RoundState.REACT) {
            // TODO: Finish recording reaction time here
            MoveToNextState();
        } else if (roundState == RoundState.FINISH) {
            ResetForNewRound();
        }
    }

    // MARK: - Lifecycle

    void Awake() {
        inputManager = GetComponent<InputManager>();
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
        signalManager.ResetForNewRound();
        roundState = RoundState.OPEN;
        reactTimerText.enabled = false;
        reactTimer = 0;
    }
    
    private void StartSignalTimer(float signalTimer) {
        if (roundState != RoundState.READY) {
            LogTools.AssertionFailure("Attempted to start round from invalid state");
        }
    
        signalManager.StartSignalTimer(signalTimer, new OnSignalFired(SignalFired));
        MoveToNextState();
    }

    private void SignalFired() {
        if (roundState != RoundState.WAIT) {
            LogTools.AssertionFailure("Attempted to fire signal from invalid state");
        }
        // TODO: Set reaction period start time here
        MoveToNextState();
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
                // TODO: Move characters to result positions
                MoveToNextState();
                break;
            case RoundState.FINISH:
                break;
        }
    }
}
