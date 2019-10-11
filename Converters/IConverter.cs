namespace MiTutorBEN.Converters
{
    public interface IConverter<E, D>
    {
        E FromDto(D dto);
        D FromEntity(E entity);
        
    }
}
