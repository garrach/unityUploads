﻿using System.Collections.Generic;
using System.Xml;
using UnityEngine;

/*
    Copyright (c) 2017 Sloan Kelly

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

class MapReader : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<ulong, OsmNode> nodes;

    [HideInInspector]
    public List<OsmWay> ways;

    public bool customFbx = false;

    [HideInInspector]
    public OsmBounds bounds;

    public GameObject groundPlane;

    [Tooltip("The resource file that contains the OSM map data")]
    public string resourceFile;

    public bool IsReady { get; private set; }
    public LineRenderer lineRenderer;
    public Material roadMaterial;
    public List<GameObject> fbxBuildings = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        resourceFile = PlayerPrefs.GetString("city");

        nodes = new Dictionary<ulong, OsmNode>();
        ways = new List<OsmWay>();

        var txtAsset = Resources.Load<TextAsset>(resourceFile);

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(txtAsset.text);

        SetBounds(doc.SelectSingleNode("/osm/bounds"));
        GetNodes(doc.SelectNodes("/osm/node"));
        GetWays(doc.SelectNodes("/osm/way"));

        float minx = (float)MercatorProjection.lonToX(bounds.MinLon);
        float maxx = (float)MercatorProjection.lonToX(bounds.MaxLon);
        float miny = (float)MercatorProjection.latToY(bounds.MinLat);
        float maxy = (float)MercatorProjection.latToY(bounds.MaxLat);

        groundPlane.transform.localScale = new Vector3((maxx - minx) / 2, 1, (maxy - miny) / 2);
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.enabled = false;
        IsReady = true;


        foreach (OsmWay w in ways)
        {
            if (w.Visible && !w.IsBoundary)
            {
                Color lineColor = w.IsBoundary ? Color.cyan : Color.red;

                LineRenderer roadRenderer = CreateLineRenderer(lineColor);
                DrawRoad(w, roadRenderer);
            }
        }
    }
    
    void Update()
    {
        foreach (OsmWay w in ways)
        {
            lineRenderer.positionCount = w.NodeIDs.Count;
            if (w.Visible)
            {
                Color c = Color.cyan;// cyan for buildings
               if (!w.IsBoundary)
                    {
                        c = Color.red; // red for roads
                    }
                for (int i = 1; i < w.NodeIDs.Count ; i++)
                {
                    OsmNode p1 = nodes[w.NodeIDs[i - 1]];
                    OsmNode p2 = nodes[w.NodeIDs[i]];

                    Vector3 v1 = p1 - bounds.Centre;
                    Vector3 v2 = p2 - bounds.Centre;
                    
                    Debug.DrawLine(v1, v2, c);                   
                }
            }
        }
    }

    void GetWays(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode node in xmlNodeList)
        {
            OsmWay way = new OsmWay(node);
            ways.Add(way);
        }
    }

    void GetNodes(XmlNodeList xmlNodeList)
    {
        foreach (XmlNode n in xmlNodeList)
        {
            OsmNode node = new OsmNode(n);
            nodes[node.ID] = node;
        }
    }

    void SetBounds(XmlNode xmlNode)
    {
        bounds = new OsmBounds(xmlNode);
    }

    void DrawRoad(OsmWay road, LineRenderer roadRenderer)
    {
        roadRenderer.positionCount = road.NodeIDs.Count;

        for (int i = 0; i < road.NodeIDs.Count; i++)
        {
            OsmNode node = nodes[road.NodeIDs[i]];
            Vector3 position = node - bounds.Centre;
            roadRenderer.SetPosition(i, position);
        }
    }

    LineRenderer CreateLineRenderer(Color color)
    {
        GameObject roadObject = new GameObject("Road");
        roadObject.transform.parent = transform;
        roadObject.transform.rotation = Quaternion.Euler(90f, 0, 0);
        LineRenderer lineRenderer = roadObject.AddComponent<LineRenderer>();
        lineRenderer.material = roadMaterial; // Assign the road material
        lineRenderer.startWidth = 10f;
        lineRenderer.endWidth = 10f;
        lineRenderer.alignment = LineAlignment.TransformZ;
        lineRenderer.textureMode = LineTextureMode.RepeatPerSegment;
        lineRenderer.enabled = true;
        LineRendererCollision.lineRenderer = lineRenderer;

        return lineRenderer;
    }
}
