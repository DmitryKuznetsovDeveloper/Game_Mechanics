namespace SaveSystem
{
    public interface ISaveLoadRepository<T>
    {
        void Save(T data);
        T Load();
    }
}