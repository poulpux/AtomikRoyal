using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Explosion : MonoBehaviour
{
    private PlayerInfos infos;
    private float baseDamage; 
    List<GameObject> touchedList = new List<GameObject>();

    public Material VisionConeMaterial;
    private float radius;
    private float VisionAngle;
    private Mesh VisionConeMesh;
    private MeshFilter MeshFilter_;

    Vector3[] vertices;
    List<int> triangles = new List<int>();

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
        Destroy(gameObject, _StaticPhysics.timeExplosionStay);

        int resolutionPerSide = _StaticPhysics.explosionResolution / 4;
        int totalVertices = resolutionPerSide * 4 * 2 + 1; // 4 côtés, plus un point central
        vertices = new Vector3[totalVertices];
    }

    void Update()
    {
        //CircleExplosion();
        //SquareExplosion();
        //CrossExplosion();
        CrossExplosion(5f, 0.5f);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    public void Init(float baseDamage, float radius, PlayerInfos infos)
    {
        this.baseDamage = baseDamage;
        this.infos = infos;
        this.radius = radius;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

    private void Hit(GameObject hitedObject)
    {
        if(touchedList.Contains(hitedObject)) return;
        touchedList.Add(hitedObject);
        HitableByBombMother hit = hitedObject.GetComponentInParent<HitableByBombMother>();
        if (hit != null)
            hit.GetHit(_StaticPlayer.DamageCalculation(baseDamage, infos));
    }

    private void CircleExplosion()
    {
        int[] triangles = new int[(_StaticPhysics.explosionResolution - 1) * 3];
        Vector3[] Vertices = new Vector3[_StaticPhysics.explosionResolution + 1];
        Vertices[0] = Vector3.zero;
        float Currentangle = -VisionAngle / 2;
        float angleIncrement = VisionAngle / (_StaticPhysics.explosionResolution - 1);
        float Sine;
        float Cosine;

        for (int i = 0; i < _StaticPhysics.explosionResolution; i++)
        {
            bool hitSomethings = false;
            GameObject hitedObject = null;
            Sine = Mathf.Sin(Currentangle);
            Cosine = Mathf.Cos(Currentangle);
            Vector3 RaycastDirection = (transform.up * Cosine) + (transform.right * Sine);
            Vector3 VertForward = (Vector3.up * Cosine) + (Vector3.right * Sine);

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, RaycastDirection, radius, _StaticPhysics.ObstructingLayers);
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
                Vertices[i + 1] = VertForward * radius;
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

    private void SquareExplosion()
    {
        int resolutionPerSide = _StaticPhysics.explosionResolution / 4;
        int totalVertices = resolutionPerSide * 4 + 1; // 4 côtés, plus un point central

        Vector3[] vertices = new Vector3[totalVertices];
        List<int> triangles = new List<int>();

        // Le centre de l'explosion
        vertices[0] = Vector3.zero;

        // La longueur du côté du carré (distance du centre au bord correspondant à l'angle)
        float halfSideLength = radius / Mathf.Sqrt(2);

        // Générer les vertices pour chaque côté du carré
        int vertexIndex = 1;
        for (int side = 0; side < 4; side++)
        {
            for (int i = 0; i < resolutionPerSide; i++)
            {
                float t = (float)i / (resolutionPerSide - 1);
                Vector3 direction = Vector3.zero;

                switch (side)
                {
                    case 0: // Bas
                        direction = new Vector3(Mathf.Lerp(-halfSideLength, halfSideLength, t), -halfSideLength, 0);
                        break;
                    case 1: // Droite
                        direction = new Vector3(halfSideLength, Mathf.Lerp(-halfSideLength, halfSideLength, t), 0);
                        break;
                    case 2: // Haut
                        direction = new Vector3(Mathf.Lerp(halfSideLength, -halfSideLength, t), halfSideLength, 0);
                        break;
                    case 3: // Gauche
                        direction = new Vector3(-halfSideLength, Mathf.Lerp(halfSideLength, -halfSideLength, t), 0);
                        break;
                }

                bool hitSomethings = false;
                GameObject hitedObject = null;

                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction.normalized, direction.magnitude, _StaticPhysics.ObstructingLayers);
                foreach (var ray in hit)
                {
                    if (!ray.collider.isTrigger)
                    {
                        hitSomethings = true;
                        vertices[vertexIndex] = direction.normalized * ray.distance;
                        hitedObject = ray.collider.gameObject;
                        break;
                    }
                }

                if (!hitSomethings)
                    vertices[vertexIndex] = direction;
                else
                    Hit(hitedObject);

                vertexIndex++;
            }
        }

        // Générer les triangles
        for (int i = 1; i < totalVertices - 1; i++)
        {
            triangles.Add(0); // Centre du carré
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        // Connecter le dernier vertex au premier vertex sur le bord
        triangles.Add(0);
        triangles.Add(totalVertices - 1);
        triangles.Add(1);

        // Mettre à jour le mesh
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = vertices;
        VisionConeMesh.triangles = triangles.ToArray();
        MeshFilter_.mesh = VisionConeMesh;
    }

    private void CrossExplosion(float width, float height)
    {
        int resolutionPerSide = _StaticPhysics.explosionResolution / 8; // Divisé par 8 car 2 rectangles croisés
        int totalVertices = resolutionPerSide * 8 + 1; // 8 segments (4 de chaque rectangle), plus un point central

        Vector3[] vertices = new Vector3[totalVertices];
        List<int> triangles = new List<int>();

        // Le centre de l'explosion
        vertices[0] = Vector3.zero;

        // Définir les dimensions de la croix
        float halfWidth = width / 2;
        float halfHeight = height / 2;

        // Générer les vertices pour chaque segment de la croix
        int vertexIndex = 1;
        for (int arm = 0; arm < 4; arm++)
        {
            for (int i = 0; i < resolutionPerSide; i++)
            {
                float t = (float)i / (resolutionPerSide - 1);
                Vector3 direction = Vector3.zero;

                switch (arm)
                {
                    case 0: // Bas
                        direction = new Vector3(Mathf.Lerp(-halfWidth, halfWidth, t), -halfHeight, 0);
                        break;
                    case 1: // Droite
                        direction = new Vector3(halfWidth, Mathf.Lerp(-halfHeight, halfHeight, t), 0);
                        break;
                    case 2: // Haut
                        direction = new Vector3(Mathf.Lerp(halfWidth, -halfWidth, t), halfHeight, 0);
                        break;
                    case 3: // Gauche
                        direction = new Vector3(-halfWidth, Mathf.Lerp(halfHeight, -halfHeight, t), 0);
                        break;
                }

                bool hitSomethings = false;
                GameObject hitedObject = null;

                RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction.normalized, direction.magnitude, _StaticPhysics.ObstructingLayers);
                foreach (var ray in hit)
                {
                    if (!ray.collider.isTrigger)
                    {
                        hitSomethings = true;
                        vertices[vertexIndex] = direction.normalized * ray.distance;
                        hitedObject = ray.collider.gameObject;
                        break;
                    }
                }

                if (!hitSomethings)
                    vertices[vertexIndex] = direction;
                else
                    Hit(hitedObject);

                vertexIndex++;
            }
        }

        // Générer les triangles
        for (int i = 1; i < totalVertices - 1; i++)
        {
            triangles.Add(0); // Centre de la croix
            triangles.Add(i);
            triangles.Add(i + 1);
        }
        // Connecter le dernier vertex au premier vertex sur le bord
        triangles.Add(0);
        triangles.Add(totalVertices - 1);
        triangles.Add(1);

        // Mettre à jour le mesh
        VisionConeMesh.Clear();
        VisionConeMesh.vertices = vertices;
        VisionConeMesh.triangles = triangles.ToArray();
        MeshFilter_.mesh = VisionConeMesh;
    }
}
