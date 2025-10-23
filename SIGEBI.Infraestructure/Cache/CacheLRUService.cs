using SIGEBI.Domain.Interfaces.Cache;

namespace SIGEBI.Infraestructure.Cache
{
    public class CacheLRUService : ICacheService
    {
        private readonly int _capacity;
        private readonly Dictionary<string , LinkedListNode<(string Key, object Value)>> _map;
        private readonly LinkedList<(string Key, object Value)> _list;

        public CacheLRUService(int capacity = 100)
        {
            _capacity = capacity;
            _map = new Dictionary<string, LinkedListNode<(string Key, object Value)>>();
            _list = new LinkedList<(string Key, object Value)>();
        }

        public void ClearKeys()
        {
            _map.Clear();
            _list.Clear();
        }

        public void Remove(string key)
        {
            if (_map.TryGetValue(key, out var node))
            {
                _list.Remove(node);
                _map.Remove(key);
            }
        }

        public void Set<T>(string key, T value)
        {
            if (_map.TryGetValue(key, out var node))
            {
                _list.Remove(node);
            }
            else if(_map.Count >= _capacity)
            {
                var last = _list.Last!;
                _map.Remove(last.Value.Key);
                _list.RemoveLast();
            }

            var newNode = new LinkedListNode<(string, object)>((key, value!));
            _list.AddFirst(newNode);
            _map[key] = newNode;
        }

        public bool TryGet<T>(string key, out T value)
        {
            if(_map.TryGetValue(key, out var node))
            {
                _list.Remove(node);
                _list.AddFirst(node);
                value = (T)node.Value.Value;
                return true;
            }
            value = default!;
            return false;
        }
    }
}
