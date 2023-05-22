using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu( fileName = "New Banner Image", menuName = "Game Objects/Banner Image")]
public class BannerImage : ScriptableObject
{
    public Sprite sprite;
    public int2 aspectRatio;
}
