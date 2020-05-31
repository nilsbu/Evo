using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCreation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TerrainData data = gameObject.GetComponent<Terrain>().terrainData;

        int r = data.detailResolution;
        float[,] heights = new float[r+1, r+1];

        InitSquare(ref heights, r, 1f);

        float sigma = 1;
        for (int rt = r; rt >= 2; rt/=2)
        {
            for (int y = 0; y < r; y+= rt)
            {
                for (int x = 0; x < r; x+= rt)
                {
                    DiamondSquare(ref heights, rt, y, x, sigma);
                }
            }
            sigma /= 2;
        }
        Clamp(ref heights, r);


        data.SetHeights(0, 0, heights);
    }

    private void InitSquare(ref float[,] data, int r, float sigma)
    {
        data[0, 0] = NormalDistribution.Rand(0, 1);
        data[r, 0] = NormalDistribution.Rand(0, 1);
        data[0, r] = NormalDistribution.Rand(0, 1);
        data[r, r] = NormalDistribution.Rand(0, 1);
    }

    private void DiamondSquare(ref float[,] data, int r, int y, int x, float sigma)
    {
        {
            // Center
            float avg = 0;
            avg += data[y, x];
            avg += data[y + r, x];
            avg += data[y, x + r];
            avg += data[y + r, x + r];

            data[y + r / 2, x + r / 2] = NormalDistribution.Rand(avg / 4, sigma / Mathf.Sqrt(2));
        }
        {
            // Left
            float avg = 0;
            avg += data[y, x];
            avg += data[y + r / 2, x + r / 2];
            avg += data[y + r, x];
            if (x > 0)
            {
                avg += data[y, x - r / 2];
                data[y + r / 2, x] = NormalDistribution.Rand(avg / 4, sigma / 2);
            }
            else
            {
                data[y + r / 2, x] = NormalDistribution.Rand(avg / 3, sigma / 2);
            }
        }
        {
            // Right
            float avg = 0;
            avg += data[y, x + r];
            avg += data[y + r / 2, x + r / 2];
            avg += data[y + r, x + r];
            if ((x + r + 1) * (x + r + 1) < data.Length)
            {
                avg += data[y, x + r + r / 2];
                data[y + r / 2, x + r] = NormalDistribution.Rand(avg / 4, sigma / 2);
            }
            else
            {
                data[y + r / 2, x + r] = NormalDistribution.Rand(avg / 3, sigma / 2);
            }
        }
        {
            // Top
            float avg = 0;
            avg += data[y, x];
            avg += data[y + r / 2, x + r / 2];
            avg += data[y, x + r];
            if (y > 0)
            {
                avg += data[y - r / 2, x];
                data[y, x + r / 2] = NormalDistribution.Rand(avg / 4, sigma / 2);
            }
            else
            {
                data[y, x + r / 2] = NormalDistribution.Rand(avg / 3, sigma / 2);
            }
        }
        {
            // Bottom
            float avg = 0;
            avg += data[y + r, x];
            avg += data[y + r / 2, x + r / 2];
            avg += data[y + r, x + r];
            if ((y + r + 1) * (y + r + 1) < data.Length)
            {
                avg += data[y + r + r / 2, x];
                data[y + r, x + r / 2] = NormalDistribution.Rand(avg / 4, sigma / 2);
            }
            else
            {
                data[y + r, x + r / 2] = NormalDistribution.Rand(avg / 3, sigma / 2);
            }
        }
    }

    private void Clamp(ref float[,] data, int r)
    {
        float min = float.MaxValue, max = -float.MaxValue;
        for (int y = 0; y <= r; y++)
        {
            for (int x = 0; x <= r; x++)
            {
                if (min > data[y, x])
                {
                    min = data[y, x];
                }
                if (max < data[y, x])
                {
                    max = data[y, x];
                }
            }
        }

        for (int y = 0; y <= r; y++)
        {
            for (int x = 0; x <= r; x++)
            {
                data[y, x] = (data[y, x] - min) / (max - min);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
