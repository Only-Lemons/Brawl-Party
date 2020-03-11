using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec3
{
    //USO DE FUNCÕES LAMBDAS EM EXCESSO

    float x, y, z;
    public Vec3(float x, float y, float z)
    {

        this.x = x;
        this.y = y;
        this.z = z;
    }


    public string ToString()
        => x.ToString() + "," + y.ToString() + "," + z.ToString();


    public static Vec3 Zero()
        => new Vec3(0, 0, 0);


    public float X
    {
        get => x;
        set => x = value;
    }

    public float Y
    {
        get => y;
        set => y = value;
    }

    public float Z
    {
        get => z;
        set => z = value;
    }

    //+
    public static Vec3 operator +(Vec3 a, Vec3 b)
        => new Vec3(a.x + b.x, a.y + b.y, a.z + b.z);

    //-
    public static Vec3 operator -(Vec3 a, Vec3 b)
        => new Vec3(a.x - b.x, a.y - b.y, a.z - b.z);

    //crossproduct e crossfire
    //diferenciados (na matemática nao faz sentido)
    public static Vec3 operator *(Vec3 a, Vec3 b)
        => new Vec3(a.x * b.x, a.y * b.y, a.z * b.z);

    public static Vec3 operator /(Vec3 a, Vec3 b)
        => new Vec3(a.x / b.x, a.y / b.y, a.z / b.z);

    //* e / na matematica
    // matematica
    public static Vec3 operator *(float a, Vec3 b)
        => new Vec3(a * b.x, a * b.y, a * b.z);

    public static Vec3 operator *(Vec3 a, float b)
        => new Vec3(a.x * b, a.y * b, a.z * b);

    public static Vec3 operator %(Vec3 a, Vec3 b)
        => new Vec3(a.x % b.x, a.y % b.y, a.z % b.z);

    public float Magnitude(Vec3 b) =>
        Mathf.Sqrt(b.x * b.x) + Mathf.Sqrt(b.y * b.y) + Mathf.Sqrt(b.z * b.z);

}