
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Game_Menager : MonoBehaviour
{

    private SpawnerZ[] spawners;
    private int spawnerIndex = 0;
    private SpawnerZ currentSpawner=null;
    public Text score;
    public static int sco = 0;
    public Camera main;
    private Vector3 pos;
    [SerializeField]
    private Animator anim,anime,animet;
    
  
    private void Awake()
    {
        spawners = FindObjectsOfType<SpawnerZ>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {

            if (Move_Cube.Current_Cube != null) Move_Cube.Current_Cube.Stop();

            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];
            currentSpawner.SwapnCube();
            sco++;
            
            score.text = sco.ToString();
            pos=new Vector3 (main.transform.position.x,main.transform.position.y+0.1f,main.transform.position.z);
           
            main.transform.position =pos;
            switch(Move_Cube.animb)
            {
                case 1:
                    
                        anim.SetBool("bool", true);
                        sco += Move_Cube.animb;
                        score.text = sco.ToString();

                    break;
                case 5: anime.SetBool("new", true);
                        sco += Move_Cube.animb;
                        score.text = sco.ToString();
                    break;
                case 10: animet.SetBool("new", true);
                         sco += Move_Cube.animb;
                         score.text = sco.ToString();
                    break;
                default: anim.SetBool("bool", false);
                    anime.SetBool("new", false);
                    animet.SetBool("new", false);
                    break;
            }
            
        }
    }

   
}
