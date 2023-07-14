using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TextGrident : BaseMeshEffect
{
    public Gradient _myGradient;
    public Text _text;

    float _time;

    private void Update()
    {
        _time += Time.deltaTime;
        _text.FontTextureChanged();
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> vertices = new List<UIVertex>();
        vh.GetUIVertexStream(vertices);

        // �ؽ�Ʈ�� ó���� �� ����
        float min = vertices.Min(t => t.position.x);
        float max = vertices.Max(t => t.position.x);

        for (int i = 0; i < vertices.Count; i++)
        {
            UIVertex v = vertices[i];
            float curXNormalized = Mathf.InverseLerp(min, max, v.position.x);
            curXNormalized = Mathf.PingPong(curXNormalized + _time, 1f);
            Color c = _myGradient.Evaluate(curXNormalized);
            v.color = new Color(c.r, c.g, c.b, 1);
            vertices[i] = v;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(vertices);
    }
}
