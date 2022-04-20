using UnityEngine;

public delegate void OnSignalFired();

public class SignalManager : MonoBehaviour
{
    public GameObject signalParent;

    private float timer = 0;
    private float timerLength;
    private bool isTimerRunning = false;

    private OnSignalFired callBack;
    private SignalDot[] dots;

    // MARK: - Lifecycle

    void Awake() {
        dots = signalParent.GetComponentsInChildren<SignalDot>();
    }

    void Start() {
        ResetForNewRound();
    }

    void Update() {
        if (!isTimerRunning || callBack == null) { return; }
        timer += Time.deltaTime;

        if (timer >= timerLength) {
            SignalFired();
        }
    }
    
    // MARK: - Public API

    public void ResetForNewRound() {
        SetSignalIsVisible(false);
        SetSignalIsActive(false);
        isTimerRunning = false;
        callBack = null;
        timer = 0;
    }

    public void StartSignalTimer(float timerLength, OnSignalFired callBack) {
        this.callBack = callBack;
        this.timerLength = timerLength;

        SetSignalIsActive(false);
        SetSignalIsVisible(true);
        isTimerRunning = true;
    }

    // MARK: - Private API

    private void SignalFired() {
        isTimerRunning = false;

        var action = callBack;
        callBack = null;
            
        SetSignalIsActive(true);
        action.Invoke();
    }

    private void SetSignalIsVisible(bool isVisible) {
        foreach (SignalDot dot in dots) {
            dot.SetSignalIsVisible(isVisible);
        }
    }

    private void SetSignalIsActive(bool isActive) {
        foreach (SignalDot dot in dots) {
            dot.SetSignalIsActive(isActive);
        }
    }
}
