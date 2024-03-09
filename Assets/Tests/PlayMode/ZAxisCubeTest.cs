using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class ZAxisCubeTest
{
    [UnityTest]
    public IEnumerator Cube_Moves_Correctly_Along_ZAxis()
    {
        //Arrenge
        GameObject gameObject = new GameObject();
        ZAxisCube zAxisCube = gameObject.AddComponent<ZAxisCube>();
        zAxisCube.SetSpeed(2.0f);
        Vector3 initialPosition = zAxisCube.transform.position;

        //Act
        yield return new WaitForSeconds(1.0f);

        //Assert
        float finalPositionZ = zAxisCube.transform.position.z;
        Assert.That(finalPositionZ, Is.Not.EqualTo(initialPosition.z));

       /* if (zAxisCube.IsMovingForward)
        {
            Assert.That(finalPositionZ, Is.GreaterThan(initialPosition.z));
        }
        else
        {
            Assert.That(finalPositionZ, Is.LessThan(initialPosition.z));
        }*/

    }
}
