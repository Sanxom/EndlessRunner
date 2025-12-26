using UnityEngine;

public class LevelPart : MonoBehaviour
{
    [field: SerializeField] public Transform StartPoint { get; private set; }
    [field: SerializeField] public Transform EndPoint { get; private set; }
}