using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Difficulty", menuName = "Game Presets/Difficulty")]
public class Difficulty : ScriptableObject
{
    public AnimationCurve maxComboRange;
    public AnimationCurve minComboRange;

    public AnimationCurve loadingBarChance;
    public AnimationCurve maxLoadingBarTimeRange;
    public AnimationCurve minLoadingBarTimeRange;

    public AnimationCurve maxBannerSpeedSpawn;
    public AnimationCurve minBannerSpeedSpawn;
    public AnimationCurve maxBannerCountSpawn;
    public AnimationCurve minBannerCountSpawn;
}
