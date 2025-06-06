using UnityEngine;

[CreateAssetMenu]
public class CommonMapDatas : ScriptableObject
{
    [SerializeField] private Vector2 limitMin;
    [SerializeField] private Vector2 limitMax;

    public Vector2  LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
