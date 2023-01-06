using UnityEngine;
using Pathfinding;

public class BuildOnPlatform : MonoBehaviour
{
    //May need to change this to be a list of prefabs for various wall configurations
    [SerializeField]
    GameObject _wallPrefab;

    private GameObject _wall;
    //private GameObject _building;
    public void BuildWall()
    {
        // Only build a wall if there isn't one already.
        if (_wall == null)
        {
            _wall = Instantiate(_wallPrefab, transform.position, transform.rotation, gameObject.transform);
        }
        FindObjectOfType<AstarPath>().Scan();
    }
    //void BuildBuilding()
    //{
        
    //}
}
