using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DeathPPE : MonoBehaviour
{
    public static DeathPPE _instance;

    [SerializeField, Range(0f, 1f)]
    private float value; // Intensity of effecs. Higher values equal higher intensity of effects. 0 = no effect, 1 = max effect

    public Volume _volume;

    private MotionBlur _motionBlur;
    private Vignette _vignette;
    private FilmGrain _grain;
    private ChromaticAberration _chromaticAberration;
    private ColorAdjustments _colorAdjustements;

    private float maxIntensity_motionBlur = 1f;
    private float maxIntensity_vignette = 0.45f;
    private float maxIntensity_grain = 0.75f;
    private float maxIntensity_chromaticAberration = 0.85f;
    private float maxIntensity_ColorAdjustments_Saturation = -40f;

    [Space]

    [Range(0f, 1f)]
    public float audioThreshhold = 0.5f; // Threshold at which the audio ambiance starts to play. Value between 0 and 1
    public AudioSource audioAmbiance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        _motionBlur = _volume.profile.TryGet<MotionBlur>(out var motionBlur) ? motionBlur : null;
        _vignette = _volume.profile.TryGet<Vignette>(out var vignette) ? vignette : null;
        _grain = _volume.profile.TryGet<FilmGrain>(out var grain) ? grain : null;
        _chromaticAberration = _volume.profile.TryGet<ChromaticAberration>(out var chromaticAberration) ? chromaticAberration : null;
        _colorAdjustements = _volume.profile.TryGet<ColorAdjustments>(out var colorAdjustments) ? colorAdjustments : null;
    }

    void Update()
    {
        UpdateEffects();
    }

    public void SetValue(float value) => value = Mathf.Clamp01(value);

    void UpdateEffects()
    {
        if (audioAmbiance)
        {
            if (value > audioThreshhold)
                audioAmbiance.volume = (value - audioThreshhold) / (1f - audioThreshhold);
            else
                audioAmbiance.volume = 0f;
        }

        if( _motionBlur)
            _motionBlur.intensity.value = value * maxIntensity_motionBlur;

        if (_vignette)
            _vignette.intensity.value = value * maxIntensity_vignette;

        if(_grain)
            _grain.intensity.value = value * maxIntensity_grain;

        if( _chromaticAberration)
            _chromaticAberration.intensity.value = value * maxIntensity_chromaticAberration;

        if(_colorAdjustements)
            _colorAdjustements.saturation.value = value * maxIntensity_ColorAdjustments_Saturation;
    }
}
