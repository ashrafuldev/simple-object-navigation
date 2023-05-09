using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class ObjectData 
{
    public float[] position;

    public ObjectData(ObjectCreator objectCreator)
    {
        Vector3 objectpos = objectCreator.transform.position;
        position = new float[]
        {
            objectpos.x,objectpos.y,objectpos.z
        };
    }
}
