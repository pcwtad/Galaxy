namespace CommentService.WebApi
{
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        public Type[] DbContextType { get; init; }
        public UnitOfWorkAttribute(params Type[] dbContextType)
        {
            DbContextType = dbContextType;
        }
    }
}
