using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class PlayModetests
{
    [Test]
    public void PlayModeTestsSimplePasses()
    {
        Assert.True(true);
    }

    [UnityTest]
    public IEnumerator PlayModeTestsWithEnumeratorPasses()
    {
        Assert.True(true);
        yield return null;
        Assert.True(true);
    }
}
