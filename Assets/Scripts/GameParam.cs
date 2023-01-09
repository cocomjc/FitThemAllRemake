using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameParam")]
public class GameParam : ScriptableObject
{
    public Vector3 blockSmallScale;
    public Vector3 blockNormalScale;
    public Vector2 piecesSize;
}
