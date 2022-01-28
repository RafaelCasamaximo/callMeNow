using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewManager : MonoBehaviour
{

    public float transitionDuration;

    // Start is called before the first frame update
    void Start()
    {
        transitionDuration = 5f;
    }

    public IEnumerator LerpPosition(Transform lerpedObjectTransform, Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            lerpedObjectTransform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lerpedObjectTransform.position = targetPosition;
    }

    public IEnumerator LerpRotation(Transform lerpedObjectTransform, Quaternion startRotation, Quaternion endRotation, float duration)
    {
        float time = 0;

        while (time < duration)
        {
            lerpedObjectTransform.rotation = Quaternion.Lerp(startRotation, endRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        lerpedObjectTransform.rotation = endRotation;
    }


}
