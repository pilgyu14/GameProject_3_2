using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSelection : MonoBehaviour
{
    [SerializeField]
    private LayerMask targetMask; 
    
    SelectedDictionary selected_table;
    RaycastHit hit;

    bool dragSelect;

    //Collider variables
    //=======================================================//

    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1; // 첫 클릭 지점
    Vector3 p2; // 마우스 up 했을때 지점 

    //the corners of our 2d selection box
    Vector2[] corners;

    //the vertices of our meshcollider
    Vector3[] verts;
    Vector3[] vecs;

    void Start()
    {
        selected_table = GetComponent<SelectedDictionary>();
        dragSelect = false;
    }

    void Update()
    {
        //1. when left mouse button clicked (but not released)
        // 좌클릭시 포지션 받고 
        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }

        //2. while left mouse button held
        // 누르는 중에 일정 범위 넘으면 다중 드래그..?   
        if (Input.GetMouseButton(0))
        {
            Debug.Log("@@" + (p1 - Input.mousePosition).magnitude );
            if((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }

        //3. when mouse button comes up
        if (Input.GetMouseButtonUp(0))
        {
            // 하나만 선택시 
            if(dragSelect == false) //single select
            {
                Ray ray = Util.MainCam.ScreenPointToRay(p1);

                if(Physics.Raycast(ray,out hit, 50000.0f,targetMask))
                {
                    // 이미 선택중인 것에 추가 
                    if (Input.GetKey(KeyCode.LeftShift)) //inclusive select
                    {
                        selected_table.AddSelected(hit.transform.gameObject);
                    }
                    // 기존 선택 배제하고 새로 선택 
                    else //exclusive selected
                    {
                        selected_table.DeselectAll();
                        selected_table.AddSelected(hit.transform.gameObject);
                    }
                }
                // 아무것도 없었다면 
                else //if we didnt hit something
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {
                        //do nothing
                    }
                    else
                    {
                        selected_table.DeselectAll();
                    }
                }
            }
            // 다중 선택시 
            else //marquee select
            {
                // 2d 박스로 그렸을 때 바닥에 닿는 지점까지 
                // 레이캐스트 쏴서 그 4개의 정점 좌표 
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                p2 = Input.mousePosition;
                // 메쉬 생성할 Vector2 좌표 정점 배열 
                corners = GetBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Util.MainCam.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, 50000.0f ,targetMask))
                    {
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.Log("Ray Origin" + ray.origin);
                        Debug.Log("Hit Point" + hit.point);
                        Debug.DrawLine(Util.MainCam.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }

                //generate the mesh
                selectionMesh = GenerateSelectionMesh(verts,vecs);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selected_table.DeselectAll();
                }

               Destroy(selectionBox, 0.02f);

            }//end marquee select

            dragSelect = false;

        }
       
    }

    private void OnGUI()
    {
        // 드래그 선택이라면
        if(dragSelect == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    //create a bounding box (4 corners in order) from the start and end mouse position
    /// <summary>
    /// 메쉬 생성할 정점 위치 4개 반환 
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    Vector2[] GetBoundingBox(Vector2 p1,Vector2 p2)
    {
        // Min and Max to get 2 corners of rectangle regardless of drag direction.
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);

        // 0 = top left; 1 = top right; 2 = bottom left; 3 = bottom right;
        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };
        return corners;

         }

    //generate a mesh from the 4 bottom points
   /// <summary>
   /// 선택 메쉬 생성 
   /// </summary>
   /// <param name="corners"></param>
   /// <param name="vecs"></param>
   /// <returns></returns>
    Mesh GenerateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for(int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for(int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }
        Mesh _selectionMesh = new Mesh();

        _selectionMesh.vertices = verts;
        _selectionMesh.triangles = tris;

        return _selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        selected_table.AddSelected(other.gameObject);
    }

}