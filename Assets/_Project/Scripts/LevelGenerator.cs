using UnityEngine;
using UnityEngine.InputSystem;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private LevelPart[] _levelPartArray;
    [SerializeField] private Vector3 _nextPartPosition;
    [SerializeField] private float _distanceToSpawn;
    [SerializeField] private float _distanceToDelete;
    [SerializeField] private Transform _player;

    private void Update()
    {
        DeleteLevelPart();
        GenerateLevelPart();
    }

    private void GenerateLevelPart()
    {
        while (Vector2.Distance(_player.position, _nextPartPosition) < _distanceToSpawn)
        {
            LevelPart part = _levelPartArray[Random.Range(0, _levelPartArray.Length)];

            Vector2 newPosition = new(_nextPartPosition.x - part.StartPoint.position.x, 0f);

            LevelPart newPart = Instantiate(part, newPosition, Quaternion.identity, transform);
            _nextPartPosition = newPart.EndPoint.position;
        }
    }

    private void DeleteLevelPart()
    {
        if (transform.childCount > 0)
        {
            Transform partToDelete = transform.GetChild(0);

            if (Vector2.Distance(_player.position, partToDelete.position) > _distanceToDelete)
            {
                Destroy(partToDelete.gameObject);
            }
        }
    }
}