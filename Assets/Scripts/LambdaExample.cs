using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LambdaExample : MonoBehaviour {
    public event Action<float> TimerExpiredWithParameter;
    public event Action TimerExpired;

    // Use this for initialization
    private void Start ()
    {
        StartCoroutine(Timer());
        StartCoroutine(CallFunctionAfterTimer(2, () => 
        {
            Debug.Log("Other timer expired");
            Debug.Log("This is a lambda with multiple statements!");
            Debug.Log("Microsoft says you shouldn't really do more than two or three statements, though.");
            // Source: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions#statement-lambdas
        }));
	}
    
    private IEnumerator Timer()
    {
        float timerDurationInSeeconds = 2;

        yield return new WaitForSeconds(timerDurationInSeeconds);

        if (TimerExpired != null)
            TimerExpired.Invoke();

        if (TimerExpiredWithParameter != null)
            TimerExpiredWithParameter.Invoke(timerDurationInSeeconds);
    }

    private void OnTimerExpired()
    {
        Debug.Log("Timer expired!");
    }

    private void OnTimerExpiredWithParameter(float x)
    {
        Debug.Log(String.Format("Timer was {0} seconds.", x));
    }

    private void OnEnable()
    {
        TimerExpired += OnTimerExpired;
        TimerExpired += () => Debug.Log("called from anonymous function!");

        TimerExpiredWithParameter += OnTimerExpiredWithParameter;
        TimerExpiredWithParameter += x => Debug.Log(String.Format("Anonymous function with parameter: {0}!", x + 2));
    }

    private void OnDisable()
    {
        TimerExpired -= OnTimerExpired;
        TimerExpiredWithParameter -= OnTimerExpiredWithParameter;
    }

    private IEnumerator CallFunctionAfterTimer(float timerDurationInSeconds, Action functionToCall)
    {
        yield return new WaitForSeconds(timerDurationInSeconds);

        if (functionToCall != null)
            functionToCall.Invoke();
    }
}
