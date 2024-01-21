using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    private KlazTweenManager tweenManager;

    public GameObject obj;
    public GameObject obj2;
    void Start()
    {
        tweenManager = FindObjectOfType<KlazTweenManager>();

        KlazTween tween = new KlazTween(1f, 2f, 2f, UpdateScale, OnTweenStart, OnTweenComplete);
        KlazTween tween2 = new KlazTween(1f, 2f, 1f, UpdateScale2, OnTweenStart2, OnTweenComplete2);
        tweenManager.AddTween(tween);
        tweenManager.AddTween(tween2);
    }

    void UpdateScale2(float value)
    {
        // Update the scale of an object
        obj2.transform.localScale = Vector3.one * value;
    }

    void OnTweenStart2()
    {
        Debug.Log("Tween2 started!");
    }

    void OnTweenComplete2()
    {
        Debug.Log("Tween2 completed!");
    }
    
    void UpdateScale(float value)
    {
        // Update the scale of an object
        obj.transform.localScale = Vector3.one * value;
    }

    void OnTweenStart()
    {
        Debug.Log("Tween started!");
    }

    void OnTweenComplete()
    {
        Debug.Log("Tween completed!");
    }
}
