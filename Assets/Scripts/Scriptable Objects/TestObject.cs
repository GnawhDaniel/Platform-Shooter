using UnityEngine;

[CreateAssetMenu(fileName = "PlayerObject", menuName = "Scriptable Objects/PlayerObject")]
public class PlayerObject : ScriptableObject
{
    [SerializeField] private string mystr;
}
