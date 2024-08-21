using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private Material _explosionMaterial;
    [SerializeField]
    private float _explosionDuration;
    private float elapsedTime = 0;

    private void Update()
    {
        if (elapsedTime > _explosionDuration)
        {
            gameObject.SetActive(false);
            return;
        }
        elapsedTime += Time.deltaTime;
        float spreading = math.lerp(0, 1, elapsedTime / _explosionDuration);
        _explosionMaterial.SetFloat("_Spreading", spreading);
    }
}
