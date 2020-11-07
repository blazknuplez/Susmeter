namespace Susmeter.Ef.Infrastructure
{
    public static class EfExtensions
    {
        private static string Entity = "Entity";

        public static string TableName(this string entityName)
        {
            if (entityName.EndsWith(Entity))
                return entityName.Substring(0, entityName.Length - Entity.Length);

            return entityName;
        }
    }
}
