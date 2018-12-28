

namespace Enjoy.Core.EnjoyModels
{
    public class QueryFilter<T>
    {
        public T[] Ids { get; set; }
        public string AppId { get; set; }
        public string Secret { get; set; }
    }
}