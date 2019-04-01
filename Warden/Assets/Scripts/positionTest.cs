using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positionTest : MonoBehaviour
{
    public bool justAttacked = false;
    public Vector3 newPos;

    public Vector3 cubePos;
    public GameObject cubeLoc;
    public GameObject mycube;
    void LateUpdate()
    {
        /*  if(justAttacked == true)
          {
              transform.position = newPos;
              justAttacked = false;
          }*/
    }


    void spawnCube()
    {
        Instantiate(mycube, cubeLoc.transform.position, Quaternion.identity);
    }
    void updatePosistion()
    {
        transform.position = GameObject.Find("mycube(Clone)").transform.position;
        Destroy(GameObject.Find("mycube(Clone)"));
       // justAttacked = true;
    }
}
