using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Edge
{
    public int v1;
    public int v2;

    public Edge(int a, int b)
    {
        v1 = a < b ? a : b;
        v2 = a < b ? b : a;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is Edge)) return false;
        Edge edge = (Edge)obj;
        return edge.v1 == v1 && edge.v2 == v2;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + v1.GetHashCode();
            hash = hash * 23 + v2.GetHashCode();
            return hash;
        }
    }
}
