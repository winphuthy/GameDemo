using Cinemachine;
using Unity.Mathematics;


public class ExperienceSystem
{
    public static int MaxEXP(int level)
    {
        return (int) (100 * math.pow(1.25, level - 1));
    }
}