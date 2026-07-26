using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DistortionController : MonoBehaviour
{
    public AudioDistortionFilter distortion;
    public float maxDistortion;

    // Update is called once per frame
    void Update()
    {
        distortion.distortionLevel = GameManager.Instance.GetDifficultyValue() * maxDistortion;
    }
}
