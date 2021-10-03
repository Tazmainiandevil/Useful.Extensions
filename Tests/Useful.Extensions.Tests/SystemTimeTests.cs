using System;
using FluentAssertions;
using Xunit;

namespace Useful.Extensions.Tests
{
    public class SystemTimeTests
    {
        [Fact]
        public void system_time_returns_now()
        {
            // Arrange
            // Act
            // Assert
            SystemTime.Now().Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(20));
        }
      
        [Fact]
        public void system_time_returns_utc_now()
        {
            // Arrange
            // Act
            // Assert
            SystemTime.UtcNow().Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(20));
        }
    }
}
