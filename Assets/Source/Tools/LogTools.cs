using UnityEngine;
using UnityEngine.Assertions;

public class LogTools : MonoBehaviour
{
    private static string msgPrefix = "BUSHI - ";

    public static void AssertionFailure(string message) {
        print(msgPrefix + message);
        Assert.IsTrue(false);
    }

    public static void log(string message) {
        print(msgPrefix + message);
    }
}
