
using UnityEngine;

public partial class SpawnerZ : MonoBehaviour
{
    [SerializeField]
    private Move_Cube MCube;
    [SerializeField]
    private MoveDirection moveDirection;
    public void SwapnCube()
    {

        var cube = Instantiate(MCube);

        if (Move_Cube.Last_Cube != null && Move_Cube.Last_Cube.gameObject != GameObject.Find("house"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : Move_Cube.Last_Cube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : Move_Cube.Last_Cube.transform.position.z;
            cube.transform.position = new Vector3(x,
            Move_Cube.Last_Cube.transform.position.y + MCube.transform.localScale.y,
            z);  

        }

        else cube.transform.position = transform.position;

        cube.MoveDirection = moveDirection;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, MCube.transform.localScale);
    }
}