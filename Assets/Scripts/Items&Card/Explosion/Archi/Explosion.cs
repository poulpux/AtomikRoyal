using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public PlayerInfos infos;
    public float baseDomage; 
    List<GameObject> touchedList = new List<GameObject>();

    public Material VisionConeMaterial;
    public LayerMask VisionObstructingLayer;//layer with objects that obstruct the enemy view, like walls, for example
    public float VisionRange = 5f;
    private float VisionAngle;
    private int VisionConeResolution;//the vision cone will be made up of triangles, the higher this value is the pretier the vision cone will be
    private Mesh VisionConeMesh;
    private MeshFilter MeshFilter_;

    [TextArea]
    public string AboutVisionField = "Warning : Add this to an empty object, not a capsule\r\nDont forget to add the material from the VisionConeMaterial variable and to change the VisionObstructingLayer.";
    //Create all of these variables, most of them are self explanatory, but for the ones that aren't i've added a comment to clue you in on what they do
    //for the ones that you dont understand dont worry, just follow along
    void Start()
    {
        transform.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        MeshFilter_ = transform.AddComponent<MeshFilter>();
        VisionConeMesh = new Mesh();
        VisionAngle = 360f;
        VisionAngle *= Mathf.Deg2Rad;
        VisionConeResolution = _StaticPhysics.explosionResolution;
    }

    void Update()
    {
        ConeVision();
    }

    public void Init()
    {

    }

    private void Hit(GameObject hitedObject)
    {

    }

    private void ConeVision()
    {
        int[] triangles = new int[(VisionConeResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[VisionConeResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIncrement = VisionAngle / (VisionConeResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < VisionConeResolution; i++)
        {
            bool hitSomethings = false;
            GameObject hitedObject = null;
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.forward * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.forward * Cosine) + (Vector3.right * Sine);

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, RaycastDirection, VisionRange, VisionObstructingLayer);
            foreach (var ray in hit)
            {
                if (!ray.collider.isTrigger)
                {
                    hitSomethings = true;
                    Vertices[i + 1] = VertForward * ray.distance;
                    hitedObject = ray.collider.gameObject;
                    break;
                }
            }

            if (!hitSomethings)
                Vertices[i + 1] = VertForward * VisionRange;
            else
                Hit(hitedObject);
            Currentangle += angleIncrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        VisionConeMesh.Clear();
        VisionConeMesh.vertices = Vertices;
        VisionConeMesh.triangles = triangles;
        MeshFilter_.mesh = VisionConeMesh;
    }
}
