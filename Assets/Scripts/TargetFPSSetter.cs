using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFPSSetter : MonoBehaviour
{
    [SerializeField]
    private int _tagretFPS = 60;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = _tagretFPS;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
