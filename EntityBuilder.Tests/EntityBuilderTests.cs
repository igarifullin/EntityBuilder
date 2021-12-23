using EntityBuilder.Abstractions;
using NUnit.Framework;

namespace EntityBuilder.Tests;

public class Tests
{
    [Test]
    public void CreatesObject()
    {
        // Arrange
        var builder = new TestEntityBuilder();

        // Act
        var instance = builder.Build();

        // Assert
        Assert.That(instance, Is.Not.Null);
    }

    [Test]
    public void FillReferenceTypeProperty()
    {
        // Arrange
        var expectedValue = 2;
        var builder = new TestEntityBuilder()
            .With(x => x.TestProperty, new InnerTestEntity(expectedValue));

        // Act
        var instance = builder.Build();

        // Assert
        Assert.That(instance.TestProperty, Is.Not.Null);
        Assert.That(instance.TestProperty.TestValue, Is.EqualTo(expectedValue));
    }

    [Test]
    public void FillValueTypeProperty()
    {
        // Arrange
        var expectedValue = 2;
        var builder = new SecondTestEntityBuilder()
            .With(x => x.TestProperty, expectedValue);

        // Act
        var instance = builder.Build();

        // Assert
        Assert.That(instance.TestProperty, Is.EqualTo(expectedValue));
    }
}

// ReSharper disable once ClassNeverInstantiated.Global
public class TestEntity : IEntity
{
    public InnerTestEntity TestProperty { get; private set; }

    public TestEntity(InnerTestEntity testProperty)
    {
        TestProperty = testProperty;
    }
}

public class InnerTestEntity : IEntity
{
    public int TestValue { get; private set; }

    public InnerTestEntity(int testValue)
    {
        TestValue = testValue;
    }
}

public class TestEntityBuilder : EntityBuilder<TestEntity>
{
}

public class SecondTestEntity : IEntity
{
    public int TestProperty { get; private set; }

    public SecondTestEntity(int testProperty)
    {
        TestProperty = testProperty;
    }
}

public class SecondTestEntityBuilder : EntityBuilder<SecondTestEntity>
{
}