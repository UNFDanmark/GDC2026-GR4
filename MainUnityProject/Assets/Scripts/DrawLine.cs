using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class DrawLine : MaskableGraphic
{
    public Vector2[] points;

    public float girth = 10;
    
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        if(points.Length < 2) return;

        int i = 0;
        foreach (Vector2 point in points)
        {
            if (i == 0)
            {
                i++;
                continue;
            };
            
            Vector2 dir = point - points[i - 1];
            float mag = dir.magnitude;
            dir = dir.normalized;
            
            Vector2 normal = new Vector2(-dir.y, dir.x).normalized;

            Vector2 v1 = normal * -girth / 2;
            Vector2 v2 = normal * girth / 2;

            Vector2 vec2 = dir * mag;
            Vector2 normal2 = new Vector2(-vec2.y, vec2.x).normalized;
            
            Vector2 v3 = normal2 * -girth / 2;
            Vector2 v4 = normal2 * girth / 2;

            Color32 c = new Color32(255, 255, 255, 255);
            vh.AddVert(new Vector3(v1.x, v1.y, 0), c, Vector2.zero);
            vh.AddVert(new Vector3(v2.x, v2.y, 0), c, Vector2.zero);
            vh.AddVert(new Vector3(v3.x, v3.y, 0), c, Vector2.zero);
            vh.AddVert(new Vector3(v4.x, v4.y, 0), c, Vector2.zero);

            vh.AddTriangle(vh.currentVertCount-2, vh.currentVertCount-4, vh.currentVertCount-3);
            vh.AddTriangle(vh.currentVertCount-1, vh.currentVertCount-2, vh.currentVertCount-3);
            
            i++;
        }
        
        /*vh.AddVert(new Vector3(0, 200, 0), new Color32(255, 255, 255, 255), Vector2.zero);
        vh.AddVert(new Vector3(-100, 0, 0), new Color32(255, 255, 255, 255), Vector2.zero);
        vh.AddVert(new Vector3(100, 0, 0), new Color32(255, 255, 255, 255), Vector2.zero);
        
        vh.AddTriangle(2, 1, 0); */
        //base.OnPopulateMesh(vh);
    }
}
