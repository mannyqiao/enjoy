
namespace Enjoy.Core
{
    using System;

    public interface ISMSEntity : IEquatable<ISMSEntity>
    {
        string Mobile { get; }
        long CreatedTime { get; }
        string GetBody();
    }
}