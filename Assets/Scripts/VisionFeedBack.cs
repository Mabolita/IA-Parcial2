using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionFeedBack : MonoBehaviour
{
    public Material _material;

    //borrar//
    public GameObject prueba;
    //------//

    //Faces//
    public Vector3[] _frontFace = new Vector3[4];
    public Vector3[] _backFace = new Vector3[4];

    //Vertices//
    Vector3[] vertices;

    //Triangles//
    int[] triangles = new int[]
    {
        //Front Face//
        //First Triangle//
        0,2,3,
        //Second Triangle//
        3,1,0,

        //Back Face//
        //first triangle//
        4,6,7,
        //second triangle//
        7,5,4,

        //Left Face//
        //first triangle//
        8,10,11,
        //second triangle//
        11,9,8,

        //Rigth Face//
        //first triangle//
        12,14,15,
        //second triangle//
        15,13,12,

        //Up Face//
        //first triangle//
        16,18,19,
        //second triangle//
        19,17,16,

        //Down Face//
        //first triangle//
        20,22,23,
        //second triangle//
        23,21,20,
    };

    //Uvs//
    Vector2[] uvs = new Vector2[]
    {
        //front face// 0,0 abajo izquierda, 1,1 arriba derecha
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),

        //Back Face//
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),

        //Left Face//
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),

        //Rigth Face//
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),

        //Down Face//
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0),

        //Up Face//
        new Vector2(0,1),
        new Vector2(0,0),
        new Vector2(1,1),
        new Vector2(1,0)
    };

    //descomentar//

    //public VisionFeedBack(Vector3[] frontFace, Vector3[] backFace,Material material)
    //{
    //    _frontFace = frontFace;
    //    _backFace = backFace;
    //    _material = material;
    //}

    //------//

    private void Awake()
    {
            CreateObject(transform);
    }
    //borrar//
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroy(prueba);
            CreateObject(transform);
        }
    }
    //------//

    private void setVertices()
    {
        vertices = new Vector3[]
        {
            //Front Face//
            _frontFace[0],
            _frontFace[1],
            _frontFace[2],
            _frontFace[3],

            //Back Face//
            _backFace[0],
            _backFace[1],
            _backFace[2],
            _backFace[3],
            
            //Left Face//
            _frontFace[2],
            _frontFace[3],
            _backFace[0],
            _backFace[1],
            
            //Rigth Face//
            _frontFace[1],
            _frontFace[0],
            _backFace[3],
            _backFace[2],
            
            //Down Face//
            _frontFace[0],
            _frontFace[2],
            _backFace[2],
            _backFace[0],

            //Up Face//
            _frontFace[3],
            _frontFace[1],
            _backFace[1],
            _backFace[3]
        };
    }

    public void CreateObject(Transform pl)
    {
        setVertices();
        GameObject vision = new GameObject();
        vision.transform.position = pl.position;
        MeshRenderer mRend = vision.AddComponent<MeshRenderer>();
        mRend.material = _material;
        MeshFilter mFil = vision.AddComponent<MeshFilter>();
        mFil.mesh.Clear();
        mFil.mesh.vertices = vertices;
        mFil.mesh.triangles = triangles;
        mFil.mesh.uv = uvs;
        mFil.mesh.Optimize();
        mFil.mesh.RecalculateNormals();
        vision.transform.parent = pl;

        //borrar//
        prueba = vision;
        //------//

    }
}
