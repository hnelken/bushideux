using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType {
    SPACEBAR
}

public class InputManager : MonoBehaviour
{

    private List<SpacebarAction> spacebarActions = new List<SpacebarAction>();

    public void subscribeTo(InputType inputType, SpacebarAction action) {
        switch (inputType) {
            case InputType.SPACEBAR:
                spacebarActions.Add(action);
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            InvokeSpacebarSubscriptions();
        }
    }

    private void InvokeSpacebarSubscriptions() {
        foreach(SpacebarAction action in spacebarActions) {
            action.Invoke();
        }
    }
}

// MARK: - Subscription Types

public delegate void SpacebarAction();
