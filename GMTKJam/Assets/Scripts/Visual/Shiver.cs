using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiver : MonoBehaviour
{
    public Vector3 defaultPos;
    public Transform pivot;

    public float _multiplier = 1f;

    public float xIntensity = 0.1f;
    public float yIntensity = 0.1f;
    public float zIntensity = 0.1f;

    public float speedMultiplier = 1f;
    public float speed = 0.1f;

    Vector3 lerpPos;

    private void Update()
    {
        SelectLocation();
        Lerp();
    }

    public void Lerp()
    {
        pivot.transform.localPosition = Vector3.Lerp(pivot.transform.localPosition, lerpPos, speed * Time.deltaTime * speedMultiplier);
    }

    public void SetMultiplier(float multiplier) => _multiplier = multiplier;

    public void SelectLocation()
    {
        System.Random r = new System.Random();

        Vector3 newLoc = new Vector3
        (
            (defaultPos.x + r.Next(-1, 2)) * xIntensity * _multiplier,
            (defaultPos.y + r.Next(-1, 2)) * yIntensity * _multiplier,
            (defaultPos.z + r.Next(-1, 2)) * zIntensity * _multiplier
        );

        lerpPos = newLoc;
    }
}
