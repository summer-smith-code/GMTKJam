using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBob : MonoBehaviour
{
    public float effectIntensityY;
    public float effectIntensityX;
    public float effectSpeed;

    BobPositionFollower followerInstance;
    Vector3 originalOffet;
    float sinTime;

    void Start()
    {
        followerInstance = GetComponent<BobPositionFollower>();
    }

    void Update()
    {
        Vector2 rawMovement = Player._instance._input.actions["Move"].ReadValue<Vector2>();

        if(Player._instance.isLocked)
            rawMovement = Vector2.zero;

        Vector3 inputVector = new Vector3(rawMovement.x, 0f, rawMovement.y);

        float sinAmountY = 0f;

        Vector3 sinAmountX = Vector3.zero;

        if (inputVector.magnitude > 0.5f)
            sinTime += Time.deltaTime * effectSpeed;
        else
            sinTime = 0f;

        sinAmountY = -Mathf.Abs(effectIntensityY * Mathf.Sin(sinTime));
        sinAmountX = followerInstance.transform.right * effectIntensityY * Mathf.Cos(sinTime) * effectIntensityX;

        followerInstance.offset = new Vector3
        {
            x = originalOffet.x,
            y = originalOffet.y + sinAmountY,
            z = originalOffet.z
        };

        followerInstance.offset += sinAmountX;
    }
}
