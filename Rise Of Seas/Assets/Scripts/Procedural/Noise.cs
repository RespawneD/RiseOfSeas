using UnityEngine;
using System.Collections;

public static class Noise {


    public static float MakeMask(int width, int height, int posX, int posY, float oldValue)
    {
        float distance_x = Mathf.Abs(posX - width * 0.5f);
        float distance_y = Mathf.Abs(posY - height * 0.5f);
        float distance = Mathf.Sqrt(distance_x * distance_x + distance_y * distance_y); // circular mask

        float max_width = width * 0.6f - 2.0f;
        float delta = distance / max_width;
        float gradient = delta * delta;

        oldValue *= Mathf.Max(0.0f, 1.0f - gradient);
        return oldValue;
    }

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
		float[,] noiseMap = new float[mapWidth,mapHeight];

		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offset.x;
			float offsetY = prng.Next (-100000, 100000) + offset.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

		if (scale <= 0) {
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
		
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				noiseMap [x, y] = noiseHeight;
			}
		}

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = MakeMask(mapWidth, mapHeight, x, y, Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]));
            }
		}



		return noiseMap;
	}

}
