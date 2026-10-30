using System;
using FluentAssertions;
using Rem.FlexiBeeSDK.Model.Products.Attributes;
using Xunit;

namespace Rem.FlexiBeeSDK.Tests;

public class ProductReferenceTests
{
    [Fact]
    public void FromCode_PrefixesRequestValueWithCode()
    {
        ProductReference.FromCode("ABC001").ToRequestValue().Should().Be("code:ABC001");
    }

    [Fact]
    public void FromId_UsesNumericRequestValue()
    {
        ProductReference.FromId(1033).ToRequestValue().Should().Be("1033");
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("A'B")]
    [InlineData("A\\B")]
    public void FromCode_ThrowsForEmptyOrQuotedCode(string code)
    {
        Action act = () => ProductReference.FromCode(code);

        act.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void FromId_ThrowsForNonPositiveId(int id)
    {
        Action act = () => ProductReference.FromId(id);

        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}
