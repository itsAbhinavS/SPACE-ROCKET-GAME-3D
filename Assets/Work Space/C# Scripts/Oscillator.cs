using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 startingPos; // set in inspector
    public Vector3 endingPos; // set in inspector
    public float moveSpeed = 2f; // adjust as needed
    private float t = 0f;

    void Update()
    {
        // Interpolate the position of the object between startingPos and endingPos
        t += moveSpeed * Time.deltaTime;
        transform.position = Vector3.Lerp(startingPos, endingPos, t);

        // If the object has reached the ending position, reset the interpolation variables
        if (t >= 1f)
        {
            t = 0f;
            Vector3 temp = startingPos;
            startingPos = endingPos;
            endingPos = temp;
        }
    }

}
