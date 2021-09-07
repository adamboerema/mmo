using System;
namespace Common.Builders
{
    public interface IBuilder<T>
    {
        public T Build();
    }
}
