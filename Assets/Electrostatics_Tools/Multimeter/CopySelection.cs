//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class CopySelection : MonoBehaviour
//{
//    // GameObject template (prefab) for each object
//    public GameObject CubeTemplate, CylinderTemplate, SphereTemplate, 
//        PlaneTemplate, RodTemplate, PointTemplate;  
//    // Spawn locations for each object
//    public Transform CubeOrigin, SphereOrigin, CylinderOrigin, PlaneOrigin, 
//        RodOrigin, PointOrigin;  

//    // List of all Objects in the scene
//    private List<ChargedObject> SceneObjects;  

//    // Start is called before the first frame update
//    void Start()
//    {
//        SceneObjects = new List<ChargedObject>();
//        // Initialize a new Cube
//        CloneGameObject(CubeTemplate, CubeOrigin.position, Quaternion.identity);  
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//        // Create a new clone of the Cube whenever it is picked up
//        if (Vector3.Distance(SceneObjects[SceneObjects.Count-1].GetGameObject().transform.position, 
//            CubeOrigin.position) > 0.2)
//        {
//            CloneGameObject(CubeTemplate, CubeOrigin.position, Quaternion.identity);
//        }
        
//    }

//    // Retreive a GameObject's corresponding ChargedObject. 
//    public ChargedObject GetChargedObjectRef(GameObject obj)
//    {
//        foreach (ChargedObject co in SceneObjects)
//        {
//            if (co.GetGameObject().GetInstanceID() == obj.GetInstanceID())
//            {
//                return co;
//            }
//        }
//        return null;
//    }

//    // Creates a new ChargedObject and adds it to the list of existing ChargedObjects.
//    // Each ChargedObject has a reference to its GameObject.
//    private void CloneGameObject(GameObject obj, Vector3 pos, Quaternion rot)
//    {
//        Debug.Log("new ChargedObject: " + obj.name + (SceneObjects.Count).ToString());
//        GameObject templateClone = Instantiate(obj, pos, rot);
//        SceneObjects.Add(new ChargedObject(obj.name + (SceneObjects.Count).ToString(), templateClone));
//    }

//}