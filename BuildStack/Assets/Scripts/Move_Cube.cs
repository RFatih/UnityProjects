using UnityEngine;
using UnityEngine.SceneManagement;

public class Move_Cube : MonoBehaviour
{
    public static Move_Cube Current_Cube { get; private set; }
    public static Move_Cube Last_Cube { get; private set; }
    public SpawnerZ.MoveDirection MoveDirection { get; set; }
    public Texture perfect;
    public MeshRenderer box;
    bool dir=true;
    public static int animb = 0;
    [SerializeField]
    private float moveSpeed = 2f;

    private void OnEnable()
    {
        
        if (Last_Cube == null)
            Last_Cube = GameObject.Find("house").GetComponent<Move_Cube>();
        Current_Cube = this;

        GetComponent<Renderer>().material.color = GetRandomColor();
        transform.localScale = new Vector3(Last_Cube.transform.localScale.x, transform.localScale.y, Last_Cube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));

    }
    private Color Blue()
    {
        return new Color(62/255f, 171/255f, 188/255f);
    }
    private Color Red()
    {
        return new Color(1f, 0f,0f);
    }
    private Color White()
    {
        return new Color(255, 255, 255);
    }

    internal void Stop()
    {
        if (gameObject.name == "house") return;
        moveSpeed = 0;

   

        float hango = GetHango();


        if (Mathf.Abs(hango) <= 0.02f)
        {
            hango = 0;
            animb++;
            if(animb<5)
            GetComponent<Renderer>().material.color = Red();
            else
                GetComponent<Renderer>().material.color = Blue();
            box.material.mainTexture = perfect;
        }
        else animb =0;
        float direction = hango > 0 ? 1f : -1f;
        float max = MoveDirection == SpawnerZ.MoveDirection.Z ? Last_Cube.transform.localScale.z : Last_Cube.transform.localScale.x;
        if (Mathf.Abs(hango) >= max||Last_Cube.transform.localScale.z- Mathf.Abs(hango)<0.1f||Last_Cube.transform.localScale.x- Mathf.Abs(hango)<0.1f)
        {
            Last_Cube = null;
            Current_Cube = null;
            SceneManager.LoadScene(0);
        }
        if (MoveDirection == SpawnerZ.MoveDirection.Z)
            SplitCubeOnZ(hango, direction);
        else
            SplitCubeOnX(hango, direction);
        Last_Cube = this;

    }

    private float GetHango()
    {
        if(MoveDirection==SpawnerZ.MoveDirection.Z)
        return transform.position.z - Last_Cube.transform.position.z;
        else
            return transform.position.x - Last_Cube.transform.position.x;
    }

    private void SplitCubeOnX(float hango, float direction)
    {


        float newXsize = Last_Cube.transform.localScale.x - Mathf.Abs(hango);

        float fallingBlockSize = transform.localScale.x - newXsize;
        float newXposition = Last_Cube.transform.position.x + (hango / 2);
        transform.localScale = new Vector3(newXsize, transform.localScale.y, transform.localScale.z);
        transform.position = new Vector3(newXposition, transform.position.y, transform.position.z);

        float cubeEdge = transform.position.x + (newXsize / 2f * direction);
        float fallingBlock = cubeEdge + fallingBlockSize / 2f * direction;
        
            SpawnDropCube(fallingBlockSize, fallingBlock,hango);
    }
    private void SplitCubeOnZ(float hango, float direction)
    {


        float newZsize = Last_Cube.transform.localScale.z - Mathf.Abs(hango);

        float fallingBlockSize = transform.localScale.z - newZsize;
        float newZposition = Last_Cube.transform.position.z + (hango / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZsize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZposition);

        float cubeEdge = transform.position.z + (newZsize / 2f * direction);
        float fallingBlock = cubeEdge + fallingBlockSize / 2f * direction;
        
        SpawnDropCube(fallingBlockSize, fallingBlock,hango);
    }

    private void SpawnDropCube(float fallingBlockSize, float fallingBlock,float hango)
    {
        if (hango != 0)
        {

       
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        if (MoveDirection == SpawnerZ.MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlock);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockSize,transform.localScale.y, transform.localScale.z );
            cube.transform.position = new Vector3(fallingBlock,transform.position.y, transform.position.z );

        }
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;
        Destroy(cube.gameObject, 1f);
        }
        else
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Plane);
            cube.transform.localScale = new Vector3(transform.localScale.x / 8f, transform.localScale.y / 8f, transform.localScale.z /8f);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            cube.GetComponent<Renderer>().material.color = White();
           Destroy(cube.gameObject, 0.1f);
           
    }
}
    // Update is called once per frame
    private void Update()
    {
        
        if (MoveDirection == SpawnerZ.MoveDirection.Z)
        {
            

            if (transform.position.z <= -1.1f)
            {
                dir = true;
               
            }
            else if (transform.position.z >= 1.1f)
            {
                dir = false;
                
            }
           
          
                

            if (dir == true)
                transform.position += transform.forward * Time.deltaTime * moveSpeed;
            else
                transform.position += transform.forward * Time.deltaTime * moveSpeed*-1f;


        }
        else
        {

            if (transform.position.x <= -1.1f) dir = true;

            else if (transform.position.x >= 1.1f) dir = false;
            if (dir == true)
                transform.position += transform.right  * Time.deltaTime * moveSpeed;
            else
                transform.position += transform.right * Time.deltaTime * moveSpeed*-1f;
        }
          
    }
}


