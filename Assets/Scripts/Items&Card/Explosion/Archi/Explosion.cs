using System.Collections.Generic;
using UnityEngine;

public enum EXPLOSIONSHAPE
{
    CIRCLE,
    SQUARE,
    CROSS,
    LONG_RECTANGLE,
    WIDE_RECTANGLE
}

public class Explosion : MonoBehaviour
{
    public EXPLOSIONSHAPE shape;

    private PlayerInfos infos;
    private float baseDamage;
    private List<GameObject> touchedList = new List<GameObject>();

    public Material VisionConeMaterial;
    private float radius, length, thickness;
    private float visionAngle;
    private Mesh visionConeMesh;
    private MeshFilter meshFilter;

    private Vector3[] vertices;
    private List<int> triangles = new List<int>();
    private int vertexIndex;

    [TextArea]
    public string AboutVisionField = "Warning: Add this to an empty object, not a capsule.\nDon't forget to add the material from the VisionConeMaterial variable and to change the VisionObstructingLayer.";

    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>().material = VisionConeMaterial;
        visionConeMesh = new Mesh();
        Destroy(gameObject, _StaticPhysics.timeExplosionStay);

        shape = EXPLOSIONSHAPE.SQUARE;

        InitializeVertices();
    }

    void FixedUpdate()
    {
        if (shape == EXPLOSIONSHAPE.CIRCLE)
            CircleExplosion();
        else if (shape == EXPLOSIONSHAPE.SQUARE)
            RectangleExplosion(radius / Mathf.Sqrt(2), radius / Mathf.Sqrt(2));
        else if (shape == EXPLOSIONSHAPE.CROSS)
            CrossExplosion(length, thickness);
        else if (shape == EXPLOSIONSHAPE.LONG_RECTANGLE)
            RectangleExplosion(length, thickness);
        else if (shape == EXPLOSIONSHAPE.WIDE_RECTANGLE)
            RectangleExplosion(thickness, length);
    }

    public void Init(float baseDamage, float radius, EXPLOSIONSHAPE shape, PlayerInfos infos)
    {
        this.baseDamage = baseDamage;
        this.infos = infos;
        this.radius = radius;
        this.shape = shape;
        if (shape == EXPLOSIONSHAPE.LONG_RECTANGLE || shape == EXPLOSIONSHAPE.WIDE_RECTANGLE || shape == EXPLOSIONSHAPE.CROSS)
            Debug.LogError("Wrong shape bro");
    }
    public void Init(float baseDamage, float length, float thickness, EXPLOSIONSHAPE shape, PlayerInfos infos)
    {
        this.baseDamage = baseDamage;
        this.infos = infos;
        this.length = Mathf.Max(length, thickness);
        this.thickness = Mathf.Min(length, thickness);
        this.shape = shape;
        if (shape == EXPLOSIONSHAPE.CIRCLE || shape == EXPLOSIONSHAPE.SQUARE)
            Debug.LogError("Wrong shape bro");
    }

    private void Hit(GameObject hitedObject)
    {
        if (touchedList.Contains(hitedObject)) return;
        touchedList.Add(hitedObject);
        HitableByBombMother hit = hitedObject.GetComponentInParent<HitableByBombMother>();
        if (hit != null)
            hit.GetHit(_StaticPlayer.DamageCalculation(baseDamage, infos));
    }

    private void InitializeVertices()
    {
        switch (shape)
        {
            case EXPLOSIONSHAPE.CIRCLE:
                visionAngle = 360f * Mathf.Deg2Rad;
                vertices = new Vector3[_StaticPhysics.explosionResolution + 1];
                break;
            case EXPLOSIONSHAPE.SQUARE:
            case EXPLOSIONSHAPE.LONG_RECTANGLE:
            case EXPLOSIONSHAPE.WIDE_RECTANGLE:
                int resolutionPerSide = _StaticPhysics.explosionResolution / 4;
                vertices = new Vector3[resolutionPerSide * 4 + 1];
                break;
            case EXPLOSIONSHAPE.CROSS:
                resolutionPerSide = _StaticPhysics.explosionResolution / 4;
                vertices = new Vector3[resolutionPerSide * 8 + 1];
                break;
        }
    }

    private void CircleExplosion()
    {
        int resolution = _StaticPhysics.explosionResolution;
        int[] triangles = new int[(resolution - 1) * 3];
        vertices[0] = Vector3.zero;
        float currentAngle = -visionAngle / 2;
        float angleIncrement = visionAngle / (resolution - 1);

        for (int i = 0; i < resolution; i++)
        {
            bool hitSomething = false;
            GameObject hitObject = null;
            Vector3 direction = new Vector3(Mathf.Cos(currentAngle), Mathf.Sin(currentAngle), 0);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, radius, _StaticPhysics.ObstructingLayers);

            foreach (var hit in hits)
            {
                if (!hit.collider.isTrigger)
                {
                    hitSomething = true;
                    vertices[i + 1] = direction * hit.distance;
                    hitObject = hit.collider.gameObject;
                    break;
                }
            }

            if (!hitSomething)
                vertices[i + 1] = direction * radius;
            else
                Hit(hitObject);

            currentAngle += angleIncrement;
        }

        for (int i = 0, j = 0; i < triangles.Length; i += 3, j++)
        {
            triangles[i] = 0;
            triangles[i + 1] = j + 1;
            triangles[i + 2] = j + 2;
        }

        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices;
        visionConeMesh.triangles = triangles;
        meshFilter.mesh = visionConeMesh;
    }

    private void RectangleExplosion(float width, float height)
    {
        vertexIndex = 1;
        GenerateRectangle(width / 2, height / 2);
        GenerateRectangleTriangles();
    }

    private void CrossExplosion(float length, float thickness)
    {
        vertexIndex = 1;
        GenerateRectangle(length / 2, thickness / 2);
        GenerateRectangle(thickness / 2, length / 2);
        GenerateRectangleTriangles();
    }

    private void GenerateRectangle(float halfWidth, float halfHeight)
    {
        int resolutionPerSide = _StaticPhysics.explosionResolution / 4;

        for (int side = 0; side < 4; side++)
        {
            for (int i = 0; i < resolutionPerSide; i++)
            {
                float t = (float)i / (resolutionPerSide - 1);
                Vector3 direction = Vector3.zero;

                switch (side)
                {
                    case 0: // Bottom
                        direction = new Vector3(Mathf.Lerp(-halfWidth, halfWidth, t), -halfHeight, 0);
                        break;
                    case 1: // Right
                        direction = new Vector3(halfWidth, Mathf.Lerp(-halfHeight, halfHeight, t), 0);
                        break;
                    case 2: // Top
                        direction = new Vector3(Mathf.Lerp(halfWidth, -halfWidth, t), halfHeight, 0);
                        break;
                    case 3: // Left
                        direction = new Vector3(-halfWidth, Mathf.Lerp(halfHeight, -halfHeight, t), 0);
                        break;
                }

                bool hitSomething = false;
                GameObject hitObject = null;
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction.normalized, direction.magnitude, _StaticPhysics.ObstructingLayers);

                foreach (var hit in hits)
                {
                    if (!hit.collider.isTrigger)
                    {
                        hitSomething = true;
                        vertices[vertexIndex] = direction.normalized * hit.distance;
                        hitObject = hit.collider.gameObject;
                        break;
                    }
                }

                if (!hitSomething)
                    vertices[vertexIndex] = direction;
                else
                    Hit(hitObject);

                vertexIndex++;
            }
        }
    }

    private void GenerateRectangleTriangles()
    {
        for (int i = 1; i < vertexIndex - 1; i++)
        {
            if (i == (vertexIndex - 1) / 2) continue;
            triangles.Add(0);
            triangles.Add(i);
            triangles.Add(i + 1);
        }

        UpdateMesh();
    }

    private void UpdateMesh()
    {
        visionConeMesh.Clear();
        visionConeMesh.vertices = vertices;
        visionConeMesh.triangles = triangles.ToArray();
        meshFilter.mesh = visionConeMesh;
    }
}